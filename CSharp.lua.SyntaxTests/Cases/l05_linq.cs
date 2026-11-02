using System;
using System.Collections.Generic;
using System.Linq;
record Pe(string Name, int Age, string City);
class Program {
  static List<Pe> People() => new() { new("a", 30, "x"), new("b", 20, "y"), new("c", 40, "x"), new("d", 20, "y") };
  static void T1() { var n = new[] { 1, 2, 3, 4, 5 }; Console.WriteLine(string.Join(",", n.Where(x => x > 2).Select(x => x * x))); }
  static void T2() { var n = new[] { 3, 1, 2 }; Console.WriteLine(string.Join(",", n.OrderBy(x => x)) + " " + string.Join(",", n.OrderByDescending(x => x)) + " " + string.Join(",", n.OrderBy(x => x).ThenBy(x => x))); }
  static void T3() { var p = People(); Console.WriteLine(string.Join(",", p.OrderBy(x => x.Age).ThenByDescending(x => x.Name).Select(x => x.Name))); }
  static void T4() { var n = new[] { 1, 2, 3, 4 }; Console.WriteLine(n.Sum() + " " + n.Average() + " " + n.Min() + " " + n.Max() + " " + n.Count() + " " + n.Count(x => x > 2) + " " + n.Sum(x => x * 2)); }
  static void T5() { var n = new[] { 1, 2, 3 }; Console.WriteLine(n.Any() + " " + n.Any(x => x > 5) + " " + n.All(x => x > 0) + " " + n.Contains(2) + " " + n.First() + " " + n.Last() + " " + n.First(x => x > 1) + " " + n.FirstOrDefault(x => x > 9) + " " + n.LastOrDefault() + " " + n.ElementAt(1) + " " + n.ElementAtOrDefault(9)); }
  static void T6() { var n = new[] { 1, 2, 3 }; try { n.First(x => x > 9); } catch (InvalidOperationException) { Console.WriteLine("ioe"); } Console.WriteLine(n.SingleOrDefault(x => x == 2) + " " + n.Single(x => x == 3)); }
  static void T7() { var n = new[] { 1, 2, 3, 4, 5 }; Console.WriteLine(string.Join(",", n.Skip(2)) + " " + string.Join(",", n.Take(2)) + " " + string.Join(",", n.SkipWhile(x => x < 3)) + " " + string.Join(",", n.TakeWhile(x => x < 3)) + " " + string.Join(",", n.TakeLast(2)) + " " + string.Join(",", n.SkipLast(2))); }
  static void T8() { var p = People(); foreach (var g in p.GroupBy(x => x.City).OrderBy(g => g.Key)) Console.Write(g.Key + ":" + g.Count() + ":" + string.Join("", g.Select(x => x.Name)) + " "); Console.WriteLine(); }
  static void T9() { var n = new[] { 1, 2, 2, 3, 3, 3 }; Console.WriteLine(string.Join(",", n.Distinct()) + " " + string.Join(",", n.Union(new[] { 9 })) + " " + string.Join(",", n.Intersect(new[] { 2, 3 })) + " " + string.Join(",", n.Except(new[] { 2 }))); }
  static void T10() { var a = new[] { 1, 2 }; var b = new[] { "x", "y" }; Console.WriteLine(string.Join(",", a.Zip(b, (i, s) => i + s)) + " " + string.Join(",", a.Concat(new[] { 3 })) + " " + string.Join(",", a.Append(9).Prepend(0))); }
  static void T11() { var n = new[] { 1, 2, 3 }; Console.WriteLine(string.Join(",", n.Select((x, i) => x * i)) + " " + string.Join(",", n.SelectMany(x => new[] { x, x }))); }
  static void T12() { var n = Enumerable.Range(1, 5); Console.WriteLine(string.Join(",", n) + " " + string.Join(",", Enumerable.Repeat(7, 3)) + " " + Enumerable.Empty<int>().Count()); }
  static void T13() { var n = new[] { 1, 2, 3 }; Console.WriteLine(n.Aggregate((a, b) => a * b) + " " + n.Aggregate(10, (a, b) => a + b) + " " + n.Aggregate("", (a, b) => a + b)); }
  static void T14() { var p = People(); var d = p.ToDictionary(x => x.Name, x => x.Age); var l = p.Select(x => x.Age).ToList(); var h = p.Select(x => x.City).ToHashSet(); var lk = p.ToLookup(x => x.City); Console.WriteLine(d["c"] + " " + l.Count + " " + h.Count + " " + lk["x"].Count()); }
  static void T15() { var p = People(); Console.WriteLine(p.Max(x => x.Age) + " " + p.Min(x => x.Age) + " " + p.MaxBy(x => x.Age).Name + " " + p.MinBy(x => x.Age).Name + " " + p.Sum(x => x.Age) + " " + p.Average(x => x.Age)); }
  static void T16() { var q = new[] { 1, 2, 3 }.Select(x => { Console.Write("s" + x); return x; }).Where(x => x > 1); Console.WriteLine("built"); foreach (var x in q) Console.Write("f" + x); Console.WriteLine(); }
  static void T17() { var l = new List<int> { 1, 2, 3 }; var q = l.Where(x => x > 1); l.Add(4); Console.WriteLine(q.Count()); }
  static void T18() { var n = new[] { 5, 3, 8 }; Console.WriteLine(string.Join(",", n.Order()) + " " + string.Join(",", n.OrderDescending()) + " " + string.Join(",", n.Reverse())); }
  static void T19() { var p = People(); var r = from x in p where x.Age > 20 orderby x.Name descending select x.Name + x.Age; Console.WriteLine(string.Join(",", r)); }
  static void T20() { var p = People(); var r = from x in p group x by x.City into g select new { g.Key, N = g.Count() }; foreach (var x in r.OrderBy(x => x.Key)) Console.Write(x.Key + x.N); Console.WriteLine(); }
  static void T21() { var a = new[] { 1, 2 }; var b = new[] { 2, 3 }; var r = from x in a join y in b on x equals y select x; Console.WriteLine(string.Join(",", r)); var r2 = from x in a from y in b select x * y; Console.WriteLine(string.Join(",", r2)); }
  static void T22() { var s = new[] { "apple", "bob", "cat" }; Console.WriteLine(string.Join(",", s.Where(w => w.Length > 3)) + " " + s.Select(w => w.Length).Max() + " " + string.Join(",", s.OrderBy(w => w.Length).ThenBy(w => w))); }
  static void T23() { var n = new[] { 1.5f, 2.5f }; Console.WriteLine(n.Sum() + " " + n.Average() + " " + n.Max()); var d = new[] { 1.5, 2.5 }; Console.WriteLine(d.Sum() + " " + d.Average()); var l = new[] { 1L, 2L }; Console.WriteLine(l.Sum()); }
  static void T24() { var n = new[] { 1, 2, 3, 4 }; Console.WriteLine(string.Join(",", n.Chunk(3).Select(c => c.Length)) + " " + n.SequenceEqual(new[] { 1, 2, 3, 4 }) + " " + string.Join(",", n.Where((x, i) => i % 2 == 0))); }
  static void T25() { IEnumerable<int> e = new List<int> { 1, 2 }; Console.WriteLine(e.Count() + " " + e.ToList().Count + " " + e.ToArray().Length + " " + e.Cast<object>().Count() + " " + new object[] { 1, "a", 2 }.OfType<int>().Sum()); }
  static void T26() { var n = new[] { 4, 2 }; Console.WriteLine(n.DefaultIfEmpty().Count() + " " + new int[0].DefaultIfEmpty(5).First() + " " + new int[0].Sum() + " " + new int[0].Max(x => (int?)x)); }
  static void T27() { var n = new List<int> { 1, 2, 3 }; Console.WriteLine(n.Select(x => x.ToString()).Aggregate((a, b) => a + "-" + b) + " " + n.Sum(x => (long)x)); }
  static void T28() { var words = new[] { "a", "bb", "cc" }; var g = words.GroupBy(w => w.Length).ToDictionary(x => x.Key, x => x.ToList()); Console.WriteLine(g[2].Count); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); H.R("T23", T23); H.R("T24", T24); H.R("T25", T25); H.R("T26", T26); H.R("T27", T27); H.R("T28", T28); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
