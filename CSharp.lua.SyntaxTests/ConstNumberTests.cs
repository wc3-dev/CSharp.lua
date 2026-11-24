using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A const float was written as its nearest nine digits (0.6f as 0.600000024) while the literal 0.6f is written 0.6,
  /// so naming a number changed the sums it was in. A const is written as a literal of its value is.
  /// </summary>
  [TestClass]
  public class ConstNumberTests {
    private const string Members = @"
static class Sizes {
  public const float GameHeight = 0.6f;
  public const float Small = 0.139f;
  public const float Twice = 0.3f * 2f;
  public const float Whole = 768f;
  public const double Tenth = 0.1;
  public const double Third = 1.0 / 3.0;
}
";

    [TestMethod]
    public void TestAConstFloatIsTheNumberItsLiteralIs() {
      SameAsNet.Prints(@"
float pixels = 100f;
Console.WriteLine(pixels * Sizes.GameHeight / 768f == pixels * 0.6f / 768f);
Console.WriteLine(pixels * Sizes.Small == pixels * 0.139f);
Console.WriteLine(pixels * Sizes.Whole == pixels * 768f);
const float local = 0.4f;
Console.WriteLine(pixels * local == pixels * 0.4f);", Members);
    }

    [TestMethod]
    public void TestAConstFloatFromAnExpressionIsTheNumberOfItsValuesLiteral() {
      SameAsNet.Prints(@"
float pixels = 100f;
Console.WriteLine(pixels * Sizes.Twice == pixels * 0.6f);", Members);
    }

    [TestMethod]
    public void TestAConstDoubleIsTheNumberItsLiteralIs() {
      SameAsNet.Prints(@"
double x = 7;
Console.WriteLine(x * Sizes.Tenth == x * 0.1);
Console.WriteLine(x * Sizes.Third == x * (1.0 / 3.0));", Members);
    }
  }
}
