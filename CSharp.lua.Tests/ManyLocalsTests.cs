using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Lua allows 200 locals in a function, so the members of a class past about 195 (methods and private static fields)
  /// are kept in a table instead of as locals. A private static field with an initializer kept there was still read
  /// and written by its bare name, a global, so it was nil; one with a constant or no initializer stayed a local, so
  /// enough of them stopped the script loading. Each is now kept in the table and read from it. The expected lines are
  /// what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class ManyLocalsTests {
    [TestMethod]
    public void TestAPrivateStaticFieldPastTheLimitIsReadFromItsTable() {
      var output = GameScript.Run("ManyLocals", "Program");
      Assert.AreEqual("1 7 n 0 199 1 0 1 7 15\n", output);
    }

    [TestMethod]
    public void TestNoGlobalIsWritten() {
      var lua = GameLuaOutput.Compile("ManyLocals");
      var output = CoreSystemLua.Run(lua + @"
System.init({ types = { ""Program"" } })
Program.Main()
local found = {}
for _, name in ipairs({ ""ids"", ""count"", ""name"", ""zero"" }) do
  if rawget(_G, name) ~= nil then
    found[#found + 1] = name
  end
end
print(""globals: "" .. table.concat(found, "" ""))
");
      StringAssert.EndsWith(output, "globals: \n");
    }
  }
}
