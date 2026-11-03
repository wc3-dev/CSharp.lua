using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// C# narrows the value of a compound assignment, ++ and -- to a byte, sbyte, short or ushort, so it wraps: byte b = 250;
  /// b += 10; is 4. Lua has no such types, and the value was kept whole: 260. It is now made the type, as a cast to it was
  /// already, for a local, an array element, a field and a property. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class SmallIntegerWrapTests {
    [TestMethod]
    public void TestSmallIntegersWrap() {
      var output = GameScript.Run("SmallIntegerWrap", "Program", "Holder");
      Assert.AreEqual("4\n255\n0\n255\n254\n252\n-126\n-32768\n32767\n-32768\n65535\n3\n1 32767\n255 0\n0 0\n256\n", output);
    }
  }
}
