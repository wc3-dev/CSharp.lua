using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// An integer constant of -2^31 (int.MinValue, 1 &lt;&lt; 31, a long of that value) was written as the literal -2147483648.
  /// The game's Lua has 32-bit integers: 2147483648 fits none, so it is read as a float before the minus, and a bitwise
  /// operator on it stops the script ("number has no integer representation"; seen 11-04). Desktop Lua has 64-bit integers
  /// and reads it as one, so only the written text shows it. It is now written (-2147483647 - 1), an integer on both, as
  /// long.MinValue already was for Lua 5.4.
  /// </summary>
  [TestClass]
  public class IntMinimumTests {
    [TestMethod]
    public void TestTheIntMinimumIsNoLiteralTheGameReadsAsAFloat() {
      var lua = GameLuaOutput.Compile("IntMinimum");
      Assert.IsFalse(Regex.IsMatch(lua, @"(?<![\d.])2147483648(?!\d)"), lua);
      Assert.IsTrue(lua.Contains("(-2147483647 - 1)"), lua);
    }

    [TestMethod]
    public void TestTheIntMinimumKeepsItsValue() {
      var output = GameScript.Run("IntMinimum", "K", "Bits", "Program");
      Assert.AreEqual("-2147483648\n-2147483647 2147483647 -1\n-2147483647\n2147483647\n0\n-2147483648\nTrue\n-2147483648\n-2147483648\n-2147483647\n-2147483647 -2147483648 Sign Least\nlowest\n", output);
    }
  }
}
