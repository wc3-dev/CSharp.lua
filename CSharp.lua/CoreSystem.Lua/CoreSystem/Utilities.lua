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
local define = System.define
local trunc = System.trunc
local sl = System.sl
local bor = System.bor
local TimeSpan = System.TimeSpan
local ArgumentNullException = System.ArgumentNullException

local select = select
local type = type
local os = os
local clock = os.clock
local tostring = tostring
local collectgarbage = collectgarbage

define("System.Environment", {
  Exit = os.exit,
  -- The game's line break, as AppendLine writes it; .NET on Windows has "\r\n".
  getNewLine = function ()
    return "\n"
  end,
  getStackTrace = System.traceback,
  getTickCount = function ()
    return System.currentTimeMillis() % 2147483648
  end
})

-- Bits as booleans, the first at index 0; And, Or, Xor, Not and the shifts change it and give it back.
local BitArray
local function checkSameLength(this, other)
  if other == nil then throw(ArgumentNullException("value")) end
  if other.length ~= this.length then throw(System.ArgumentException("value")) end
end

local function bitArrayEnumerator(this)
  local bits, length, index = this.bits, this.length, 0
  local en = {}
  function en.MoveNext()
    if index < length then
      index = index + 1
      en.current = bits[index]
      return true
    end
    return false
  end
  function en.getCurrent()
    return en.current
  end
  en.Dispose = System.emptyFn
  return en
end

BitArray = define("System.BitArray", {
  base = function ()
    return { System.ICollection, System.ICloneable }
  end,
  -- (length), (length, value), (bool[]), (byte[] or int[], 8 or 32 bits each) or (BitArray)
  __ctor__ = function (this, value, defaultValue)
    local bits, length = {}, 0
    if type(value) == "number" then
      if value < 0 then throw(System.ArgumentOutOfRangeException("length")) end
      length = value
      for i = 1, length do
        bits[i] = defaultValue == true
      end
    elseif getmetatable(value) == BitArray then
      length = value.length
      for i = 1, length do
        bits[i] = value.bits[i]
      end
    else
      if value == nil then throw(ArgumentNullException("values")) end
      local T = value.__genericT__
      if T == System.Boolean then
        length = #value
        for i = 1, length do
          bits[i] = value:get(i - 1)
        end
      else
        local width = T == System.Byte and 8 or 32
        for i = 1, #value do
          local v = value:get(i - 1) & 0xFFFFFFFF
          for b = 0, width - 1 do
            length = length + 1
            bits[length] = (v >> b) & 1 == 1
          end
        end
      end
    end
    this.bits, this.length = bits, length
  end,
  get = function (this, index)
    if index < 0 or index >= this.length then throw(System.ArgumentOutOfRangeException("index")) end
    return this.bits[index + 1]
  end,
  set = function (this, index, value)
    if index < 0 or index >= this.length then throw(System.ArgumentOutOfRangeException("index")) end
    this.bits[index + 1] = value
  end,
  Get = function (this, index)
    return this:get(index)
  end,
  Set = function (this, index, value)
    this:set(index, value)
  end,
  getLength = function (this)
    return this.length
  end,
  setLength = function (this, value)
    if value < 0 then throw(System.ArgumentOutOfRangeException("value")) end
    local bits = this.bits
    for i = this.length + 1, value do
      bits[i] = false
    end
    for i = value + 1, this.length do
      bits[i] = nil
    end
    this.length = value
  end,
  getCount = function (this)
    return this.length
  end,
  SetAll = function (this, value)
    for i = 1, this.length do
      this.bits[i] = value
    end
  end,
  And = function (this, other)
    checkSameLength(this, other)
    for i = 1, this.length do
      this.bits[i] = this.bits[i] and other.bits[i]
    end
    return this
  end,
  Or = function (this, other)
    checkSameLength(this, other)
    for i = 1, this.length do
      this.bits[i] = this.bits[i] or other.bits[i]
    end
    return this
  end,
  Xor = function (this, other)
    checkSameLength(this, other)
    for i = 1, this.length do
      this.bits[i] = this.bits[i] ~= other.bits[i]
    end
    return this
  end,
  Not = function (this)
    for i = 1, this.length do
      this.bits[i] = not this.bits[i]
    end
    return this
  end,
  -- Toward higher indexes; the lowest count bits become false.
  LeftShift = function (this, count)
    local bits, length = this.bits, this.length
    for i = length, 1, -1 do
      bits[i] = i > count and bits[i - count] or false
    end
    return this
  end,
  RightShift = function (this, count)
    local bits, length = this.bits, this.length
    for i = 1, length do
      bits[i] = i + count <= length and bits[i + count] or false
    end
    return this
  end,
  HasAllSet = function (this)
    for i = 1, this.length do
      if not this.bits[i] then
        return false
      end
    end
    return true
  end,
  HasAnySet = function (this)
    for i = 1, this.length do
      if this.bits[i] then
        return true
      end
    end
    return false
  end,
  Clone = function (this)
    return BitArray(this)
  end,
  GetEnumerator = bitArrayEnumerator
})

-- The same on every machine, so a map's hashes agree in multiplayer; .NET seeds its own anew for each run.
local function hashOf(value, T)
  if value == nil then
    return 0
  end
  local h = System.EqualityComparer(T).getDefault():GetHashCodeOf(value) // 1
  return math.tointeger(h) or 0
end

local function mix(h, value)
  h = (h * 31 + value) & 0xFFFFFFFF
  if h >= 0x80000000 then
    h = h - 0x100000000
  end
  return h
end

local HashCode
HashCode = System.defStc("System.HashCode", {
  hash = 17,
  __ctor__ = function (this)
    this.hash = 17
  end,
  -- (value, T) or (value, comparer, T)
  Add = function (this, value, ...)
    local comparer, T = ...
    if select("#", ...) == 1 then
      T, comparer = comparer, nil
    end
    this.hash = mix(this.hash, comparer and comparer:GetHashCodeOf(value) or hashOf(value, T))
  end,
  ToHashCode = function (this)
    return this.hash
  end,
  -- (value1, ..., valueN, T1, ..., TN)
  Combine = function (...)
    local n = select("#", ...) // 2
    local h = 17
    for i = 1, n do
      h = mix(h, hashOf((select(i, ...)), (select(n + i, ...))))
    end
    return h
  end,
  default = function ()
    return HashCode()
  end
})

define("System.GC", {
  Collect = function ()
    collectgarbage("collect")
  end,
  GetTotalMemory = function (forceFullCollection)
    if forceFullCollection then 
      collectgarbage("collect")
    end
    return collectgarbage("count") * 1024
  end
})

local Lazy = {
  created = false,
  __ctor__ = function (this, ...)
    local n = select("#", ...)
    if n == 0 then
    elseif n == 1 then
      local valueFactory = ...
      if valueFactory == nil then
        throw(ArgumentNullException("valueFactory"))
      elseif type(valueFactory) ~= "boolean" then
        this.valueFactory = valueFactory
      end
    elseif n == 2 then
      local valueFactory = ...
      if valueFactory == nil then
        throw(ArgumentNullException("valueFactory"))
      end
      this.valueFactory = valueFactory
    end
  end,
  getIsValueCreated = function (this)
    return this.created
  end,
  getValue = function (this)
    if not this.created then
      local valueFactory = this.valueFactory
      if valueFactory then
        this.value = valueFactory()
        this.valueFactory = nil
      else
        this.value = this.__genericT__()
      end
      this.created = true
    end
    return this.value
  end,
  ToString = function (this)
    if this.created then
      return this.value:ToString()
    end
    return "Value is not created."
  end
}

define("System.Lazy", function (T)
  return { 
    __genericT__ = T 
  }
end, Lazy, 1)

local ticker, frequency
local time = System.config.time
if time then
  ticker = time
  frequency = 10000
else
  ticker = clock
  frequency = 1000
end

local function getRawElapsedSeconds(this)
  local timeElapsed = this.elapsed
  if this.running then
    local currentTimeStamp = ticker()
    local elapsedUntilNow  = currentTimeStamp - this.startTimeStamp
    timeElapsed = timeElapsed + elapsedUntilNow
  end
  return timeElapsed
end

local Stopwatch
Stopwatch = define("System.Diagnostics.Stopwatch", {
  elapsed = 0,
  running = false,
  IsHighResolution = false,
  Frequency = frequency,
  StartNew = function ()
    local t = Stopwatch()
    t:Start()
    return t
  end,
  GetTimestamp = function ()
    return trunc(ticker() * frequency)
  end,
  Start = function (this)
    if not this.running then
      this.startTimeStamp = ticker()
      this.running = true
    end
  end,
  Stop = function (this)
    if this.running then
      local endTimeStamp = ticker()
      local elapsedThisPeriod = endTimeStamp - this.startTimeStamp
      local elapsed = this.elapsed + elapsedThisPeriod
      this.running = false
      if elapsed < 0 then
        -- os.clock may be return negative value
        elapsed = 0
      end
      this.elapsed = elapsed
    end
  end,
  Reset = function (this)
    this.elapsed = 0
    this.running = false
    this.startTimeStamp = 0
  end,
  Restart = function (this)
    this.elapsed = 0
    this.startTimeStamp = ticker()
    this.running = true
  end,
  getIsRunning = function (this)
    return this.running
  end,
  getElapsed = function (this)
    return TimeSpan(trunc(getRawElapsedSeconds(this) * 1e7))
  end,
  getElapsedMilliseconds = function (this)
    return trunc(getRawElapsedSeconds(this) * 1000)
  end,
  getElapsedTicks = function (this)
    return trunc(getRawElapsedSeconds(this) * frequency)
  end
})
System.Stopwatch = Stopwatch

local weaks = setmetatable({}, { __mode = "kv" })

local function setWeakTarget(this, target)
  weaks[this] = target
end

define("System.WeakReference", {
  trackResurrection = false,
  SetTarget = setWeakTarget,
  setTarget = setWeakTarget,
  __ctor__ = function (this, target, trackResurrection)
    if trackResurrection then
      this.trackResurrection = trackResurrection
    end
    weaks[this] = target
  end,
  TryGetTarget = function (this)
    local target = weaks[this]
    return target ~= nil, target
  end,
  getIsAlive = function (this)
    return weaks[this] ~= nil
  end,
  getTrackResurrection = function (this)
    return this.trackResurrection
  end,
  getTarget = function (this)
    return weaks[this]
  end
})

define("System.Guid", {})
define("System.ArraySegment", {})
