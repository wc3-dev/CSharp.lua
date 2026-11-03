using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Number members the Lua core lacked or answered wrongly (Moonwell's MW0030 rejected them): each prints in Lua what it
  /// prints on .NET, as 32-bit integers, which the game's are.
  /// </summary>
  [TestClass]
  public class LibraryNumberTests {
    [TestMethod]
    public void TestDivRem() {
      SameAsNet.Prints(@"
Console.WriteLine(Math.DivRem(17, 5, out var r) + "" "" + r + "" "" + Math.DivRem(-17, 5, out var s) + "" "" + s);
var (q, m) = Math.DivRem(17, -5);
Console.WriteLine(q + "" "" + m);");
    }

    [TestMethod]
    public void TestRotate() {
      SameAsNet.Prints(@"
Console.WriteLine(int.RotateLeft(1, 31) + "" "" + int.RotateLeft(int.MinValue, 1) + "" "" + int.RotateRight(1, 1) + "" "" + int.RotateLeft(0x12345678, 8));
Console.WriteLine(int.RotateLeft(-1, 5) + "" "" + int.RotateLeft(1, 33) + "" "" + int.RotateLeft(1, -1) + "" "" + int.RotateRight(6, 0));
Console.WriteLine(uint.RotateLeft(1u, 31) + "" "" + uint.RotateRight(1u, 1) + "" "" + uint.RotateLeft(0x80000001u, 1));");
    }

    [TestMethod]
    public void TestBitCounts() {
      SameAsNet.Prints(@"
Console.WriteLine(int.PopCount(255) + "" "" + int.PopCount(-1) + "" "" + int.PopCount(0) + "" | "" + int.LeadingZeroCount(1) + "" "" + int.LeadingZeroCount(0) + "" "" + int.LeadingZeroCount(-1)
  + "" | "" + int.TrailingZeroCount(8) + "" "" + int.TrailingZeroCount(0) + "" "" + int.TrailingZeroCount(int.MinValue));
Console.WriteLine(int.Log2(1) + "" "" + int.Log2(1024) + "" "" + int.Log2(int.MaxValue) + "" "" + int.IsPow2(64) + "" "" + int.IsPow2(0) + "" "" + int.IsPow2(-8) + "" "" + int.IsPow2(int.MinValue));
Console.WriteLine(System.Numerics.BitOperations.PopCount(255u) + "" "" + System.Numerics.BitOperations.LeadingZeroCount(1u) + "" "" + System.Numerics.BitOperations.TrailingZeroCount(8u)
  + "" "" + System.Numerics.BitOperations.Log2(0u) + "" "" + System.Numerics.BitOperations.Log2(1024u) + "" "" + System.Numerics.BitOperations.IsPow2(64u) + "" "" + System.Numerics.BitOperations.IsPow2(0u)
  + "" "" + System.Numerics.BitOperations.RotateLeft(1u, 31) + "" "" + System.Numerics.BitOperations.RotateRight(1u, 1));");
    }

    [TestMethod]
    public void TestHexStrings() {
      SameAsNet.Prints(@"
var bytes = new byte[] { 0, 10, 255 };
Console.WriteLine(Convert.ToHexString(bytes) + "" "" + Convert.ToHexString(bytes, 1, 2) + "" "" + Convert.ToHexStringLower(bytes) + "" ["" + Convert.ToHexString(new byte[0]) + ""]"");
var back = Convert.FromHexString(""0aFF"");
Console.WriteLine(string.Join("","", back) + "" "" + back.Length + "" "" + Convert.FromHexString("""").Length);");
    }

    [TestMethod]
    public void TestBase64() {
      SameAsNet.Prints(@"
Console.WriteLine(Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 }) + "" "" + Convert.ToBase64String(new byte[] { 1 }) + "" "" + Convert.ToBase64String(new byte[] { 1, 2 }) + "" ["" + Convert.ToBase64String(new byte[0]) + ""]"");
var back = Convert.FromBase64String(""AQIDBAU="");
Console.WriteLine(string.Join("","", back) + "" "" + back.Length + "" "" + string.Join("","", Convert.FromBase64String(""/+8="")));");
    }

    [TestMethod]
    public void TestHashCode() {
      SameAsNet.Prints(@"
var hash = new HashCode();
hash.Add(1);
hash.Add(""two"");
Console.WriteLine((HashCode.Combine(1, ""two"") == hash.ToHashCode()) + "" "" + (HashCode.Combine(1, 2) == HashCode.Combine(1, 2)) + "" "" + (HashCode.Combine(1, 2) != HashCode.Combine(2, 1))
  + "" "" + (HashCode.Combine(1) == HashCode.Combine(1)) + "" "" + (HashCode.Combine(1, 2, 3, 4, 5, 6, 7, 8) == HashCode.Combine(1, 2, 3, 4, 5, 6, 7, 8)));");
    }
  }
}
