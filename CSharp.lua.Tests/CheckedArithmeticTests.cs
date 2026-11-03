using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// checked never threw for an int: checked(int.MaxValue + 1) gave 2147483648 here and wraps in a game, whose Lua has
  /// 32-bit integers. In a checked context +, -, *, unary -, ++, -- and a compound assignment of ints now call the core's
  /// System.checkedAdd, checkedSub, checkedMul or checkedNeg, which throw an OverflowException both when the result is
  /// outside an int's range (64-bit integers, as here) and when its sign shows a 32-bit wrap (the game's; not run here).
  /// The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class CheckedArithmeticTests {
    [TestMethod]
    public void TestCheckedIntArithmeticThrows() {
      var output = GameScript.Run("CheckedArithmetic", "Program");
      Assert.AreEqual(
        "Add: overflow\n"
        + "AddFits: 2147483647\n"
        + "Subtract: overflow\n"
        + "Multiply: overflow\n"
        + "MultiplyFits: 2000000000\n"
        + "MultiplyNegative: overflow\n"
        + "Negate: overflow\n"
        + "Block: overflow\n"
        + "Compound: overflow\n"
        + "Cast: overflow\n",
        output);
    }
  }
}
