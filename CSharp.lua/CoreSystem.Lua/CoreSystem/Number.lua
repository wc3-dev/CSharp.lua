--[[
Copyright 2017 YANG Huan (sy.yanghuan@gmail.com).

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
--]]

local System = System
local throw = System.throw
local define = System.defStc
local equals = System.equals
local zeroFn = System.zeroFn
local hash = System.hash
local debugsetmetatable = System.debugsetmetatable

local IComparable = System.IComparable
local IComparable_1 = System.IComparable_1
local IEquatable_1 = System.IEquatable_1
local IConvertible = System.IConvertible
local IFormattable = System.IFormattable

local ArgumentException = System.ArgumentException
local ArgumentNullException = System.ArgumentNullException
local FormatException = System.FormatException
local OverflowException = System.OverflowException

local type = type
local tonumber = tonumber
local floor = math.floor
local abs = math.abs
local mtype = math.type
local setmetatable = setmetatable
local tostring = tostring
local sformat = string.format
local sbyte = string.byte
local schar = string.char
local ssub = string.sub
local srep = string.rep
local sfind = string.find
local sgsub = string.gsub
local tconcat = table.concat

-- C# writes a whole float as 4, Lua as 4.0.
local function numberToString(this)
  local s = tostring(this)
  if s:find("^-?%d+%.0$") then
    s = s:sub(1, -3)
  end
  return s
end

-- Number formats, as .NET writes them in the invariant culture, but 50% without a space, as the game writes it (Sven,
-- 11-03). A float's standard format rounds its exact value,
-- a tie to even, as string.format does; an int's, and every custom format, round half away from zero.

-- The game's numbers are 32-bit floats, of which .NET reads 7 digits for a custom format, as of a float; 15 of a double.
local customPrecision = 16777216.0 + 1.0 == 16777216.0 and 7 or 15

local function isNegative(x)
  return x < 0 or (x == 0 and 1 / x < 0)
end

local function withoutTrailingZeros(digits)
  return (sgsub(digits, "0+$", ""))
end

-- |x| = 0.digits × 10^scale, with no zero at either end of digits; 0 is "" and 0.
local function digitsOf(x, isFloat)
  if x == 0 then
    return "", 0
  end
  if not isFloat then
    local s = tostring(x)
    if sbyte(s) == 45 then
      s = ssub(s, 2)
    end
    return withoutTrailingZeros(s), #s
  end
  local s = sformat("%." .. (customPrecision - 1) .. "e", x < 0 and -x or x)
  local first, rest, exponent = s:match("^(%d)%.?(%d*)e([-+]%d+)$")
  return withoutTrailingZeros(first .. rest), tonumber(exponent) + 1
end

-- digits kept to their first n, rounded half away from zero; a carry past the first digit raises the scale.
local function roundDigits(digits, scale, n)
  if n < 0 or digits == "" then
    return "", 0
  end
  if #digits <= n then
    return digits, scale
  end
  local up = sbyte(digits, n + 1) >= 53
  digits = ssub(digits, 1, n)
  if up then
    local i = n
    while i >= 1 and sbyte(digits, i) == 57 do
      i = i - 1
    end
    if i == 0 then
      digits, scale = "1", scale + 1
    else
      digits = ssub(digits, 1, i - 1) .. schar(sbyte(digits, i) + 1)
    end
  end
  digits = withoutTrailingZeros(digits)
  if digits == "" then
    scale = 0
  end
  return digits, scale
end

-- The integer and fraction digits of 0.digits × 10^scale; the integer part of a number below 1 is "".
local function fixedParts(digits, scale)
  if scale <= 0 then
    return "", srep("0", -scale) .. digits
  elseif scale >= #digits then
    return digits .. srep("0", scale - #digits), ""
  end
  return ssub(digits, 1, scale), ssub(digits, scale + 1)
end

local function group(int)
  local n = #int
  if n <= 3 then
    return int
  end
  local head = n % 3
  if head == 0 then
    head = 3
  end
  local t = { ssub(int, 1, head) }
  for i = head + 1, n, 3 do
    t[#t + 1] = ssub(int, i, i + 2)
  end
  return tconcat(t, ",")
end

local function exponentText(letter, exponent, plus, width)
  return letter .. (exponent < 0 and "-" or plus) .. sformat("%0" .. width .. "d", abs(exponent))
end

-- The integer and fraction digits of |x| with decimals after the point.
local function fixed(x, decimals)
  if mtype(x) == "integer" then
    local int = fixedParts(digitsOf(x, false))
    return int == "" and "0" or int, srep("0", decimals)
  end
  local shown = decimals > 99 and 99 or decimals
  local int, frac = sformat("%." .. shown .. "f", x < 0 and -x or x):match("^(%d+)%.?(%d*)$")
  return int, frac .. srep("0", decimals - shown)
end

-- The significant digits of |x| rounded to n, and the exponent of the first; 0 is "".
local function significant(x, n)
  if mtype(x) == "integer" then
    local digits, scale = digitsOf(x, false)
    digits, scale = roundDigits(digits, scale, n)
    return digits, digits == "" and 0 or scale - 1
  end
  if x == 0 then
    return "", 0
  end
  local first, rest, exponent = sformat("%." .. (n - 1) .. "E", x < 0 and -x or x):match("^(%d)%.?(%d*)E([-+]%d+)$")
  return first .. rest, tonumber(exponent)
end

local function standardFormat(x, letter, precision)
  local kind = letter:upper()
  local minus = isNegative(x) and "-" or ""
  if kind == "D" or kind == "X" or kind == "B" then
    if mtype(x) ~= "integer" then
      throw(FormatException())
    end
    local s
    if kind == "D" then
      s = fixed(x, 0)
    else
      local u = x < 0 and x & 0xFFFFFFFF or x
      if kind == "X" then
        s = sformat(letter == "X" and "%X" or "%x", u)
      else
        local bits = {}
        repeat
          bits[#bits + 1] = u & 1 == 1 and "1" or "0"
          u = u >> 1
        until u == 0
        s = tconcat(bits):reverse()
      end
      minus = ""
    end
    if precision and #s < precision then
      s = srep("0", precision - #s) .. s
    end
    return minus .. s
  elseif kind == "F" or kind == "N" or kind == "P" then
    local int, frac = fixed(kind == "P" and x * 100 or x, precision or 2)
    if kind ~= "F" then
      int = group(int)
    end
    local s = minus .. int .. (frac ~= "" and "." .. frac or "")
    return kind == "P" and s .. "%" or s
  elseif kind == "E" then
    precision = precision or 6
    local digits, exponent = significant(x, precision + 1)
    digits = digits .. srep("0", precision + 1 - #digits)
    local mantissa = ssub(digits, 1, 1) .. (precision > 0 and "." .. ssub(digits, 2) or "")
    return minus .. mantissa .. exponentText(letter == "e" and "e" or "E", exponent, "+", 3)
  elseif kind == "G" and precision and precision > 0 then
    local digits, exponent = significant(x, precision)
    digits = withoutTrailingZeros(digits)
    if digits == "" then
      return minus .. "0"
    end
    if exponent >= -5 and exponent < precision then
      local int, frac = fixedParts(digits, exponent + 1)
      return minus .. (int == "" and "0" or int) .. (frac ~= "" and "." .. frac or "")
    end
    local mantissa = ssub(digits, 1, 1) .. (#digits > 1 and "." .. ssub(digits, 2) or "")
    return minus .. mantissa .. exponentText(letter == "g" and "e" or "E", exponent, "+", 2)
  end
  -- G, R and the formats not written here (C): the number as ToString gives it.
  return numberToString(x)
end

local PerMille = "\226\128\176"

-- A section of a custom format, read into placeholders ("0", "#"), the point, commas, "%", per mille, an exponent
-- and literal text, with what they ask of the number.
local function readSection(text)
  local tokens, i, n = {}, 1, #text
  while i <= n do
    local c = ssub(text, i, i)
    if c == "'" or c == '"' then
      local close = sfind(text, c, i + 1, true) or n + 1
      tokens[#tokens + 1] = { ssub(text, i + 1, close - 1) }
      i = close + 1
    elseif c == "\\" then
      tokens[#tokens + 1] = { ssub(text, i + 1, i + 1) }
      i = i + 2
    elseif (c == "E" or c == "e") and sfind(text, "^[-+]?0", i + 1) then
      local sign, zeros = text:match("^([-+]?)(0+)", i + 1)
      tokens[#tokens + 1] = { exponent = c, plus = sign == "+" and "+" or "", width = #zeros }
      i = i + 1 + #sign + #zeros
    elseif ssub(text, i, i + 2) == PerMille then
      tokens[#tokens + 1] = PerMille
      i = i + 3
    elseif c == "0" or c == "#" or c == "." or c == "," or c == "%" then
      tokens[#tokens + 1] = c
      i = i + 1
    else
      tokens[#tokens + 1] = { c }
      i = i + 1
    end
  end

  local section = { tokens = tokens, text = text, parts = {}, intCount = 0, minInt = 0, maxDec = 0, minDec = 0, shift = 0 }
  local part, lastInt, firstZero, commaAfter = "int", nil, nil, 0
  for k, t in ipairs(tokens) do
    if t == "." and part == "int" then
      part = "frac"
      section.point = k
    elseif type(t) == "table" and t.exponent and not section.exponent then
      part = "exp"
      section.exponent = t
    elseif t == "0" or t == "#" then
      section.parts[k] = part
      if part == "int" then
        section.intCount = section.intCount + 1
        if t == "0" and not firstZero then
          firstZero = section.intCount
        end
        if commaAfter > 0 and lastInt then
          section.grouping = true
        end
        commaAfter, lastInt = 0, k
      elseif part == "frac" then
        section.maxDec = section.maxDec + 1
        if t == "0" then
          section.minDec = section.maxDec
        end
      end
    elseif t == "," and part == "int" then
      commaAfter = commaAfter + 1
    elseif t == "%" then
      section.shift = section.shift + 2
    elseif t == PerMille then
      section.shift = section.shift + 3
    end
  end
  if lastInt then
    section.shift = section.shift - 3 * commaAfter
  end
  if firstZero then
    section.minInt = section.intCount - firstZero + 1
  end
  return section
end

local customFormats = {}

local function readCustomFormat(format)
  local sections = customFormats[format]
  if sections == nil then
    sections = {}
    local start, i, n, quote = 1, 1, #format, nil
    while i <= n do
      local c = ssub(format, i, i)
      if quote then
        if c == quote then quote = nil end
      elseif c == "'" or c == '"' then
        quote = c
      elseif c == "\\" then
        i = i + 1
      elseif c == ";" and #sections < 2 then
        sections[#sections + 1] = readSection(ssub(format, start, i - 1))
        start = i + 1
      end
      i = i + 1
    end
    sections[#sections + 1] = readSection(ssub(format, start))
    customFormats[format] = sections
  end
  return sections
end

-- The digits of |x| rounded as the section asks, with the exponent of a scientific section.
local function roundFor(section, digits, scale)
  scale = scale + section.shift
  if section.exponent then
    if digits == "" then
      return "", 0, 0
    end
    local intCount = section.intCount > 0 and section.intCount or 1
    local exponent = scale - intCount
    digits, scale = roundDigits(digits, intCount, intCount + section.maxDec)
    return digits, intCount, exponent + scale - intCount
  end
  digits, scale = roundDigits(digits, scale, scale + section.maxDec)
  return digits, scale, 0
end

local function writeSection(section, digits, scale, exponent, minus)
  local int, frac = fixedParts(digits, scale)
  if #int < section.minInt then
    int = srep("0", section.minInt - #int) .. int
  end
  if #frac < section.minDec then
    frac = frac .. srep("0", section.minDec - #frac)
  end
  local out, count, shown = {}, 0, false
  local intLength, intCount, intIndex, fracIndex = #int, section.intCount, 0, 0
  local function digit(j)
    count = count + 1
    out[count] = ssub(int, j, j)
    if section.grouping and intLength - j > 0 and (intLength - j) % 3 == 0 then
      count = count + 1
      out[count] = ","
    end
    shown = true
  end
  for k, t in ipairs(section.tokens) do
    if t == "0" or t == "#" then
      local part = section.parts[k]
      if part == "int" then
        intIndex = intIndex + 1
        local last = intLength - intCount + intIndex
        for j = intIndex == 1 and 1 or last, last do
          if j >= 1 then
            digit(j)
          end
        end
      elseif part == "frac" then
        fracIndex = fracIndex + 1
        if fracIndex <= #frac then
          count = count + 1
          out[count] = ssub(frac, fracIndex, fracIndex)
          shown = true
        end
      end
    elseif t == "." then
      if k == section.point then
        if intCount == 0 then
          for j = 1, intLength do
            digit(j)
          end
        end
        if #frac > 0 then
          count = count + 1
          out[count] = "."
        end
      end
    elseif t == "%" or t == PerMille then
      count = count + 1
      out[count] = t
    elseif type(t) == "table" then
      count = count + 1
      if t.exponent then
        out[count] = exponentText(t.exponent, exponent, t.plus, t.width)
      else
        out[count] = t[1]
      end
    end
  end
  local text = tconcat(out)
  return (minus and shown) and "-" .. text or text, shown
end

local function customFormat(x, format)
  local sections = readCustomFormat(format)
  local isFloat = mtype(x) == "float"
  local negative = isNegative(x)
  local digits, scale = digitsOf(x, isFloat)
  local index = 1
  if negative and sections[2] and sections[2].text ~= "" then
    index = 2
  end
  local rounded, roundedScale, exponent = roundFor(sections[index], digits, scale)
  if rounded == "" then
    if sections[3] then
      return (writeSection(sections[3], "", 0, 0, false))
    end
    index = 1
  end
  -- An int rounded to 0 is 0; a float keeps its sign, shown when a digit is.
  local minus = index == 1 and negative and (rounded ~= "" or isFloat)
  local text, shown = writeSection(sections[index], rounded, roundedScale, exponent, minus)
  if minus and not shown and rounded ~= "" then
    text = "-" .. text
  end
  return text
end

local function toStringWithFormat(this, format)
  if #format ~= 0 then
    local letter, precision = format:match("^([A-Za-z])(%d*)$")
    if letter then
      return standardFormat(this, letter, tonumber(precision))
    end
    return customFormat(this, format)
  end
  return numberToString(this)
end

local function toString(this, format)
  if format then
    return toStringWithFormat(this, format)
  end
  return numberToString(this)
end

local function compareInt(this, v)
  if this < v then return -1 end
  if this > v then return 1 end
  return 0
end

local function inherits(_, T)
  return { IComparable, IComparable_1(T), IEquatable_1(T), IConvertible, IFormattable }
end

local Int = define("System.Int", {
  base = inherits,
  default = zeroFn,
  CompareTo = compareInt,
  Equals = equals,
  ToString = toString,
  GetHashCode = hash,
  CompareToObj = function (this, v)
    if v == nil then return 1 end
    if type(v) ~= "number" then
      throw(ArgumentException("Arg_MustBeInt"))
    end
    return compareInt(this, v)
  end,
  EqualsObj = function (this, v)
    if type(v) ~= "number" then
      return false
    end
    return this == v
  end
})
Int.__call = zeroFn

local function parseInt(s, min, max)
  if s == nil then
    return nil, 1
  end
  local v = tonumber(s)
  if v == nil or v ~= floor(v) then
    return nil, 2
  end
  if v < min or v > max then
    return nil, 3
  end
  return v
end

local function tryParseInt(s, min, max)
  local v = parseInt(s, min, max)
  if v then
    return true, v
  end
  return false, 0
end

local function parseIntWithException(s, min, max)
  local v, err = parseInt(s, min, max)
  if v then
    return v
  end
  if err == 1 then
    throw(ArgumentNullException())
  elseif err == 2 then
    throw(FormatException())
  else
    throw(OverflowException())
  end
end

local SByte = define("System.SByte", {
  Parse = function (s)
    return parseIntWithException(s, -128, 127)
  end,
  TryParse = function (s)
    return tryParseInt(s, -128, 127)
  end
})
setmetatable(SByte, Int)

local Byte = define("System.Byte", {
  Parse = function (s)
    return parseIntWithException(s, 0, 255)
  end,
  TryParse = function (s)
    return tryParseInt(s, 0, 255)
  end
})
setmetatable(Byte, Int)

local Int16 = define("System.Int16", {
  Parse = function (s)
    return parseIntWithException(s, -32768, 32767)
  end,
  TryParse = function (s)
    return tryParseInt(s, -32768, 32767)
  end
})
setmetatable(Int16, Int)

local UInt16 = define("System.UInt16", {
  Parse = function (s)
    return parseIntWithException(s, 0, 65535)
  end,
  TryParse = function (s)
    return tryParseInt(s, 0, 65535)
  end
})
setmetatable(UInt16, Int)

local Int32 = define("System.Int32", {
  Parse = function (s)
    return parseIntWithException(s, -2147483648, 2147483647)
  end,
  TryParse = function (s)
    return tryParseInt(s, -2147483648, 2147483647)
  end
})
setmetatable(Int32, Int)

local UInt32 = define("System.UInt32", {
  Parse = function (s)
    return parseIntWithException(s, 0, 4294967295)
  end,
  TryParse = function (s)
    return tryParseInt(s, 0, 4294967295)
  end
})
setmetatable(UInt32, Int)

-- Bits of a 32-bit integer. The game's Lua has 32-bit integers, where the masks change nothing; the desktop's has 64.
local function toInt32(x)
  x = x & 0xFFFFFFFF
  if x >= 0x80000000 then
    x = x - 0x100000000
  end
  return x
end

local function rotateLeft(x, offset)
  local u, n = x & 0xFFFFFFFF, offset & 31
  return ((u << n) | (u >> (32 - n))) & 0xFFFFFFFF
end

local function popCount(x)
  local u, count = x & 0xFFFFFFFF, 0
  while u ~= 0 do
    u = u & (u - 1)
    count = count + 1
  end
  return count
end

local function leadingZeroCount(x)
  local u = x & 0xFFFFFFFF
  for i = 31, 0, -1 do
    if (u >> i) & 1 == 1 then
      return 31 - i
    end
  end
  return 32
end

local function trailingZeroCount(x)
  local u = x & 0xFFFFFFFF
  for i = 0, 31 do
    if (u >> i) & 1 == 1 then
      return i
    end
  end
  return 32
end

local function log2(x)
  return 31 - leadingZeroCount(x | 1)
end

local function isPow2(x)
  x = x & 0xFFFFFFFF
  return x ~= 0 and x & (x - 1) == 0
end

Int32.RotateLeft = function (x, offset) return toInt32(rotateLeft(x, offset)) end
Int32.RotateRight = function (x, offset) return toInt32(rotateLeft(x, -offset)) end
Int32.PopCount = popCount
Int32.LeadingZeroCount = leadingZeroCount
Int32.TrailingZeroCount = trailingZeroCount
Int32.Log2 = function (x)
  if x < 0 then throw(System.ArgumentOutOfRangeException("value")) end
  return log2(x)
end
Int32.IsPow2 = function (x) return x > 0 and isPow2(x) end
UInt32.RotateLeft = rotateLeft
UInt32.RotateRight = function (x, offset) return rotateLeft(x, -offset) end

System.define("System.Numerics.BitOperations", {
  PopCount = popCount,
  LeadingZeroCount = leadingZeroCount,
  TrailingZeroCount = trailingZeroCount,
  Log2 = log2,
  IsPow2 = isPow2,
  RotateLeft = rotateLeft,
  RotateRight = UInt32.RotateRight
})

local Int64 = define("System.Int64", {
  Parse = function (s)
    return parseIntWithException(s, (-9223372036854775807 - 1), 9223372036854775807)
  end,
  TryParse = function (s)
    return tryParseInt(s, (-9223372036854775807 - 1), 9223372036854775807)
  end
})
setmetatable(Int64, Int)

local UInt64 = define("System.UInt64", {
  Parse = function (s)
    return parseIntWithException(s, 0, 18446744073709551615.0)
  end,
  TryParse = function (s)
    return tryParseInt(s, 0, 18446744073709551615.0)
  end
})
setmetatable(UInt64, Int)

local nan = 0 / 0
local posInf = 1 / 0
local negInf = - 1 / 0

--http://lua-users.org/wiki/InfAndNanComparisons
local function isNaN(v)
  return v ~= v
end

local function compareDouble(this, v)
  if this < v then return -1 end
  if this > v then return 1 end
  if this == v then return 0 end
  if isNaN(this) then
    return isNaN(v) and 0 or -1
  else
    return 1
  end
end

local function equalsDouble(this, v)
  if this == v then return true end
  return isNaN(this) and isNaN(v)
end

local function equalsObj(this, v)
  if type(v) ~= "number" then
    return false
  end
  return equalsDouble(this, v)
end

local Number = define("System.Number", {
  base = inherits,
  default = zeroFn,
  CompareTo = compareDouble,
  Equals = equalsDouble,
  ToString = toString,
  NaN = nan,
  IsNaN = isNaN,
  NegativeInfinity = negInf,
  PositiveInfinity = posInf,
  EqualsObj = equalsObj,
  GetHashCode = hash,
  CompareToObj = function (this, v)
    if v == nil then return 1 end
    if type(v) ~= "number" then
      throw(ArgumentException("Arg_MustBeNumber"))
    end
    return compareDouble(this, v)
  end,
  IsFinite = function (v)
    return v ~= posInf and v ~= negInf and not isNaN(v)
  end,
  IsInfinity = function (v)
    return v == posInf or v == negInf
  end,
  IsNegativeInfinity = function (v)
    return v == negInf
  end,
  IsPositiveInfinity = function (v)
    return v == posInf
  end
})
Number.__call = zeroFn
if debugsetmetatable then
  debugsetmetatable(0, Number)
end

local function parseDouble(s)
  if s == nil then
    return nil, 1
  end
  local v = tonumber(s)
  if v == nil then
    return nil, 2
  end
  return v
end

local function parseDoubleWithException(s)
  local v, err = parseDouble(s)
  if v then
    return v
  end
  if err == 1 then
    throw(ArgumentNullException())
  else
    throw(FormatException())
  end
end

local function zeroFloatFn()
  return 0.0
end

local Single = define("System.Single", {
  default = zeroFloatFn,
  Parse = function (s)
    local v = parseDoubleWithException(s)
    if v < -3.40282347E+38 or v > 3.40282347E+38 then
      throw(OverflowException())
    end
    return v
  end,
  TryParse = function (s)
    local v = parseDouble(s)
    if v and v >= -3.40282347E+38 and v < 3.40282347E+38 then
      return true, v
    end
    return false, 0
  end
})
setmetatable(Single, Number)

local Double = define("System.Double", {
  default = zeroFloatFn,
  Parse = parseDoubleWithException,
  TryParse = function (s)
    local v = parseDouble(s)
    if v then
      return true, v
    end
    return false, 0
  end
})
setmetatable(Double, Number)

if not debugsetmetatable then
  local NullReferenceException = System.NullReferenceException
  local systemToString = System.toString

  function System.ObjectEqualsObj(this, obj)
    if this == nil then throw(NullReferenceException()) end
    local t = type(this)
    if t == "number" then
      return equalsObj(this, obj)
    elseif t == "table" then
      return this:EqualsObj(obj)
    end
    return this == obj
  end

  function System.ObjectGetHashCode(this)
    if this == nil then throw(NullReferenceException()) end
    if type(this) == "table" then
      return this:GetHashCode()
    end
    return hash(this)
  end

  function System.ObjectToString(this)
    if this == nil then throw(NullReferenceException()) end
    return systemToString(this)
  end

  function System.IComparableCompareTo(this, other)
    if this == nil then throw(NullReferenceException()) end
    local t = type(this)
    if t == "number" then
      return compareDouble(this, other)
    elseif t == "boolean" then
      return System.Boolean.CompareTo(this, other)
    end
    return this:CompareTo(other)
  end

  function System.IEquatableEquals(this, other)
    if this == nil then throw(NullReferenceException()) end
    local t = type(this)
    if t == "number" then
      return equalsDouble(this, other)
    elseif t == "boolean" then
      return System.Boolean.Equals(this, other)
    end
    return this:Equals(other)
  end

  function System.IFormattableToString(this, format, formatProvider)
    if this == nil then throw(NullReferenceException()) end
    local t = type(this)
    if t == "number" then
      return toString(this, format, formatProvider)
    end
    return this:ToString(format, formatProvider)
  end
end

