using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// String calls the core got wrong. Its escape of a text for Lua's patterns left out - and the other pattern characters,
  /// so s.Split("--") matched nothing over and over until memory ran out, and IndexOf("a+b") found other text.
  /// Split(string, options) and IndexOf(..., StringComparison) reached the core with the options as a count and the
  /// comparison as a start index; StartsWith and EndsWith took no char and no comparison. And in a game's script, which
  /// has no debug library, a string had no element type, so most LINQ over a string (Reverse, Where, Take, OrderBy...)
  /// stopped. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class StringCallsTests {
    [TestMethod]
    public void TestStringCalls() {
      var output = GameScript.Run("StringCalls", "Program");
      Assert.AreEqual(
        "SplitString: a|b|c\n"
        + "SplitStringOptions: a|b,|c\n"
        + "SplitStrings: a|b|c\n"
        + "SplitStringCount: a|b--c\n"
        + "SplitDot: 1|5|2\n"
        + "StartsWithChar: True False False\n"
        + "EndsWithChar: True False False\n"
        + "IndexOfIgnoreCase: 2 -1\n"
        + "IndexOfFrom: 6\n"
        + "IndexOfOrdinal: 2 -1\n"
        + "IndexOfSign: 3 1 2\n"
        + "ContainsSign: False True\n"
        + "StartsWithIgnoreCase: True False\n"
        + "EndsWithIgnoreCase: True False\n"
        + "Reverse: cba\n"
        + "Where: a,c\n"
        + "Take: a,b\n"
        + "Distinct: a,b\n"
        + "OrderBy: a,b,c\n"
        + "Concat: a,b,c,d\n"
        + "ToList: 3\n",
        output);
    }
  }
}
