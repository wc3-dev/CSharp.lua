using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A generic method group given its type argument: in a conditional, a cast, parentheses, an arrow body, a property's
  /// initializer, an array's or a delegate's creation it stopped the compiler (no delegate type for that place); handed to
  /// the core's LINQ it had no type argument (the core passes an index where the type goes); and bound to a delegate of no
  /// or of two or more parameters its type argument took the place of the first missing argument. The expected lines are
  /// what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class GenericMethodGroupsTests {
    [TestMethod]
    public void TestAGenericMethodGroupKeepsItsTypeArgument() {
      var output = GameScript.Run("GenericMethodGroups", "Program", "Groups");
      Assert.AreEqual("0,0,0\n1,2,0\n1,2,3,0\n0\n0\n2:1:0\n0\n0,0,0,0,0,0,0\n", output);
    }
  }
}
