using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A struct that can change, given back through an out argument by a method of the core (d.TryGetValue(k, out var c)),
  /// was the collection's own value, not a copy: c.X = 10 changed d[k] too. It is now copied after the call, into a local,
  /// a field, a parameter or an element, as a struct read from a collection already was. The expected lines are what the
  /// sample prints on .NET.
  /// </summary>
  [TestClass]
  public class OutStructCopiesTests {
    [TestMethod]
    public void TestAStructGivenBackByTheCoreIsACopy() {
      var output = GameScript.Run("OutStructCopies", "Program", "P");
      Assert.AreEqual("1 10\n1 20\n1 30\n1 40\n1 50\n1 60\n2 70\n", output);
    }
  }
}
