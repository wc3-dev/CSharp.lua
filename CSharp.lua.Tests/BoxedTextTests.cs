using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A char or an enum given as an object to a method that writes it as text (String.Format, Concat and Join,
  /// Console.Write and WriteLine, StringBuilder's Append, AppendFormat and Insert) was written as its number: in Lua both
  /// are numbers, and the object keeps no type. Such an argument now goes in a box of the core whose ToString writes it as
  /// .NET does. An object variable holding one is left as it was. The expected lines are
  /// what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class BoxedTextTests {
    [TestMethod]
    public void TestACharOrEnumAsAnObjectIsWrittenAsText() {
      var output = GameScript.Run("BoxedText", "Program", "Color");
      Assert.AreEqual("z Blue\nazBlue\nz,Blue,3\nz\nBlue\nz Blue Blue\n", output);
    }
  }
}
