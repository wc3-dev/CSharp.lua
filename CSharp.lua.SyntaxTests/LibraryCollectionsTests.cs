using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Members of the collections the Lua core lacked or answered wrongly (Moonwell's MW0030 rejected most of them): each
  /// prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class LibraryCollectionsTests {
    private const string Point = "struct P { public int X; public P(int x) { X = x; } }\n";

    [TestMethod]
    public void TestGetValueOrDefault() {
      SameAsNet.Prints(@"
var d = new Dictionary<int, string> { [1] = ""a"" };
Console.WriteLine(d.GetValueOrDefault(1) + "","" + (d.GetValueOrDefault(2) ?? ""null"") + "","" + d.GetValueOrDefault(2, ""z""));
var n = new Dictionary<string, int> { [""x""] = 3 };
Console.WriteLine(n.GetValueOrDefault(""x"") + "" "" + n.GetValueOrDefault(""y"") + "" "" + n.GetValueOrDefault(""y"", 7));
IReadOnlyDictionary<string, int> r = n;
Console.WriteLine(r.GetValueOrDefault(""x"", 9));
var p = new Dictionary<int, P>();
Console.WriteLine(p.GetValueOrDefault(1).X);", Point);
    }

    [TestMethod]
    public void TestRemoveWithItsValue() {
      SameAsNet.Prints(@"
var d = new Dictionary<int, string> { [1] = ""a"" };
Console.WriteLine(d.Remove(1, out var v) + "" "" + v + "" "" + d.Count);
Console.WriteLine(d.Remove(1, out var w) + "" "" + (w ?? ""null""));
d[2] = ""b"";
Console.WriteLine(d.Remove(2));
Console.WriteLine(d.Remove(2));
var t = new Dictionary<(int, int), string> { [(1, 2)] = ""a"", [(3, 4)] = ""b"" };
Console.WriteLine(t.Remove((1, 2), out var x) + "" "" + x + "" "" + t.Count);
Console.WriteLine(t.Remove((3, 4)) + "" "" + t.Remove((3, 4)) + "" "" + t.Count);");
    }

    [TestMethod]
    public void TestADictionarysPairsKeepTheirKey() {
      SameAsNet.Prints(@"
var d = new Dictionary<int, string> { [1] = ""a"", [2] = ""b"" };
var list = d.ToList();
list.Sort((x, y) => x.Key.CompareTo(y.Key));
Console.WriteLine(list[0].Key + list[0].Value + list[1].Key + list[1].Value);
var array = d.ToArray();
Array.Sort(array, (x, y) => x.Key.CompareTo(y.Key));
Console.WriteLine(array[1].Key + array[1].Value + "" "" + array.Length);");
    }

    [TestMethod]
    public void TestConvertAll() {
      SameAsNet.Prints(@"
var list = new List<int> { 1, 2, 3 };
var texts = list.ConvertAll(i => ""n"" + i);
Console.WriteLine(string.Join("","", texts) + "" "" + texts.Count);
var doubled = Array.ConvertAll(new[] { 1, 2 }, i => i * 2);
Console.WriteLine(string.Join("","", doubled) + "" "" + doubled.Length);");
    }

    [TestMethod]
    public void TestTryPeekAndTryPop() {
      SameAsNet.Prints(@"
var q = new Queue<int>();
Console.WriteLine(q.TryPeek(out var a) + "" "" + a + "" "" + q.TryDequeue(out var b) + "" "" + b);
q.Enqueue(5);
Console.WriteLine(q.TryPeek(out var c) + "" "" + c + "" "" + q.Count);
var s = new Stack<int>();
Console.WriteLine(s.TryPop(out var d) + "" "" + d + "" "" + s.TryPeek(out var e) + "" "" + e);
s.Push(1);
s.Push(2);
Console.WriteLine(s.TryPeek(out var f) + "" "" + f + "" "" + s.Count);
Console.WriteLine(s.TryPop(out var g) + "" "" + g + "" "" + s.Count);
var points = new Queue<P>();
points.TryDequeue(out var p);
points.TryPeek(out var r);
var stack = new Stack<P>();
stack.TryPop(out var t);
Console.WriteLine(p.X + "" "" + r.X + "" "" + t.X);", Point);
    }

    [TestMethod]
    public void TestStackCopyTo() {
      SameAsNet.Prints(@"
var s = new Stack<int>();
s.Push(1);
s.Push(2);
var array = new int[3];
s.CopyTo(array, 1);
Console.WriteLine(string.Join("","", array));");
    }

    [TestMethod]
    public void TestSetEquals() {
      SameAsNet.Prints(@"
var set = new HashSet<int> { 1, 2 };
Console.WriteLine(set.SetEquals(new[] { 2, 1 }) + "" "" + set.SetEquals(new[] { 1, 2, 2 }) + "" "" + set.SetEquals(new[] { 1 }) + "" "" + set.SetEquals(new[] { 1, 3 }));
Console.WriteLine(set.SetEquals(new HashSet<int> { 2, 1 }) + "" "" + new HashSet<int>().SetEquals(new int[0]));
ISet<int> i = set;
Console.WriteLine(i.SetEquals(new List<int> { 1, 2 }));");
    }

    [TestMethod]
    public void TestHashSetTryGetValue() {
      SameAsNet.Prints(@"
var set = new HashSet<string> { ""a"" };
Console.WriteLine(set.TryGetValue(""a"", out var a) + "" "" + a);
Console.WriteLine(set.TryGetValue(""b"", out var b) + "" "" + (b ?? ""null""));");
    }

    [TestMethod]
    public void TestSortKeysAndItems() {
      SameAsNet.Prints(@"
var keys = new[] { 3, 1, 2 };
var items = new[] { ""c"", ""a"", ""b"" };
Array.Sort(keys, items);
Console.WriteLine(string.Join("","", keys) + "" "" + string.Join("","", items));
var more = new[] { 2, 1 };
var names = new[] { ""two"", ""one"" };
Array.Sort(more, names, Comparer<int>.Create((x, y) => y.CompareTo(x)));
Console.WriteLine(string.Join("","", more) + "" "" + string.Join("","", names));");
    }
  }
}
