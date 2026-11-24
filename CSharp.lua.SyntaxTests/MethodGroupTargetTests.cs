using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A method group of an object that a call, a property or an indexer gives, such as Make().Tick, was written
  /// System.fn(Make(), Make().Tick), so the call ran twice. What stands before the dot runs once, as on .NET.
  /// </summary>
  [TestClass]
  public class MethodGroupTargetTests {
    private const string Members = @"
class Counter {
  public int Ticks;
  public void Tick() { Ticks++; }
  public virtual string Name() => ""counter"";
}
class Named : Counter {
  public override string Name() => ""named"";
}
static class Maker {
  public static int Made;
  public static List<Counter> All = new List<Counter>();
  public static Counter Make() { Made++; var c = new Counter(); All.Add(c); return c; }
  public static Counter MakeNamed() { Made++; var c = new Named(); All.Add(c); return c; }
  public static Counter Current { get { Made++; return All[0]; } }
}
class Holder {
  public int Reads;
  public Counter this[int i] { get { Reads++; return Maker.All[i]; } }
}
";

    [TestMethod]
    public void TestTheCallBeforeAMethodGroupRunsOnce() {
      SameAsNet.Prints(@"
Action tick = Maker.Make().Tick;
tick();
tick();
Console.WriteLine(Maker.Made + "" "" + Maker.All.Count + "" "" + Maker.All[0].Ticks);", Members);
    }

    [TestMethod]
    public void TestAPropertyOrIndexerBeforeAMethodGroupRunsOnce() {
      SameAsNet.Prints(@"
Maker.Make();
var holder = new Holder();
Action fromProperty = Maker.Current.Tick;
Action fromIndexer = holder[0].Tick;
fromProperty();
fromIndexer();
Console.WriteLine(Maker.Made + "" "" + holder.Reads + "" "" + Maker.All[0].Ticks);", Members);
    }

    [TestMethod]
    public void TestAVirtualMethodGroupOfACallRunsTheOverride() {
      SameAsNet.Prints(@"
Func<string> name = Maker.MakeNamed().Name;
Console.WriteLine(name() + "" "" + Maker.Made);
Func<Action, Action> pass = a => a;
pass(Maker.Make().Tick)();
Console.WriteLine(Maker.Made + "" "" + Maker.All[1].Ticks);", Members);
    }
  }
}
