using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// C#'s >> of an int keeps its sign and takes its count mod 32; Lua's >> shifts in zeros, so -1 >> 1 was 2147483647 in
  /// the game (seen 11-03) and larger still on the desktop. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class ShiftTests {
    [TestMethod]
    public void TestRightShiftOfAnIntKeepsItsSign() {
      SameAsNet.Prints(@"
int a = -1, b = -8, c = int.MinValue, d = 5, e = -5, n = 2;
Console.WriteLine((a >> 1) + "" "" + (b >> 1) + "" "" + (c >> 31) + "" "" + (d >> 31) + "" "" + (e >> 33) + "" "" + (e >> 0) + "" "" + (b >> 3) + "" "" + (-100 >> n));
var f = -64;
f >>= 3;
Console.WriteLine(f);
int? g = -16;
Console.WriteLine(g >> 2);
short h = -4;
Console.WriteLine(h >> 1);
uint u = 0x80000000;
Console.WriteLine((u >> 4) + "" "" + (d >> 1) + "" "" + (1 << 4 >> 2));");
    }
  }
}
