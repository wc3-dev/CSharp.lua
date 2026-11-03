using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A call with an out argument in a branch of a conditional (cond ? a : d.TryGetValue(k, out var v) ? v : b) was
  /// written before the conditional, so it ran when its branch was not taken: a null key threw though cond was true.
  /// Such a conditional is now an if, as one whose first branch may be nil already was. An out variable declared in a
  /// part that runs only sometimes (a branch, or the right of &&, || or ??) is declared at the function's head, as the
  /// right of && and || already had it, so code after the statement still sees it. The expected lines are what the
  /// sample prints on .NET.
  /// </summary>
  [TestClass]
  public class OutInConditionalTests {
    [TestMethod]
    public void TestAnOutCallRunsOnlyWhenItsBranchIsTaken() {
      var output = GameScript.Run("OutInConditional", "Program");
      Assert.AreEqual("5 1 7\n6 1 8\n5 4 1 4\nk 9 x1 9\n111\n", output);
    }

    [TestMethod]
    public void TestNoGlobalIsWritten() {
      var lua = GameLuaOutput.Compile("OutInConditional");
      var output = CoreSystemLua.Run(lua + @"
System.init({ types = { ""Program"" } })
Program.Main()
local found = {}
for _, name in ipairs({ ""v"", ""w"", ""later"", ""found"", ""n"" }) do
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
