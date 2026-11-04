using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// foreach over a Span or a ReadOnlySpan goes through its items, a slice's only: the core's spans had no GetEnumerator,
  /// so the loop stopped the script. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class SpanForeachTests {
    [TestMethod]
    public void TestForeachOverASpan() {
      SameAsNet.Prints(@"
int[] items = { 1, 2, 3, 4 };
Span<int> all = items;
var text = """";
foreach (var item in all) { text += item; }
foreach (var item in all.Slice(1, 2)) { text += "" "" + item; }
ReadOnlySpan<int> read = items;
foreach (var item in read.Slice(2)) { text += "" r"" + item; }
foreach (var item in all.Slice(0, 0)) { text += "" never""; }
Console.WriteLine(text);");
    }
  }
}
