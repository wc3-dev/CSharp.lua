using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// C# gives a switch's sections one scope, so a local function declared in one section can be called from another one or
  /// from a case label's when clause; in Lua each section is a block of its own, so such a call read a global of the
  /// function's name: nil, or a native of that name. A local function used outside its section is now declared above the
  /// sections, with the local functions it calls; one that uses a local declared in a section is refused, as that local is
  /// not seen above the sections either. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class LocalFunctionsAcrossSwitchSectionsTests {
    [TestMethod]
    public void TestALocalFunctionCanBeCalledFromAnotherSection() {
      var output = GameScript.Run("LocalFunctionsAcrossSwitchSections", "Program");
      Assert.AreEqual("zero 2, one 10, other 6\nA! B! C!D!\nzero small big\n", output);
    }

    [TestMethod]
    public void TestAMovedFunctionUsingALocalOfASectionIsRefused() {
      const string code = @"
class Program {
  static int Run(int k) {
    switch (k) {
      case 0:
        int m = 3;
        int AddM(int x) => x + m;
        return AddM(1);
      default:
        m = 4;
        return AddM(2);
    }
  }

  public static void Main() {
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      StringAssert.Contains(e.Message, "the local function 'AddM' is used outside its switch section and uses 'm'");
    }
  }
}
