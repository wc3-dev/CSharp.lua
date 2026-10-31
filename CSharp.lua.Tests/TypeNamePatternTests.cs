using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A type's name with no variable after it reads to the parser as a constant, so case Box when x:, Box =>, Box or Other, and
  /// { Inner: Box } were written as an equality with the class, which is never true: the code around them silently did not
  /// run. Each is now written as a type check, as case Box: and Box b were. string =>, which the parser does read as a type,
  /// stopped the compiler in a switch expression's arm, and case T: wrote an equality with a type parameter. b is not Box on a
  /// value already of the type wrote not b ~= nil, which is always true. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class TypeNamePatternTests {
    [TestMethod]
    public void TestATypesNameInAPatternChecksTheType() {
      var output = GameScript.Run("TypeNamePatterns", "Program", "Box", "Crate", "Other", "Goods.Sack", "IThing", "Thing", "Spot", "Shelf");
      Assert.AreEqual(
        "box none box other none\n"
        + "sack list none\n"
        + "alias none\n"
        + "T when none T none\n"
        + "box none\n"
        + "either either none\n"
        + "plain box not other other\n"
        + "holds other holds box or text holds box or text none\n"
        + "box one none other\n"
        + "box box other sack text none\n"
        + "box none text none\n"
        + "box one none other\n"
        + "True True False | True False False | False False True\n"
        + "True False\n"
        + "True True False False\n"
        + "one small many other\n"
        + "limit word limit none word\n"
        + "box none none\n"
        + "False False False box | True True True null\n"
        + "True False | False True\n"
        + "big small big small\n"
        + "thing spot none\n"
        + "thing spot none\n",
        output);
    }
  }
}
