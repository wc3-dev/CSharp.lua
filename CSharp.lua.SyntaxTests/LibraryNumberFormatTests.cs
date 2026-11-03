using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Number formats, standard and custom, as .NET writes them in the invariant culture (decision 120): each prints in
  /// Lua what it prints on .NET. A float's standard format rounds its exact value, a tie to even; an int's, and every
  /// custom format, round half away from zero.
  /// </summary>
  [TestClass]
  public class LibraryNumberFormatTests {
    [TestMethod]
    public void TestStandardFormatsOfFloats() {
      SameAsNet.Prints(@"
foreach (var x in new[] { 1234.5678, 0.125, 2.5, -0.001, 0.0001234, -1234.5, 0.0, 1e-7, 12345678.9 }) {
  Console.WriteLine(x.ToString(""F"") + "" "" + x.ToString(""F0"") + "" "" + x.ToString(""F3"") + "" "" + x.ToString(""F10"") + "" | ""
    + x.ToString(""N"") + "" "" + x.ToString(""N0"") + "" "" + x.ToString(""n1"") + "" | ""
    + x.ToString(""P"") + "" "" + x.ToString(""P0"") + "" "" + x.ToString(""p1"") + "" | ""
    + x.ToString(""E"") + "" "" + x.ToString(""E2"") + "" "" + x.ToString(""e0"") + "" | ""
    + x.ToString(""G3"") + "" "" + x.ToString(""G5"") + "" "" + x.ToString(""g2""));
}");
    }

    [TestMethod]
    public void TestStandardFormatsOfInts() {
      SameAsNet.Prints(@"
foreach (var i in new[] { 0, 5, -5, 255, -1, 1235, 1225, 25, -1234567, int.MaxValue, int.MinValue }) {
  Console.WriteLine(i.ToString(""D"") + "" "" + i.ToString(""D3"") + "" "" + i.ToString(""X"") + "" "" + i.ToString(""x4"") + "" | ""
    + i.ToString(""N"") + "" "" + i.ToString(""N0"") + "" "" + i.ToString(""F2"") + "" "" + i.ToString(""P0"") + "" | ""
    + i.ToString(""E2"") + "" "" + i.ToString(""G1"") + "" "" + i.ToString(""G3"") + "" "" + i.ToString(""G""));
}");
    }

    [TestMethod]
    public void TestCustomFormats() {
      SameAsNet.Prints(@"
foreach (var x in new[] { 0.25, 1234567.891, 5.0, 0.5, -0.4, 0.004, -0.004, 0.0, 12345.0, -1234.5, 1.5, 0.0015 }) {
  Console.WriteLine(x.ToString(""0.0"") + "" "" + x.ToString(""00"") + "" "" + x.ToString(""#,##0"") + "" "" + x.ToString(""#,##0.00"") + "" ""
    + x.ToString(""0.##"") + "" "" + x.ToString(""#.##"") + "" "" + x.ToString(""#,##0,,"") + "" "" + x.ToString(""#,#.0"") + "" | ""
    + x.ToString(""0.0%"") + "" "" + x.ToString(""0.0\x2030"") + "" "" + x.ToString(""0.00E+00"") + "" "" + x.ToString(""0.#E-0"") + "" "" + x.ToString(""0.0e+00"") + "" | ""
    + x.ToString(""'n='0"") + "" "" + x.ToString(""\\#0"") + "" "" + x.ToString(""0 \""ab\"" 0"") + "" "" + x.ToString(""00-00"") + "" "" + x.ToString(""#"") + "" | ""
    + x.ToString(""0;(0);zero"") + "" "" + x.ToString(""0.00;neg;zero"") + "" "" + x.ToString(""0.00;neg"") + "" "" + x.ToString(""#,##0.00;(#,##0.00)"") + "" "" + x.ToString(""0;;none"") + "" "" + x.ToString(""0000.##""));
}
foreach (var i in new[] { 0, 7, -7, 1234567, 25 }) {
  Console.WriteLine(i.ToString(""00"") + "" "" + i.ToString(""#,##0"") + "" "" + i.ToString(""0.0"") + "" "" + i.ToString(""#,##0,"") + "" "" + i.ToString(""0%"") + "" "" + i.ToString(""0.0E+0"") + "" "" + i.ToString(""0;minus;zero"") + "" "" + i.ToString(""abc""));
}");
    }

    [TestMethod]
    public void TestFormatsInText() {
      SameAsNet.Prints(@"
var gold = 1234567;
var rate = 0.256;
var speed = 3.5f;
Console.WriteLine($""{gold:N0} {rate:P1} {speed:F2} {gold:#,##0} {rate:0.00}"");
Console.WriteLine($""[{speed,8:F1}] [{gold,-12:N0}]"");
Console.WriteLine(string.Format(""{0:N2} {1:00} {2:P0}"", 1234.5, 7, 0.5));
Console.WriteLine(new StringBuilder().AppendFormat(""{0:F3}"", 2.0).ToString());");
    }

    [TestMethod]
    public void TestDOfAFloatIsAFormatError() {
      SameAsNet.Prints(@"
try {
  Console.WriteLine((1.5).ToString(""D2""));
} catch (FormatException) {
  Console.WriteLine(""FormatException"");
}");
    }
  }
}
