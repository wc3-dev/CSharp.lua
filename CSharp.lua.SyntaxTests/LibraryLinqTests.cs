using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// LINQ members the Lua core lacked or answered wrongly (Moonwell's MW0030 rejected some, and missed the newer ones):
  /// each prints in Lua what it prints on .NET, also when its result is enumerated twice.
  /// </summary>
  [TestClass]
  public class LibraryLinqTests {
    [TestMethod]
    public void TestAppendAndPrepend() {
      SameAsNet.Prints(@"
var list = new List<int> { 1, 2 };
Console.WriteLine(string.Join("","", list.Append(3)) + "" "" + string.Join("","", list.Prepend(0)) + "" "" + string.Join("","", list.Append(3).Prepend(0).Append(4)));
var appended = list.Append(9);
Console.WriteLine(appended.Count() + "" "" + appended.Count() + "" "" + appended.Last());");
    }

    [TestMethod]
    public void TestSkipLastAndTakeLast() {
      SameAsNet.Prints(@"
var items = new[] { 1, 2, 3, 4 };
Console.WriteLine(string.Join("","", items.SkipLast(1)) + "" "" + string.Join("","", items.SkipLast(0)) + "" "" + items.SkipLast(9).Count());
var last = items.TakeLast(2);
Console.WriteLine(string.Join("","", last) + "" "" + string.Join("","", last) + "" "" + items.TakeLast(0).Count() + "" "" + string.Join("","", items.TakeLast(9)));
Console.WriteLine(string.Join("","", items.Where(i => i > 1).TakeLast(2)) + "" "" + string.Join("","", items.Where(i => i > 1).SkipLast(2)));");
    }

    [TestMethod]
    public void TestTakeAndSkipCanRunTwice() {
      SameAsNet.Prints(@"
var items = new List<int> { 1, 2, 3, 4 };
var first = items.Take(2);
var rest = items.Skip(2);
Console.WriteLine(first.Count() + "" "" + first.Count() + "" "" + rest.Count() + "" "" + rest.Count());");
    }

    [TestMethod]
    public void TestChunk() {
      SameAsNet.Prints(@"
foreach (var chunk in Enumerable.Range(1, 5).Chunk(2)) {
  Console.WriteLine(string.Join("","", chunk) + "" "" + chunk.Length);
}
var chunks = new[] { ""a"", ""b"" }.Chunk(3);
Console.WriteLine(chunks.Count() + "" "" + chunks.Count());");
    }

    [TestMethod]
    public void TestToHashSet() {
      SameAsNet.Prints(@"
var set = new[] { 1, 2, 2, 3 }.ToHashSet();
Console.WriteLine(set.Count + "" "" + set.Contains(2) + "" "" + set.Add(3));
var words = new List<string> { ""a"", ""b"", ""a"" }.ToHashSet(EqualityComparer<string>.Default);
Console.WriteLine(words.Count);");
    }

    [TestMethod]
    public void TestEmpty() {
      SameAsNet.Prints(@"
var empty = Enumerable.Empty<int>();
Console.WriteLine(empty.Count() + "" "" + empty.Any() + "" "" + string.Join("","", empty.Concat(new[] { 1 })));
Console.WriteLine(Enumerable.Empty<string>().FirstOrDefault() ?? ""null"");");
    }

    [TestMethod]
    public void TestZip() {
      SameAsNet.Prints(@"
var numbers = new[] { 1, 2, 3 };
var words = new List<string> { ""one"", ""two"" };
foreach (var (n, w) in numbers.Zip(words)) {
  Console.WriteLine(n + w);
}
var pair = numbers.Zip(words).First();
Console.WriteLine(pair.First + pair.Second);
var flags = new[] { true, false, true };
foreach (var t in numbers.Zip(words, flags)) {
  Console.WriteLine(t.First + t.Second + t.Third);
}
var sums = numbers.Zip(numbers, (a, b) => a + b);
Console.WriteLine(string.Join("","", sums) + "" "" + sums.Count());");
    }

    [TestMethod]
    public void TestToLookup() {
      SameAsNet.Prints(@"
var words = new[] { ""ant"", ""bee"", ""wasp"", ""fly"" };
var byLength = words.ToLookup(w => w.Length);
Console.WriteLine(byLength.Count + "" "" + string.Join("","", byLength[3]) + "" "" + byLength.Contains(4) + "" "" + byLength[9].Count());
foreach (var group in byLength) {
  Console.WriteLine(group.Key + "":"" + string.Join("","", group));
}
var firsts = words.ToLookup(w => w.Length, w => w[0]);
Console.WriteLine(string.Join("","", firsts[3]));");
    }

    [TestMethod]
    public void TestDefaultIfEmpty() {
      SameAsNet.Prints(@"
var none = new List<int>();
var zero = none.DefaultIfEmpty();
Console.WriteLine(string.Join("","", zero) + "" "" + string.Join("","", zero) + "" "" + zero.Count());
Console.WriteLine(string.Join("","", none.DefaultIfEmpty(7)) + "" "" + string.Join("","", new[] { 1, 2 }.DefaultIfEmpty(7)));
var some = new[] { 1, 2 }.DefaultIfEmpty();
Console.WriteLine(string.Join("","", some) + "" "" + string.Join("","", some));
Console.WriteLine(new List<string>().DefaultIfEmpty().First() ?? ""null"");");
    }

    [TestMethod]
    public void TestDistinctByAndTheOtherSetsByKey() {
      SameAsNet.Prints(@"
var words = new[] { ""ant"", ""bee"", ""wasp"", ""fly"", ""moth"" };
Console.WriteLine(string.Join("","", words.DistinctBy(w => w.Length)));
Console.WriteLine(string.Join("","", words.ExceptBy(new[] { 4 }, w => w.Length)));
Console.WriteLine(string.Join("","", words.IntersectBy(new[] { 4, 9 }, w => w.Length)));
Console.WriteLine(string.Join("","", words.UnionBy(new[] { ""cat"", ""horse"" }, w => w.Length)));
var distinct = words.DistinctBy(w => w[0]);
Console.WriteLine(distinct.Count() + "" "" + distinct.Count());");
    }

    [TestMethod]
    public void TestCountByAndAggregateBy() {
      SameAsNet.Prints(@"
var words = new[] { ""ant"", ""bee"", ""wasp"", ""fly"", ""moth"" };
foreach (var pair in words.CountBy(w => w.Length)) {
  Console.WriteLine(pair.Key + ""="" + pair.Value);
}
foreach (var pair in words.AggregateBy(w => w.Length, """", (text, w) => text + w[0])) {
  Console.WriteLine(pair.Key + ""="" + pair.Value);
}
foreach (var pair in words.AggregateBy(w => w.Length, length => length * 10, (total, w) => total + 1)) {
  Console.WriteLine(pair.Key + ""="" + pair.Value);
}");
    }

    [TestMethod]
    public void TestIndex() {
      SameAsNet.Prints(@"
foreach (var (i, word) in new[] { ""a"", ""b"" }.Index()) {
  Console.WriteLine(i + word);
}
var first = new List<int> { 5, 6 }.Index().Last();
Console.WriteLine(first.Index + "" "" + first.Item);");
    }

    [TestMethod]
    public void TestLongCountAndTryGetNonEnumeratedCount() {
      SameAsNet.Prints(@"
var list = new List<int> { 1, 2, 3 };
long all = list.LongCount();
Console.WriteLine(all + "" "" + list.LongCount(i => i > 1));
Console.WriteLine(list.TryGetNonEnumeratedCount(out var a) + "" "" + a);
Console.WriteLine(new[] { 4, 5 }.TryGetNonEnumeratedCount(out var b) + "" "" + b);
Console.WriteLine(list.Where(i => i > 1).TryGetNonEnumeratedCount(out var c) + "" "" + c);");
    }

    [TestMethod]
    public void TestLeftJoinAndRightJoin() {
      SameAsNet.Prints(@"
var people = new[] { (Name: ""Ann"", Team: 1), (Name: ""Bo"", Team: 2), (Name: ""Cy"", Team: 3) };
var teams = new[] { (Id: 1, Title: ""red""), (Id: 2, Title: ""blue""), (Id: 4, Title: ""green"") };
foreach (var line in people.LeftJoin(teams, p => p.Team, t => t.Id, (p, t) => p.Name + "":"" + (t.Title ?? ""none""))) {
  Console.WriteLine(line);
}
foreach (var line in people.RightJoin(teams, p => p.Team, t => t.Id, (p, t) => (p.Name ?? ""none"") + "":"" + t.Title)) {
  Console.WriteLine(line);
}");
    }
  }
}
