using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A positional pattern in a switch reads the value's Deconstruct into locals before the arms are tested, and it did so
  /// for any value: a class, a record or a nullable struct given null stopped on "attempt to index a nil value" in
  /// Deconstruct, and a positional pattern with a type (Pair (3, 4)) on an object called Deconstruct on a value of another
  /// type. Now a value that can be null is deconstructed only when it is not, and an arm with a type only when the value is
  /// of it, each such arm with its own locals. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class PositionalPatternNullTests {
    [TestMethod]
    public void TestAPositionalPatternIsFalseForNull() {
      var output = GameScript.Run("PositionalPatternNull", "Program", "Pair", "Point", "Cell");
      Assert.AreEqual(
        "one-two null other\n"
        + "one-two null other\n"
        + "y 5 null other\n"
        + "one null other\n"
        + "pair null other\n"
        + "pair point null other other\n",
        output);
    }
  }
}
