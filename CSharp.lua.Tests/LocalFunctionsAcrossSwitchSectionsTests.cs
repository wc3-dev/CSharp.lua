using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// C# gives a switch's sections one scope, so a local function declared in one section can be called from another one or
  /// from a case label's when clause; in Lua each section is a block of its own, so such a call read a global of the
  /// function's name: nil, or a native of that name. A local function used outside its section is now declared above the
  /// sections, with the local functions it calls, and so is a local of a section it uses (SwitchSectionLocalsTests). The
  /// expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class LocalFunctionsAcrossSwitchSectionsTests {
    [TestMethod]
    public void TestALocalFunctionCanBeCalledFromAnotherSection() {
      var output = GameScript.Run("LocalFunctionsAcrossSwitchSections", "Program");
      Assert.AreEqual("zero 2, one 10, other 6\nA! B! C!D!\nzero small big\n", output);
    }
  }
}
