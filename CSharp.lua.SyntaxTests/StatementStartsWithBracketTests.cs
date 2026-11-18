using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A statement that starts with a bracket in Lua, as `((Window)faction).Refresh();` does, after a statement that ends in
  /// a call: with no `;` between, Lua reads the two lines as one, calling what the first call gave back (J88, 11-15,
  /// "attempt to call a nil value"). Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class StatementStartsWithBracketTests {
    private const string Windows = @"
class Window {
  public string Name;
  public int Shown;
  public void Refresh() { Console.WriteLine(""refresh "" + Name); }
  public Window Again() { Console.WriteLine(""again "" + Name); return this; }
  public Action Later() { return () => Console.WriteLine(""later "" + Name); }
}
class FactionOverview : Window { }
class RunStatsWindow : Window { }

static class Screens {
  public static object Faction = new FactionOverview { Name = ""faction"" };
  public static object Stats = new RunStatsWindow { Name = ""stats"" };

  public static void TwoCasts() {
    ((FactionOverview)Faction).Refresh();
    ((RunStatsWindow)Stats).Refresh();
  }

  public static void ThreeCasts() {
    ((FactionOverview)Faction).Refresh();
    ((RunStatsWindow)Stats).Refresh();
    ((Window)Faction).Refresh();
  }

  public static void AfterACallThatGivesSomething() {
    ((Window)Faction).Again();
    ((Window)Stats).Again();
  }

  public static void AfterAPlainCall() {
    Console.WriteLine(""plain"");
    ((RunStatsWindow)Stats).Refresh();
  }

  public static void AfterALocal() {
    var later = ((Window)Faction).Later();
    ((RunStatsWindow)Stats).Refresh();
    later();
  }

  public static void AfterAnAssignment() {
    Action later;
    later = ((Window)Faction).Later();
    ((RunStatsWindow)Stats).Refresh();
    later();
  }

  public static void AfterAComment() {
    ((FactionOverview)Faction).Refresh();
    // The other window.

    ((RunStatsWindow)Stats).Refresh();
  }

  public static void AfterALineMark() {
    /*[[--@mw 1:27]]*/ ((FactionOverview)Faction).Refresh();
    /*[[--@mw 1:28]]*/ ((RunStatsWindow)Stats).Refresh();
    /*[[--@mw 1:29]]*/
    ((Window)Faction).Again();
    // The other window.
    /*[[--@mw 1:32]]*/ ((Window)Stats).Again();
  }

  public static void AfterALineMarkInAnIf(bool yes) {
    /*[[--@mw 1:36]]*/ if (yes) {
      /*[[--@mw 1:37]]*/ ((FactionOverview)Faction).Refresh();
      /*[[--@mw 1:38]]*/ ((RunStatsWindow)Stats).Refresh();
    }
    /*[[--@mw 1:40]]*/ ((Window)Faction).Refresh();
  }

  public static void AsCasts() {
    (Faction as Window).Refresh();
    (Stats as Window).Refresh();
  }

  public static void SetsAfterACall() {
    ((Window)Faction).Refresh();
    ((Window)Stats).Shown = 3;
    ((Window)Faction).Shown += 2;
    Console.WriteLine(((Window)Stats).Shown + "" "" + ((Window)Faction).Shown);
  }

  public static void InALambda() {
    Action both = () => {
      ((FactionOverview)Faction).Refresh();
      ((RunStatsWindow)Stats).Refresh();
    };
    both();
  }

  public static void InsideAnIf(bool yes) {
    if (yes) {
      ((FactionOverview)Faction).Refresh();
      ((RunStatsWindow)Stats).Refresh();
    }
    ((Window)Faction).Refresh();
  }

  public static void InvokedLambdaAfterACall() {
    Console.WriteLine(""before"");
    ((Action)(() => Console.WriteLine(""invoked"")))();
  }
}";

    [TestMethod]
    public void TestTwoCastCallsInARow() {
      SameAsNet.Prints(@"
Screens.TwoCasts();
Screens.ThreeCasts();
Screens.AfterACallThatGivesSomething();
Screens.AfterAPlainCall();", Windows);
    }

    // Moonwell writes a line mark, a Lua comment, before every statement: the `;` goes on the statement before the mark.
    [TestMethod]
    public void TestTwoCastCallsWithLineMarksBetween() {
      SameAsNet.Prints(@"
Screens.AfterALineMark();
Screens.AfterALineMarkInAnIf(true);
Screens.AfterALineMarkInAnIf(false);", Windows);
    }

    [TestMethod]
    public void TestCastCallAfterOtherStatements() {
      SameAsNet.Prints(@"
Screens.AfterALocal();
Screens.AfterAnAssignment();
Screens.AfterAComment();
Screens.AsCasts();
Screens.SetsAfterACall();", Windows);
    }

    [TestMethod]
    public void TestCastCallsInBlocks() {
      SameAsNet.Prints(@"
Screens.InALambda();
Screens.InsideAnIf(true);
Screens.InsideAnIf(false);
Screens.InvokedLambdaAfterACall();", Windows);
    }

    [TestMethod]
    public void TestCastCallsInMain() {
      SameAsNet.Prints(@"
object faction = new FactionOverview { Name = ""faction"" };
object stats = new RunStatsWindow { Name = ""stats"" };
((FactionOverview)faction).Refresh();
((RunStatsWindow)stats).Refresh();", Windows);
    }
  }
}
