using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A ref local of an element, ref int r = ref a[i], was written as the text a:get(i) in place of r, so r = 5 was a Lua
  /// syntax error, and a later change to a or i moved r. The array and the index are now kept in locals where r is made,
  /// and r reads and writes that element. An index out of range throws where r is made. The expected lines are what the
  /// sample prints on .NET.
  /// </summary>
  [TestClass]
  public class RefElementLocalsTests {
    [TestMethod]
    public void TestARefLocalOfAnElementReadsAndWritesIt() {
      var output = GameScript.Run("RefElementLocals", "Program", "Point");
      Assert.AreEqual(
        "Write: 5 5\n"
        + "Compound: 14 14\n"
        + "IndexOnce: 9 3 2\n"
        + "ArrayOnce: 4 7\n"
        + "FromEnd: 1,2,0\n"
        + "RefArgument: 2 2\n"
        + "RefOfRef: 6 6\n"
        + "StructElement: 4 4\n"
        + "StructCopy: 0 9\n"
        + "ReadOnly: 8\n"
        + "Grid: 7 7\n"
        + "Static: 11 11\n"
        + "Loop: 10,20,30\n"
        + "OutOfRange: thrown\n"
        + "Span: 5 5\n",
        output);
    }
  }
}
