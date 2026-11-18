using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A pattern variable declared in a part of a condition that runs only sometimes (the right of || or &&) and read after
  /// the if, as `if (gear == null || !(dragged is Round round)) return; Use(round);`: seen (J82, 11-15) as nil in Lua,
  /// the local being declared inside the if the || is written as. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class PatternVariableAcrossLogicalTests {
    private const string Shapes = @"
class Shape { public string Name = ""shape""; }
class Round : Shape { public int R = 2; }

static class Patterns {
  public static string NotIs(object gear, object dragged) {
    if (gear == null || !(dragged is Round round)) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string IsNot(object gear, object dragged) {
    if (gear == null || dragged is not Round round) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string ThreeWay(object gear, object other, object dragged) {
    if (gear == null || other == null || !(dragged is Round round)) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string UsedInTheOr(object gear, object dragged) {
    if (gear == null || !(dragged is Round round) || round.R > 5) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string LeftOfAnAnd(object gear, object dragged) {
    if (gear == null || !(dragged is Round round && round.R > 1)) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string NotOfAnAnd(object gear, object dragged) {
    if (!(gear != null && dragged is Round round)) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string Var(object gear, object dragged) {
    if (gear == null || !(dragged is var seen && seen != null)) { return ""out""; }
    return ""seen "" + (seen is Round);
  }

  public static string Property(object gear, object dragged) {
    if (gear == null || !(dragged is Round { R: 2 } round)) { return ""out""; }
    return ""round "" + round.R;
  }

  public static string PropertyVar(object gear, object dragged) {
    if (gear == null || !(dragged is Round { R: var r })) { return ""out""; }
    return ""r "" + r;
  }

  public static string And(object gear, object dragged) {
    if (gear != null && dragged is Round round) { return ""round "" + round.R; }
    return ""out"";
  }

  public static string AndInBrackets(object gear, object dragged) {
    if (gear != null && (dragged is Round round)) { return ""round "" + round.R; }
    return ""out"";
  }

  public static string Kept(object gear, object dragged) {
    bool isRound = gear != null && dragged is Round round && round.R > 1;
    return ""kept "" + isRound;
  }

  public static string Conditional(object gear, object dragged) {
    return gear != null ? (dragged is Round round ? ""round "" + round.R : ""other"") : ""none"";
  }
}";

    private const string EachPair = @"
var round = new Round();
var wide = new Round { R = 9 };
foreach (var gear in new object[] { null, ""gear"" }) {
  foreach (var dragged in new object[] { null, ""text"", round, wide }) {
    Console.WriteLine({0});
  }
}";

    private static void Same(string call) => SameAsNet.Prints(EachPair.Replace("{0}", call), Shapes);

    [TestMethod]
    public void TestPatternVariableAfterAnOr() {
      Same(@"Patterns.NotIs(gear, dragged) + "" / "" + Patterns.IsNot(gear, dragged) + "" / "" + Patterns.ThreeWay(gear, gear, dragged)
      + "" / "" + Patterns.UsedInTheOr(gear, dragged)");
    }

    [TestMethod]
    public void TestPatternVariableAfterAnOrHoldingAnAnd() {
      Same(@"Patterns.LeftOfAnAnd(gear, dragged) + "" / "" + Patterns.NotOfAnAnd(gear, dragged)");
    }

    [TestMethod]
    public void TestVarAndPropertyPatternVariablesAfterAnOr() {
      Same(@"Patterns.Var(gear, dragged) + "" / "" + Patterns.Property(gear, dragged) + "" / "" + Patterns.PropertyVar(gear, dragged)");
    }

    [TestMethod]
    public void TestPatternVariableInsideAnAnd() {
      Same(@"Patterns.And(gear, dragged) + "" / "" + Patterns.AndInBrackets(gear, dragged) + "" / "" + Patterns.Kept(gear, dragged)
      + "" / "" + Patterns.Conditional(gear, dragged)");
    }

    [TestMethod]
    public void TestPatternVariableAfterAnOrInMain() {
      SameAsNet.Prints(@"
foreach (var dragged in new object[] { null, ""text"", new Round() }) {
  if (dragged == null || !(dragged is Round round)) {
    Console.WriteLine(""out"");
    continue;
  }
  Console.WriteLine(""round "" + round.R);
}", Shapes);
    }

    [TestMethod]
    public void TestPatternVariableInLoopConditionsAndLambdas() {
      SameAsNet.Prints(@"
var items = new object[] { new Round { R = 1 }, new Round { R = 3 }, ""text"", new Round { R = 5 } };
int i = 0;
while (i < items.Length && items[i] is Round round) {
  Console.WriteLine(""while "" + round.R);
  i++;
}
for (int j = 0; !(j >= items.Length || !(items[j] is Round each)); j++) {
  Console.WriteLine(""for "" + each.R);
}
Func<object, bool> wide = o => o != null && o is Round r && r.R > 1;
Func<object, string> name = o => {
  if (o == null || !(o is Round r)) { return ""out""; }
  return ""round "" + r.R;
};
foreach (var item in items) {
  Console.WriteLine(wide(item) + "" "" + name(item));
}", Shapes);
    }

    // Each turn of a loop has a variable of its own, which a lambda made in that turn keeps.
    [TestMethod]
    public void TestPatternVariableAfterAnOrIsOnePerTurn() {
      SameAsNet.Prints(@"
var reads = new List<Func<int>>();
foreach (var dragged in new object[] { new Round { R = 1 }, null, new Round { R = 3 } }) {
  if (dragged == null || !(dragged is Round round)) {
    continue;
  }
  reads.Add(() => round.R);
}
foreach (var read in reads) {
  Console.WriteLine(read());
}", Shapes);
    }
  }
}
