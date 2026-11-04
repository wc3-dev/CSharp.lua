using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using CSharpLua.CoreSystem;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// The game's Lua has 32-bit integers and reads the literal -2147483648 as a float (IntMinimumTests). CoreSystem's own Lua
  /// gave that float as a value: System.ToInt32's fallback (a float out of range or NaN cast to int), Math.ILogB(0), and
  /// Environment.TickCount, which took the time modulo 2147483648. They are now (-2147483647 - 1) and a mask with
  /// 0x7fffffff, integers on both Lua. Desktop Lua reads the literal as an integer, so only the text shows it.
  /// </summary>
  [TestClass]
  public class CoreIntMinimumTests {
    [TestMethod]
    public void TestCoreSystemOnlyComparesWithTheLiteral() {
      foreach (var file in CoreSystemProvider.GetCoreSystemFiles().Concat(Directory.GetFiles(Path.GetDirectoryName(CoreSystemProvider.GetCoreSystemFiles().First()), "*.lua", SearchOption.AllDirectories)).Distinct()) {
        var lua = Regex.Replace(File.ReadAllText(file), @"--\[\[.*?\]\]|--[^\n]*|""(?:[^""\\\n]|\\.)*""", "", RegexOptions.Singleline);
        // A comparison or a limit handed to a function that compares with it.
        var made = Regex.Match(lua, @"(?<!(?:[<>]=?|[=~]=|,)\s*-?)(?<![\d.])2147483648(?![\d.])");
        Assert.IsFalse(made.Success, file + ": " + (made.Success ? lua.Substring(System.Math.Max(0, made.Index - 80), 120) : ""));
      }
    }

    [TestMethod]
    public void TestTheValuesStayIntegers() {
      var output = CoreSystemLua.Run("print(System.ToInt32(0 / 0), System.Math.ILogB(0), math.type(System.Environment.getTickCount()), System.Environment.getTickCount() >= 0)");
      Assert.AreEqual("-2147483648\t-2147483648\tinteger\ttrue\n", output);
    }
  }
}
