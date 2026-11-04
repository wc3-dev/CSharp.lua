using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// The compiler writes some values with and/or (a nullable's arithmetic as none and none + 2, a nullable's comparison as
  /// x and x > 0 or false), and Lua's and/or bind looser than its other operators, or looser than and. Under a tighter
  /// operator such a value lost its grouping: none + 2 == null was none and (none + 2 == nil), nil when none is null, and
  /// some + 1 > 0 &amp;&amp; no was true. Such a value is now written in parentheses there. The expected lines are what the sample
  /// prints on .NET.
  /// </summary>
  [TestClass]
  public class NullableArithmeticNullTests {
    [TestMethod]
    public void TestALogicValueKeepsItsGroupingUnderATighterOperator() {
      var output = GameScript.Run("NullableArithmeticNull", "Program");
      Assert.AreEqual("True\nFalse\nTrue\nFalse\nTrue\nTrue\nTrue\nFalse\nFalse\nTrue\nif\n", output);
    }
  }
}
