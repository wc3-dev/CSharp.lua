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
local Int = System.Int
local Number = System.Number
local band = System.band
local bor = System.bor
local ArgumentNullException = System.ArgumentNullException
local ArgumentException = System.ArgumentException

local assert = assert
local pairs = pairs
local tostring = tostring
local type = type

-- An enum class's members as .NET orders them: by value, unsigned, then by name; worked out once for each class.
local membersOf = setmetatable({}, { __mode = "k" })

local function getMembers(cls)
  local members = membersOf[cls]
  if members == nil then
    members = {}
    for k, v in pairs(cls) do
      if type(v) == "number" then
        members[#members + 1] = { k, v }
      end
    end
    table.sort(members, function (a, b)
      if a[2] ~= b[2] then
        return math.ult(a[2], b[2])
      end
      return a[1] < b[1]
    end)
    membersOf[cls] = members
  end
  return members
end

local function nameOf(cls, value)
  for _, member in ipairs(getMembers(cls)) do
    if member[2] == value then
      return member[1]
    end
  end
end

-- A combination of flags as .NET names it: from the largest member down, each one all of whose bits are set, named
-- smallest first; nil when bits are left over.
local function flagsName(cls, value)
  local members = getMembers(cls)
  if value == 0 then
    return nameOf(cls, 0) or "0"
  end
  local names, rest = {}, value
  for i = #members, 1, -1 do
    local v = members[i][2]
    if v ~= 0 and band(rest, v) == v then
      table.insert(names, 1, members[i][1])
      rest = rest - v
      if rest == 0 then
        return table.concat(names, ", ")
      end
    end
  end
end

local function toString(this, cls, format)
  if this == nil then return "" end
  if cls == nil then return tostring(this) end
  local f = format and format:upper()
  if f == "D" then
    return tostring(this)
  elseif f == "X" then
    return ("%08X"):format(band(this, 0xFFFFFFFF))
  end
  local name = nameOf(cls, this)
  if name then
    return name
  end
  if cls.__flags__ or f == "F" then
    name = flagsName(cls, this)
  end
  return name or tostring(this)
end

local function hasFlag(this, flag)
  if this == flag then
    return true
  end
  return band(this, flag) == flag
end

Number.EnumToString = toString
Number.HasFlag = hasFlag
System.EnumToString = toString
System.EnumHasFlag = hasFlag

local function tryParseEnum(enumType, value, ignoreCase)
  if enumType == nil then throw(ArgumentNullException("enumType")) end
  local cls = enumType[1] or enumType
  if cls.class ~= "E" then throw(ArgumentException("Arg_MustBeEnum")) end
  if value == nil then
    return
  end
  local number = tonumber(value:match("^%s*([-+]?%d+)%s*$"))
  if number then
    return math.tointeger(number)
  end
  if ignoreCase then
    value = value:lower()
  end
  local i, j, s, r = 1
  while true do
    i, j, s = value:find("%s*(%a+)%s*", i)
    if not i then
      return
    end
    for k, v in pairs(cls) do
      local k = k
      if ignoreCase then
        k = k:lower()
      end
      if k == s then
        if not r then
          r = v
        else
          r = bor(r, v)
        end
        break
      end
    end
    i = value:find(',', j + 1)
    if not i then
      break
    end
    i = i + 1
  end
  return r
end

System.define("System.Enum", {
  CompareToObj = Int.CompareToObj,
  EqualsObj = Int.EqualsObj,
  default = Int.default,
  ToString = toString,
  HasFlag = hasFlag,
  GetHashCode = Int.GetHashCode,
  GetName = function (enumType, value)
    if enumType == nil then throw(ArgumentNullException("enumType")) end
    if value == nil then throw(ArgumentNullException("value")) end
    local cls = enumType[1] or enumType
    if cls.class ~= "E" then throw(ArgumentException("Arg_MustBeEnum")) end
    return nameOf(cls, value)
  end,
  GetNames = function (enumType)
    if enumType == nil then throw(ArgumentNullException("enumType")) end
    local cls = enumType[1] or enumType
    if cls.class ~= "E" then throw(ArgumentException("Arg_MustBeEnum")) end
    local t = {}
    for i, member in ipairs(getMembers(cls)) do
      t[i] = member[1]
    end
    return System.arrayFromTable(t, System.String)
  end,
  -- The values as the enum's own type, so that text made of them names them.
  GetValues = function (enumType)
    if enumType == nil then throw(ArgumentNullException("enumType")) end
    local cls = enumType[1] or enumType
    if cls.class ~= "E" then throw(ArgumentException("Arg_MustBeEnum")) end
    local t = {}
    for i, member in ipairs(getMembers(cls)) do
      t[i] = member[2]
    end
    return System.arrayFromTable(t, cls)
  end,
  IsDefined = function (enumType, value)
    if enumType == nil then throw(ArgumentNullException("enumType")) end
    if value == nil then throw(ArgumentNullException("value")) end
    local cls = enumType[1] or enumType
    if cls.class ~= "E" then throw(ArgumentException("Arg_MustBeEnum")) end
    local t = type(value)
    if t == "string" then
      return cls[value] ~= nil
    elseif t == "number" then
      for _, v in pairs(cls) do
        if v == value then
          return true
        end
      end
      return false
    end
    throw(System.InvalidOperationException())
  end,
  Parse = function (enumType, value, ignoreCase)
    local result = tryParseEnum(enumType, value, ignoreCase)
    if result == nil then
      throw(ArgumentException("Requested value '" .. value .. "' was not found."))
    end
    return result
  end,
  TryParse = function (type, value, ignoreCase)
    local result = tryParseEnum(type, value, ignoreCase)
    if result == nil then
      return false, 0
    end
    return true, result
  end
})
