using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// An enum as text. A constant that is not one member ((Color)5 + " x", F.A | F.C) stopped the compiler: it took the
  /// constant's member name, and there is none. The core found a value's name in a hash table, so a [Flags] combination
  /// was its number, Enum.GetValues and GetNames came in a new order each run, and ToString("D") or ("X") gave the name;
  /// Enum.Parse took no number, and an interpolation hole's width was dropped. Now a [Flags] enum is marked as one, its
  /// combinations are named as .NET names them, values and names come in .NET's order, and the formats and widths are
  /// .NET's ("X" as eight digits, an int's). Left: Enum.Parse(typeof(Color), ...) gives an object, which in Lua is the bare
  /// number, so it shows the number. The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class EnumTextTests {
    [TestMethod]
    public void TestEnumsAsText() {
      var output = GameScript.Run("EnumText", "Program", "Color", "F");
      Assert.AreEqual(
        "Name: Blue Blue Blue\n"
        + "FromVariable: Green Green\n"
        + "Unknown: 99 Green x\n"
        + "ConstantCast: Green x\n"
        + "FlagsCombination: A, C AB None 8\n"
        + "FlagsZero: None x\n"
        + "Parse: Green Blue A, C\n"
        + "Aligned: [  Blue] [Blue  ] [   A, C]\n"
        + "GetValues: Red;Green;Blue\n"
        + "GetNames: Red;Green;Blue None;A;B;AB;C\n"
        + "Formats: 6 00000006 Blue 5 6\n",
        output);
    }
  }
}
