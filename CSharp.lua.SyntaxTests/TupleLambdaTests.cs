using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A lambda or method group written straight into a tuple literal takes its delegate type from the tuple's target
  /// type. The compiler stopped with a NullReferenceException at it, inside a collection initializer and out.
  /// </summary>
  [TestClass]
  public class TupleLambdaTests {
    private const string Members = @"
static class S {
  public static string Shout(int n) => ""shout "" + n;
}
";

    [TestMethod]
    public void TestALambdaInATupleInACollectionInitializer() {
      SameAsNet.Prints(@"
var list = new List<(string Name, Func<int, string> Click)> { (""a"", n => ""a "" + n), (""b"", n => ""b "" + n) };
Console.WriteLine(list[0].Name + "" "" + list[0].Click(1) + "", "" + list[1].Click(2));");
    }

    [TestMethod]
    public void TestALambdaInATupleOutsideAnInitializer() {
      SameAsNet.Prints(@"
(string Name, Func<int, string> Click) one = (""a"", n => ""a "" + n);
(string, Action) two = (""b"", () => Console.WriteLine(""ran""));
Console.WriteLine(one.Click(3));
two.Item2();", Members);
    }

    [TestMethod]
    public void TestAMethodGroupInATuple() {
      SameAsNet.Prints(@"
(string Name, Func<int, string> Click) one = (""a"", S.Shout);
var list = new List<(string, Func<int, string>)> { (""b"", S.Shout) };
Console.WriteLine(one.Click(4) + "", "" + list[0].Item2(5));", Members);
    }

    [TestMethod]
    public void TestALambdaInANestedTuple() {
      SameAsNet.Prints(@"
(int, (string, Func<int, string>)) nested = (1, (""a"", n => ""nested "" + n));
var list = new List<(int, (string, Func<int, string>))> { (2, (""b"", S.Shout)) };
Console.WriteLine(nested.Item2.Item2(6) + "", "" + list[0].Item2.Item2(7));", Members);
    }
  }
}
