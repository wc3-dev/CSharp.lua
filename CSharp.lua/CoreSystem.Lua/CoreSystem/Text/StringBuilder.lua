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
local clear = System.Array.clear
local toString = System.toString
local ArgumentException = System.ArgumentException
local ArgumentNullException = System.ArgumentNullException
local ArgumentOutOfRangeException = System.ArgumentOutOfRangeException
local IndexOutOfRangeException = System.IndexOutOfRangeException

local table = table
local tconcat = table.concat
local schar = string.char
local ssub = string.sub
local sbyte = string.byte
local sfind = string.find
local srep = string.rep
local type = type
local select = select

-- A char[] is its characters as text; anything else is its ToString.
local function textOf(value)
  if type(value) == "table" and value.__genericT__ == System.Char and System.isArrayLike(value) then
    return System.String(value)
  end
  return toString(value)
end

-- Puts s in place of the builder's text.
local function reset(this, s)
  clear(this)
  if #s > 0 then
    this[1] = s
  end
  this.Length = #s
end

local function checkRange(length, startIndex, count)
  if startIndex < 0 or count < 0 or startIndex + count > length then
    throw(ArgumentOutOfRangeException("startIndex"))
  end
end

local function toStringOf(this, startIndex, length)
  local s = tconcat(this)
  if startIndex == nil then
    return s
  end
  checkRange(#s, startIndex, length)
  return ssub(s, startIndex + 1, startIndex + length)
end

local function build(this, value, startIndex, length)
  value = value:Substring(startIndex, length)
  local len = #value
  if len > 0 then
    this[#this + 1] = value
    this.Length = len
  end
end

local function getItemIndex(this, index)
  for i = 1, #this do
    local s = this[i]
    local len = #s
    local begin = index
    index = index - len
    if index < 0 then
      begin = begin + 1
      local ch = sbyte(s, begin)
      if not ch then
        break
      end
      return i, s, begin, ch
    end
  end
end

local function getLength(this)
  return this.Length
end

local StringBuilder = System.define("System.Text.StringBuilder", { 
  Length = 0,
  ToString = toStringOf,
  __tostring = tconcat,
  __ctor__ = function (this, ...)
    local len = select("#", ...)
    if len == 0 then
    elseif len == 1 or len == 2 then
      local value = ...
      if type(value) == "string" then
        build(this, value, 0, #value)
      else
        build(this, "", 0, 0)
      end
    else 
      local value, startIndex, length = ...
      build(this, value, startIndex, length)
    end
  end,
  get = function (this, index)
    local _, _, _, ch = getItemIndex(this, index)
    if not _ then
      throw(IndexOutOfRangeException())
    end
    return ch
  end,
  set = function (this, index, value)
    local i, s, j = getItemIndex(this, index)
    if not i then
      throw(ArgumentOutOfRangeException("index"))
    end
    this[i] = ssub(s, 1, j - 1) .. schar(value) .. ssub(s, j + 1)
  end,
  setCapacity = function (this, value)
    if value < this.Length then
      throw(ArgumentOutOfRangeException())
    end
  end,
  getCapacity = getLength,
  getMaxCapacity = getLength,
  getLength = getLength,
  setLength = function (this, value) 
    if value < 0 then throw(ArgumentOutOfRangeException("value")) end
    if value == 0 then
      this:Clear()
      return
    end
    local delta = value - this.Length
    if delta > 0 then
      this:AppendCharRepeat(0, delta)
    else
      local length, remain = #this, value
      for i = 1, length do
        local s = this[i]
        local len = #s
        if len >= remain then
          if len ~= remain then
            s = ssub(s, 0, remain)
            this[i] = s
          end
          for j = i + 1, length do
            this[j] = nil
          end
          break
        end
        remain = remain - len
      end
      this.Length = this.Length + delta
    end  
  end,
  Append = function (this, value, startIndex, count)
    if not startIndex then
      if value ~= nil then
        value = textOf(value)
        if value ~= nil then
          this[#this + 1] = value
          this.Length =  this.Length + #value
        end
      end
    else
      if value == nil then
        throw(ArgumentNullException("value"))
      end
      if type(value) == "string" then
        value = value:Substring(startIndex, count)
      else
        value = System.String(value, startIndex, count)
      end
      this[#this + 1] = value
      this.Length =  this.Length + #value
    end
    return this
  end,
  -- (separator, values...), (separator, values) and (separator, values, T): params come as arguments of their own.
  AppendJoin = function (this, separator, ...)
    if type(separator) == "number" then
      separator = schar(separator)
    end
    local n, values = select("#", ...), ...
    local t, count = {}, 0
    if n <= 2 and type(values) == "table" and values.GetEnumerator ~= nil then
      local isChar = select(2, ...) == System.Char
      for _, v in System.each(values) do
        count = count + 1
        t[count] = isChar and schar(v) or (v ~= nil and toString(v) or "")
      end
    else
      for i = 1, n do
        local v = select(i, ...)
        count = count + 1
        t[count] = v ~= nil and toString(v) or ""
      end
    end
    return this:Append(tconcat(t, separator))
  end,
  AppendChar = function (this, v) 
    v = schar(v)
    this[#this + 1] = v
    this.Length = this.Length + 1
    return this
  end,
  AppendCharRepeat = function (this, v, repeatCount)
    if repeatCount < 0 then throw(ArgumentOutOfRangeException("repeatCount")) end
    if repeatCount == 0 then return this end
    v = schar(v)
    local count = #this + 1
    for i = 1, repeatCount do
      this[count] = v
      count = count + 1
    end
    this.Length = this.Length + repeatCount
    return this
  end,
  AppendFormat = function (this, format, ...)
    local value = format:Format(...)
    this[#this + 1] = value
    this.Length = this.Length + #value
    return this
  end,
  AppendLine = function (this, value)
    local count = 1
    local len = #this + 1
    if value ~= nil then
      this[len] = value
      len = len + 1
      count = count + #value
    end
    this[len] = "\n"
    this.Length = this.Length + count
    return this
  end,
  Clear = function (this)
    clear(this)
    this.Length = 0
    return this
  end,
  -- (index, value), (index, value, count) and (index, char[], startIndex, charCount).
  Insert = function (this, index, value, count, charCount)
    if index < 0 or index > this.Length then
      throw(ArgumentOutOfRangeException("index"))
    end
    if value ~= nil then
      if charCount then
        value = System.String(value, count, charCount)
      else
        value = textOf(value)
        if count then
          if count < 0 then throw(ArgumentOutOfRangeException("count")) end
          value = srep(value, count)
        end
      end
      if #value > 0 then
        local s = tconcat(this)
        reset(this, ssub(s, 1, index) .. value .. ssub(s, index + 1))
      end
    end
    return this
  end,
  InsertChar = function (this, index, c)
    return this:Insert(index, schar(c))
  end,
  Remove = function (this, startIndex, length)
    checkRange(this.Length, startIndex, length)
    if length > 0 then
      local s = tconcat(this)
      reset(this, ssub(s, 1, startIndex) .. ssub(s, startIndex + length + 1))
    end
    return this
  end,
  -- Every oldValue inside the range, as it reads before any is replaced; a char comes as its number.
  Replace = function (this, oldValue, newValue, startIndex, count)
    if type(oldValue) == "number" then
      oldValue, newValue = schar(oldValue), schar(newValue)
    end
    if oldValue == nil then throw(ArgumentNullException("oldValue")) end
    if #oldValue == 0 then throw(ArgumentException("oldValue")) end
    local s = tconcat(this)
    startIndex = startIndex or 0
    count = count or #s - startIndex
    checkRange(#s, startIndex, count)
    local region = ssub(s, startIndex + 1, startIndex + count)
    local t, n, i = {}, 0, 1
    while true do
      local first, last = sfind(region, oldValue, i, true)
      if not first then
        break
      end
      t[n + 1], t[n + 2] = ssub(region, i, first - 1), newValue or ""
      n, i = n + 2, last + 1
    end
    if n > 0 then
      t[n + 1] = ssub(region, i)
      reset(this, ssub(s, 1, startIndex) .. tconcat(t) .. ssub(s, startIndex + count + 1))
    end
    return this
  end,
  Equals = function (this, other)
    return other ~= nil and tconcat(this) == tconcat(other)
  end,
  CopyTo = function (this, sourceIndex, destination, destinationIndex, count)
    if destination == nil then throw(ArgumentNullException("destination")) end
    local s = tconcat(this)
    checkRange(#s, sourceIndex, count)
    System.checkIndexAndCount(destination, destinationIndex, count)
    for i = 1, count do
      destination[destinationIndex + i] = sbyte(s, sourceIndex + i)
    end
  end
})
System.StringBuilder = StringBuilder
