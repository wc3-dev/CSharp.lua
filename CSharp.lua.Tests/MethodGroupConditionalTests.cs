using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A conditional whose first branch is a method group read through its class or an object (yes ? Reactions.OnMoved :
  /// null, yes ? o.Run : null) stopped the compiler with a NullReferenceException: a method group has no type, and the
  /// check whether the branch may be nil read the type. A method group by its bare name was right. A method group, a lambda
  /// and an anonymous method are never nil, so the conditional is written with and/or. The expected lines are what the
  /// sample prints on .NET.
  /// </summary>
  [TestClass]
  public class MethodGroupConditionalTests {
    [TestMethod]
    public void TestAMethodGroupInAConditionalIsADelegate() {
      var output = GameScript.Run("MethodGroupConditional", "Program", "Reactions", "Other");
      Assert.AreEqual("moved\nrun\nhello\n8\nTrue\n2\nanonymous\n", output);
    }

    [TestMethod]
    public void TestTheConditionalIsWrittenWithAndOr() {
      var lua = GameLuaOutput.Compile("MethodGroupConditional");
      Assert.IsTrue(lua.Contains("local a = yes and Reactions.OnMoved or nil"), lua);
    }
  }
}
