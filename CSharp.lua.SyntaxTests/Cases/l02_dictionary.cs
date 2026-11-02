using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  static void T1() { var d = new Dictionary<string, int>(); d["a"] = 1; d.Add("b", 2); d["a"] += 10; Console.WriteLine(d["a"] + " " + d["b"] + " " + d.Count); }
  static void T2() { var d = new Dictionary<string, int> { ["a"] = 1 }; Console.WriteLine(d.ContainsKey("a") + " " + d.ContainsKey("z") + " " + d.ContainsValue(1) + " " + d.TryGetValue("a", out var v) + v + " " + d.TryGetValue("z", out var w) + w); }
  static void T3() { var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }; Console.WriteLine(d.Remove("a") + " " + d.Remove("zz") + " " + d.Count); d.Clear(); Console.WriteLine(d.Count); }
  static void T4() { var d = new Dictionary<int, string> { [3] = "c", [1] = "a" }; foreach (var kv in d.OrderBy(k => k.Key)) Console.Write(kv.Key + kv.Value); Console.WriteLine(); }
  static void T5() { var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }; Console.WriteLine(d.Keys.Count + " " + d.Values.Sum() + " " + string.Join(",", d.Keys.OrderBy(k => k)) + " " + d.Keys.Contains("a")); }
  static void T6() { var d = new Dictionary<string, int>(); try { var x = d["nope"]; } catch (KeyNotFoundException) { Console.WriteLine("knf"); } try { d.Add("a", 1); d.Add("a", 2); } catch (ArgumentException) { Console.WriteLine("dup"); } }
  static void T7() { var d = new Dictionary<string, List<int>>(); if (!d.TryGetValue("k", out var l)) { l = new List<int>(); d["k"] = l; } l.Add(1); l.Add(2); Console.WriteLine(d["k"].Count); }
  static void T8() { var d = new Dictionary<(int, int), string> { [(0, 1)] = "x" }; Console.WriteLine(d[(0, 1)] + d.ContainsKey((1, 0))); }
  static void T9() { var d = new Dictionary<char, int>(); foreach (var c in "hello") d[c] = d.GetValueOrDefault(c) + 1; Console.WriteLine(d['l'] + " " + d.Count); }
  static void T10() { var d = new Dictionary<string, int> { ["x"] = 1 }; d.TryAdd("x", 5); d.TryAdd("y", 6); Console.WriteLine(d["x"] + " " + d["y"]); d.Remove("x", out var rem); Console.WriteLine(rem); }
  static void T11() { var d = new Dictionary<int, int>(); for (int i = 0; i < 100; i++) d[i] = i * i; int s = 0; foreach (var kv in d) s += kv.Value; Console.WriteLine(s + " " + d.Count); }
  static void T12() { var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }; var l = d.ToList(); Console.WriteLine(l.Count + " " + l[0].Key); var arr = d.Select(kv => kv.Key + kv.Value).ToArray(); Console.WriteLine(arr.Length); }
  static void T13() { var d = new Dictionary<float, string> { [1.5f] = "a", [2f] = "b" }; Console.WriteLine(d[1.5f] + d[2f]); }
  static void T14() { var d = new Dictionary<bool, string> { [true] = "t", [false] = "f" }; Console.WriteLine(d[true] + d[false]); }
  static void T15() { var d = new Dictionary<object, int>(); var k = new object(); d[k] = 1; Console.WriteLine(d[k] + " " + d.ContainsKey(new object())); }
  static void T16() { var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Ab"] = 1 }; Console.WriteLine(d["aB"]); }
  static void T17() { var d = new Dictionary<int, string> { [1] = "a" }; foreach (var k in d.Keys.ToList()) d[k] = d[k] + "!"; Console.WriteLine(d[1]); }
  static void T18() { var d = new Dictionary<string, int> { ["a"] = 1 }; try { foreach (var kv in d) d["b"] = 2; } catch (InvalidOperationException) { Console.WriteLine("modified"); } }
  static void T19() { var d = new SortedDictionary<int, string> { [3] = "c", [1] = "a", [2] = "b" }; Console.WriteLine(string.Join(",", d.Keys) + string.Join("", d.Values)); }
  static void T20() { var d = new SortedList<string, int> { ["b"] = 2, ["a"] = 1 }; Console.WriteLine(d.Keys[0] + d.Values[1]); }
  static void T21() { var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }; var o = d.ToDictionary(kv => kv.Key.ToUpper(), kv => kv.Value * 2); Console.WriteLine(o["B"]); }
  static void T22() { var d = new Dictionary<int, int> { [1] = 1, [2] = 2, [3] = 3 }; foreach (var k in d.Keys.Where(k => k != 2).ToList()) d.Remove(k); Console.WriteLine(d.Count + " " + d.Keys.First()); }
  static void T23() { Dictionary<string, int> d = new() { { "a", 1 }, { "b", 2 } }; Console.WriteLine(d.Sum(kv => kv.Value) + " " + d.Any(kv => kv.Value > 1) + " " + d.Count(kv => kv.Value > 0)); }
  static void T24() { var d = new Dictionary<int, int>(); d[int.MaxValue] = 1; d[-1] = 2; d[0] = 3; Console.WriteLine(d[int.MaxValue] + d[-1] + d[0]); }
  static void T25() { var d = new Dictionary<string, Dictionary<string, int>>(); d["a"] = new() { ["x"] = 1 }; d["a"]["y"] = 2; Console.WriteLine(d["a"].Count); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); H.R("T23", T23); H.R("T24", T24); H.R("T25", T25); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
