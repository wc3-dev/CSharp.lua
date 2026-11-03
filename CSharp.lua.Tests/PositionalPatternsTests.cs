using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A positional pattern outside a switch: on a value that can be null (p is (1, 2) with a class, a record, a nullable
  /// struct or an object) it was refused, as Deconstruct would run on nil; inside another pattern ({ Pair: (5, 6) }) it
  /// was refused; and with names ((A: 1, B: 2)) it read the names as members. Now the value is deconstructed behind the
  /// same checks a switch makes (not nil, of the pattern's type), a member's own positional pattern behind the checks on
  /// the way to it, and a name is only the position's. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class PositionalPatternsTests {
    [TestMethod]
    public void TestPositionalPatternsOutsideASwitch() {
      var output = GameScript.Run("PositionalPatterns", "Program", "Pair", "Point", "Cell", "Holder");
      Assert.AreEqual(
        "one-two not not\n"
        + "y 5 not not\n"
        + "one not not\n"
        + "pair not not\n"
        + "nested not not\n"
        + "tuple not\n"
        + "named not\n"
        + "named record not not\n",
        output);
    }
  }
}
