using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// C# gives a switch's sections one scope, so a local declared in one section can be given a value and read in another;
  /// in Lua each section is a block of its own, so the other section wrote a global of the local's name, which read right
  /// but replaced a native of that name. Such a local is now declared above the sections and assigned where C# declares
  /// it. A local declared by an out argument, a deconstruction or a pattern cannot be moved, and is refused when another
  /// section uses it. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class SwitchSectionLocalsTests {
    [TestMethod]
    public void TestALocalOfOneSectionCanBeUsedInAnother() {
      var output = GameScript.Run("SwitchSectionLocals", "Program");
      Assert.AreEqual("5 14 -1\nzero3 one15 other20\n100 1 2\n4 6\n", output);
    }

    [TestMethod]
    public void TestNoGlobalIsWritten() {
      var lua = GameLuaOutput.Compile("SwitchSectionLocals");
      var output = CoreSystemLua.Run(lua + @"
System.init({ types = { ""Program"" } })
Program.Main()
local found = {}
for _, name in ipairs({ ""y"", ""text"", ""a"", ""b"", ""seen"", ""m"" }) do
  if rawget(_G, name) ~= nil then
    found[#found + 1] = name
  end
end
print(""globals: "" .. table.concat(found, "" ""))
");
      StringAssert.EndsWith(output, "globals: \n");
    }

    [TestMethod]
    public void TestAnOutLocalUsedInAnotherSectionIsRefused() {
      const string code = @"
class Program {
  static int Run(int k, string s) {
    switch (k) {
      case 0:
        int.TryParse(s, out var v);
        return v;
      default:
        v = 3;
        return v;
    }
  }

  public static void Main() {
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      StringAssert.Contains(e.Message, "'v' is declared in one switch section and used in another");
    }

    [TestMethod]
    public void TestADeconstructedLocalUsedInAnotherSectionIsRefused() {
      const string code = @"
class Program {
  static int Run(int k) {
    switch (k) {
      case 0:
        var (a, b) = (1, 2);
        return a + b;
      default:
        a = 4;
        return a;
    }
  }

  public static void Main() {
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      StringAssert.Contains(e.Message, "'a' is declared in one switch section and used in another");
    }
  }
}
