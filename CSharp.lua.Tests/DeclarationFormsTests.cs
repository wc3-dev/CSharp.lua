using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Declarations the compiler wrote wrong. A user-defined ++ or -- was written x = x + 1, arithmetic on a table; it now
  /// calls the operator, on a local, a field, a property and an element, before or after. A record deriving from a record
  /// called its == as op_Equality1, a name nothing has: a record's own == and != are the core's, which every record
  /// inherits under their plain names. with on an anonymous type called a __clone__ the core's anonymous type did not
  /// have. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class DeclarationFormsTests {
    [TestMethod]
    public void TestDeclarationForms() {
      var output = GameScript.Run("DeclarationForms", "Program", "Counter", "Vec", "Holder", "Base", "Derived", "Third");
      Assert.AreEqual(
        "Increment: 5 3 5\n"
        + "Decrement: 3\n"
        + "StructIncrement: 11\n"
        + "Members: 1 2 1\n"
        + "DerivedRecord: True True True\n"
        + "AnonymousWith: 1 2 a\n",
        output);
    }
  }
}
