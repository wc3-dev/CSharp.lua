using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A struct declared with no value and set field by field (P p; p.X = 1;) needs its default in Lua, where the local is
  /// nil until given one. The compiler gave it only to one in a method's own top-level statements, so in an if, a loop or
  /// a switch section p.X stopped the script. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class StructDefaultTests {
    private const string Members = @"
struct P { public int X; public int Y; }

static class Uses {
  public static int InIf(bool b) { if (b) { P q; q.X = 2; return q.X; } return 0; }
  public static int InSection(int k) { switch (k) { case 0: P p; p.X = 1; p.Y = 2; return p.X + p.Y; default: return 0; } }
  public static int AcrossSections(int k) { switch (k) { case 0: P p; return 0; default: p.X = 5; return p.X; } }
  public static int InLoop() { var total = 0; for (int i = 0; i < 3; i++) { P p; p.X = i; total += p.X; } return total; }
  public static int NestedFirstUse(bool b) { P p; if (b) { p.X = 3; } else { p = new P { X = 4 }; } return p.X; }
  public static int WholeFirst() { P p; p = new P { X = 6 }; p.Y = 1; return p.X + p.Y; }
  public static int TopLevel() { P p; p.X = 7; return p.X; }
  public static int Captured() { Action a = () => Run(); P p; p.X = 8; p.Y = 1; a(); return p.X; void Run() { p.X = 9; } }
}
";

    [TestMethod]
    public void TestAStructSetFieldByFieldHasItsDefault() {
      SameAsNet.Prints(@"
Console.WriteLine(Uses.InIf(true) + "" "" + Uses.InIf(false) + "" "" + Uses.InSection(0) + "" "" + Uses.AcrossSections(1) + "" "" + Uses.InLoop());
Console.WriteLine(Uses.NestedFirstUse(true) + "" "" + Uses.NestedFirstUse(false) + "" "" + Uses.WholeFirst() + "" "" + Uses.TopLevel() + "" "" + Uses.Captured());", Members);
    }
  }
}
