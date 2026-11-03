using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A class marked @CSharpLua.Ignore is given by someone else: in a Warcraft III map, a type of the game's handles, whose
  /// values are the game's userdata. A cast to such a class was written System.cast(unit, x), which checks the value's
  /// CSharp.lua class, so a handle failed every cast to its own type. Such a cast, also the one a foreach makes for each
  /// item, is now written with no check. Here io.stdout, a userdata, stands in for a handle.
  /// </summary>
  [TestClass]
  public class IgnoredTypeCastsTests {
    [TestMethod]
    public void TestACastToAnIgnoredClassChecksNothing() {
      var output = GameScript.Run("IgnoredTypeCasts", "Program");
      Assert.AreEqual("userdata\nuserdata\nuserdata\nTrue\n", output);
    }

    [TestMethod]
    public void TestNoCastIsWritten() {
      var lua = GameLuaOutput.Compile("IgnoredTypeCasts");
      Assert.IsFalse(lua.Contains("System.cast"), lua);
    }
  }
}
