using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A switch expression's arm that is a relational pattern (&lt; 0 =&gt;), not, and, a pattern in parentheses or var with one
  /// name stopped the compiler: NotImplementedException, or an InvalidCastException for var n. Each is now written as the
  /// same pattern in an is test is. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class SwitchArmPatternsTests {
    [TestMethod]
    public void TestEveryPatternWorksAtTheTopOfAnArm() {
      var output = GameScript.Run("SwitchArmPatterns", "Program");
      Assert.AreEqual(
        "negative, zero, small, big, medium\n"
        + "one or three, not one or three, one or three\n"
        + "even 4, odd 7\n"
        + "text a, none\n"
        + "not text, long text, other, other\n",
        output);
    }
  }
}
