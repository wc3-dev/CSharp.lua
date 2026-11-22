using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Lua's .. binds tighter than its bitwise operators, comparisons and logic, so a hole such as {a &amp; b} written bare
  /// read as ("" .. a) &amp; b and stopped the script. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class InterpolatedOperatorTests {
    [TestMethod]
    public void TestAnOperatorInAHoleKeepsItsBrackets() {
      SameAsNet.Prints(@"
int a = 6, b = 3;
int? none = null;
var yes = a > b;
uint u = 12;
Console.WriteLine($""{a & b}"");
Console.WriteLine($""{a | b}"");
Console.WriteLine($""{a ^ b}"");
Console.WriteLine($""{a << 1}"");
Console.WriteLine($""{a >> 1}"");
Console.WriteLine($""{u >> 1}"");
Console.WriteLine($""{~a}"");
Console.WriteLine($""x{a & b}y{a << 1}z"");
Console.WriteLine($""x{(yes ? a : b)}y{none ?? 5}z"");
Console.WriteLine($""{a & b:D3}|{a | b,4}"");
Console.WriteLine($""x{a > b}y"");
Console.WriteLine($""x{yes && a == b}y"");
Console.WriteLine($""x{a + b * 2}y"");");
    }
  }
}
