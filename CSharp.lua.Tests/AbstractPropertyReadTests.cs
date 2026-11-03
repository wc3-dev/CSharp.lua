using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// An abstract property, or a virtual auto-property, read or set by its bare name or through <c>this.</c> in the class
  /// that declares it was compiled as its field (<c>this.Kind</c>), so an override was never asked and an abstract one
  /// read nil. It is now read and set through its accessors, as a read through another receiver always was.
  /// </summary>
  [TestClass]
  public class AbstractPropertyReadTests {
    [TestMethod]
    public void TestAnOverriddenPropertyIsReadThroughItsAccessors() {
      var output = GameScript.Run("AbstractPropertyReads", "Program", "Shape", "Square", "Plain");
      Assert.AreEqual("square square 4 4 red 50\nplain plain 0 0 grey 5\n", output);
    }
  }
}
