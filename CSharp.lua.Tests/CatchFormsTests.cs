using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A catch that missed. Some Lua errors became a plain Exception where C# throws a typed one: the length of a null
  /// string, a call of a null delegate and % by zero; and an array index out of range threw ArgumentOutOfRangeException,
  /// a List's, where an array throws IndexOutOfRangeException. A catch filter's pattern variable (when (e is X x)) was
  /// local to the filter's function, so the catch's block read a global. The expected lines are what the sample prints
  /// on .NET.
  /// </summary>
  [TestClass]
  public class CatchFormsTests {
    [TestMethod]
    public void TestEachErrorIsCaughtByItsType() {
      var output = GameScript.Run("CatchForms", "Program", "Box");
      Assert.AreEqual(
        "Field: null\n"
        + "Method: null\n"
        + "Length: null\n"
        + "Delegate: null\n"
        + "ArrayRead: index\n"
        + "ArrayWrite: index\n"
        + "ListRead: argument\n"
        + "Divide: divide\n"
        + "Modulo: divide\n"
        + "filter: ioe\n",
        output);
    }
  }
}
