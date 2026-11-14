using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Two calls giving the same out variable across || or &&, as `a.TryGetValue(k, out v) || b.TryGetValue(k, out v)`:
  /// said (K10, 11-13) to lose the first call's hit. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class OutAcrossLogicalTests {
    private const string Stores = @"
static class Stores {
  public static Dictionary<string, int> A = new Dictionary<string, int> { [""x""] = 1 };
  public static Dictionary<string, int> B = new Dictionary<string, int> { [""y""] = 2 };

  public static string Either(string k) {
    int v;
    if (A.TryGetValue(k, out v) || B.TryGetValue(k, out v)) {
      return ""hit "" + v;
    }
    return ""miss "" + v;
  }

  public static string EitherVar(string k) {
    if (A.TryGetValue(k, out var v) || B.TryGetValue(k, out v)) {
      return ""hit "" + v;
    }
    return ""miss"";
  }

  public static string EitherKept(string k) {
    int v;
    bool found = A.TryGetValue(k, out v) || B.TryGetValue(k, out v);
    return found + "" "" + v;
  }

  public static string EitherReturned(string k, out int v) {
    return (A.TryGetValue(k, out v) || B.TryGetValue(k, out v)) ? ""hit"" : ""miss"";
  }

  public static string Both(string k, string j) {
    int v, w;
    if (A.TryGetValue(k, out v) && B.TryGetValue(j, out w)) {
      return ""both "" + v + "" "" + w;
    }
    return ""not both "" + v;
  }

  public static string BothVar(string k, string j) {
    if (A.TryGetValue(k, out var v) && B.TryGetValue(j, out var w)) {
      return ""both "" + v + "" "" + w;
    }
    return ""not both"";
  }

  public static string Chain(string k) {
    int v;
    if (A.TryGetValue(k, out v) || B.TryGetValue(k, out v) || A.TryGetValue(""x"", out v)) {
      return ""hit "" + v;
    }
    return ""miss"";
  }

  public static string Negated(string k) {
    if (!A.TryGetValue(k, out var v) && !B.TryGetValue(k, out v)) {
      return ""miss"";
    }
    return ""hit "" + v;
  }
}";

    [TestMethod]
    public void TestOutAcrossOrKeepsTheFirstHit() {
      SameAsNet.Prints(@"
foreach (var k in new[] { ""x"", ""y"", ""z"" }) {
  Console.WriteLine(Stores.Either(k) + "" / "" + Stores.EitherVar(k) + "" / "" + Stores.EitherKept(k) + "" / "" + Stores.Chain(k) + "" / "" + Stores.Negated(k));
  Console.WriteLine(Stores.EitherReturned(k, out var r) + "" "" + r);
}", Stores);
    }

    [TestMethod]
    public void TestOutAcrossAnd() {
      SameAsNet.Prints(@"
Console.WriteLine(Stores.Both(""x"", ""y"") + "" / "" + Stores.BothVar(""x"", ""y""));
Console.WriteLine(Stores.Both(""x"", ""z"") + "" / "" + Stores.BothVar(""x"", ""z""));
Console.WriteLine(Stores.Both(""z"", ""y"") + "" / "" + Stores.BothVar(""z"", ""y""));", Stores);
    }

    [TestMethod]
    public void TestOutAcrossOrInMain() {
      SameAsNet.Prints(@"
var a = new Dictionary<int, string> { [1] = ""one"" };
var b = new Dictionary<int, string> { [2] = ""two"" };
for (int k = 1; k <= 3; k++) {
  if (a.TryGetValue(k, out var s) || b.TryGetValue(k, out s)) Console.WriteLine(k + "" "" + s);
  else Console.WriteLine(k + "" none "" + (s == null));
}");
    }
  }
}
