using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A name declared for a member inside a property pattern ({ X: var x }, { Inner: { Y: int y } }) was refused: Lua has no
  /// assignment inside a condition, and a switch's arms are one if chain. The member is now read into a local before the
  /// condition, behind the checks the pattern makes on the way to it (not nil, of the pattern's type), so the name holds
  /// what C# gives it, also after the member changes. A getter there runs even where an earlier arm matched (Sven's choice,
  /// 11-03). In a loop's condition, which runs again each time, it stays refused. The expected lines are what the sample
  /// prints on .NET.
  /// </summary>
  [TestClass]
  public class PropertyPatternNamesTests {
    [TestMethod]
    public void TestANameInAPropertyPatternHoldsTheMember() {
      var output = GameScript.Run("PropertyPatternNames", "Program", "Point", "Box");
      Assert.AreEqual(
        "on the axis at 3, inner 2 8, y 4, null\n"
        + "far 9, inner x 6, other, other\n"
        + "is 1 2, is not, is not\n"
        + "kept 5\n"
        + "box 4, not a box, not a box\n",
        output);
    }

    [TestMethod]
    public void TestANameInALoopsConditionIsRefused() {
      const string code = @"
class Node {
  public Node Next;
}

class Program {
  static int Count(Node node) {
    int count = 0;
    while (node is { Next: var next }) {
      count++;
      node = next;
    }
    return count;
  }

  public static void Main() {
  }
}";
      var e = Assert.ThrowsException<CompilationErrorException>(() => Compiler.CompileSingleCodeForGame(code));
      StringAssert.Contains(e.Message, "a loop's condition");
    }
  }
}
