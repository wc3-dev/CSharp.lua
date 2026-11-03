local System = System
local bitLShift = System.sl
local bitNot = System.bnot

local HashCodeHelper = {}

function HashCodeHelper.CombineHashCodes(h1, h2)
  return (bitLShift(h1, 5) + h1) * bitNot(h2)
end

System.define("System.Numerics.HashCodeHelper", HashCodeHelper)

-- Here, not in Number.lua, so that a map naming no System.Numerics leaves the Numerics files out (Moonwell's trim).
local UInt32 = System.UInt32
System.define("System.Numerics.BitOperations", {
  PopCount = UInt32.PopCount,
  LeadingZeroCount = UInt32.LeadingZeroCount,
  TrailingZeroCount = UInt32.TrailingZeroCount,
  Log2 = UInt32.Log2,
  IsPow2 = UInt32.IsPow2,
  RotateLeft = UInt32.RotateLeft,
  RotateRight = UInt32.RotateRight
})
