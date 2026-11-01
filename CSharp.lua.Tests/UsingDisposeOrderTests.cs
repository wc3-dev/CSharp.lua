using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// System.usingX takes two variables of one using, or two using declarations next to each other, in one call. It disposed
  /// them first to last, where C# goes last to first, and a Dispose that threw left the ones after it undisposed. It now does
  /// as nested usings do: last to first, every value disposed, and the last failure the one that is thrown. The expected lines
  /// are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class UsingDisposeOrderTests {
    [TestMethod]
    public void TestUsingsInOneCallAreDisposedLastToFirst() {
      var output = GameScript.Run("UsingDisposeOrder", "Program", "Res");
      Assert.AreEqual(
        "one declaration\ndispose b\ndispose a\n"
        + "one statement\ndispose b\ndispose a\n"
        + "next to each other\ndispose b\ndispose a\n"
        + "three\ndispose c\ndispose b\ndispose a\n"
        + "dispose b\ndispose a\n7\n"
        + "no first\ndispose b\n"
        + "no last\ndispose a\n"
        + "dispose b\ndispose a\ncaught from body\n"
        + "last fails\ndispose b\ndispose a\ncaught from b\n"
        + "first fails\ndispose b\ndispose a\ncaught from a\n"
        + "both fail\ndispose b\ndispose a\ncaught from a\n"
        + "dispose b\ndispose a\ncaught from b\n",
        output);
    }
  }
}
