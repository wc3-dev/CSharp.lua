using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// After a using declaration, the rest of its block is a function given to System.using, and a break or a continue there
  /// of a loop around the declaration was written as a plain break, which Lua refuses inside a function: the script did not
  /// load. Now such a break or continue leaves the function with false, and the loop is left or continued after Dispose,
  /// as for one inside a using statement or a try. The caller of such a function passes it on the same way when it is
  /// itself inside another one within the loop, which also fixes a try inside a try or after a using declaration, whose
  /// break did not load either. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class UsingDeclarationLoopControlTests {
    [TestMethod]
    public void TestBreakAndContinueAfterAUsingDeclaration() {
      var output = GameScript.Run("UsingDeclarationLoopControl", "Program", "Res");
      Assert.AreEqual(
        "for: f0 -f0 -f1 f2 -f2 -f3\n"
        + "while: w1 -w1 -w2 -w3\n"
        + "foreach: -a b -b -c\n"
        + "do: -d1 -d2\n"
        + "two: x0 -b0 -a0 x1 -b1 -a1\n"
        + "inner: i0 -i0 i1 -i1\n"
        + "nested: -n00 -n01 -o0 -n10 -n11 o1 -o1\n"
        + "switch: -s0 s1 -s1 d s2 -s2\n"
        + "return: -r0 -r1 -r2 returned 2\n"
        + "in try: -t0 finally0 -t1 finally1\n"
        + "try after: finally0 -u0 finally1 -u1\n"
        + "try in try: inner0 outer0 inner1 outer1\n",
        output);
    }
  }
}
