using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Three tuple forms went wrong. var (a, b) = (1, 2) gave a the tuple: the right side was written as values only when the
  /// left was a tuple of declarations, not var (a, b). A tuple of elements or properties on the left,
  /// (items[0], items[2]) = (items[2], items[0]), was written items:get(0), items:get(2) = ..., which does not load; the
  /// values are now read first and each target set after. A tuple as text failed: the core wrote each item with
  /// v:ToString(), which a number has only with the debug library; it now writes them as the rest of the core's text does.
  /// The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class TupleFormsTests {
    [TestMethod]
    public void TestTupleForms() {
      var output = GameScript.Run("TupleForms", "Program", "Holder");
      Assert.AreEqual(
        "3\n"
        + "x 2.5\n"
        + "20\n"
        + "3 1\n"
        + "1 2 9\n"
        + "(1, one)\n"
        + "pair (1, one)\n"
        + "(1, one)\n"
        + "(3, c)\n"
        + "(7, (n, True))\n"
        + "(1.5, z)\n",
        output);
    }
  }
}
