using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A const string local longer than 27 characters is kept as a local and read by its name, but it was declared only when
  /// its value was one literal: const string L = Prefix + "..."; was named and never declared, so it read nil. Whether it is
  /// declared now goes by its value, as reading it does. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class LongConstStringLocalTests {
    [TestMethod]
    public void TestALongConstStringLocalIsDeclared() {
      var output = GameScript.Run("LongConstStringLocal", "Program");
      Assert.AreEqual(
        "abc0123456789012345678901234567890\n"
        + "0123456789012345678901234567890\n"
        + "abcdef\n"
        + "35 abc0123456789012345678901234567890!\n"
        + "abc0123456789012345678901234567890\n",
        output);
    }
  }
}
