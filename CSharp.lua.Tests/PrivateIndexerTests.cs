using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A private indexer's accessors are local functions of its class, as a private property's are, but reading or writing
  /// it was written as a method of the object, this:set(0, 1), which is nil: "attempt to call a nil value (method 'set')".
  /// It is now called as a private property is, set(this, 0, 1), on this or on another object of the class. The expected
  /// lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class PrivateIndexerTests {
    [TestMethod]
    public void TestAPrivateIndexerIsCalledAsALocalFunction() {
      var output = GameScript.Run("PrivateIndexer", "Program", "Grid", "Bag");
      Assert.AreEqual("11\nab\n", output);
    }
  }
}
