using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A struct that can change, read from an array, a List or a Dictionary (arr[0], list[i], dict[k]), was not copied where
  /// C# copies it: var c = arr[0]; c.X = 6 changed arr[0], and so did passing arr[0] to a method that changes its parameter,
  /// or returning it. It is now copied in the same places a struct read from a variable already was: an initializer, the
  /// right of an assignment, an argument and a return. Changing the element in place (arr[0].X = 4) still changes it. The
  /// expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class StructElementCopiesTests {
    [TestMethod]
    public void TestAStructReadFromAnElementIsACopy() {
      var output = GameScript.Run("StructElementCopies", "Program", "P");
      Assert.AreEqual("1 107\n2 7\n3 4\n50 1\n1 9\n4\n", output);
    }
  }
}
