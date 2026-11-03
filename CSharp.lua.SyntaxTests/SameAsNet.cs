using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>Runs a Main on .NET and in Lua, as a map's script is, and asserts that both print the same.</summary>
  internal static class SameAsNet {
    /// <param name="main">Main's body.</param>
    /// <param name="members">Members of Program, or types beside it, that the body uses.</param>
    public static void Prints(string main, string members = "") {
      var code = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\n\n" + members
        + "\nclass Program {\n  public static void Main() {\n" + main + "\n  }\n}\n";
      var net = DotNetRun.Run(code);
      Assert.IsFalse(net.StartsWith("DOES NOT COMPILE"), net);
      Assert.AreEqual(net, LuaRun.Run(code));
    }
  }
}
