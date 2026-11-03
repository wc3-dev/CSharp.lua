using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  static void T1() { var l = new List<int> { 3, 1, 2 }; l.Add(5); l.AddRange(new[] { 7, 8 }); l.Insert(0, 9); Console.WriteLine(string.Join(",", l) + " " + l.Count); }
  static void T2() { var l = new List<int> { 3, 1, 2, 1 }; Console.WriteLine(l.Remove(1) + " " + l.Remove(99) + " " + string.Join(",", l)); l.RemoveAt(0); l.RemoveAll(x => x == 1); Console.WriteLine(string.Join(",", l)); }
  static void T3() { var l = new List<int> { 3, 1, 2 }; l.Sort(); Console.WriteLine(string.Join(",", l)); l.Reverse(); Console.WriteLine(string.Join(",", l)); l.Sort((a, b) => a.CompareTo(b)); Console.WriteLine(string.Join(",", l)); }
  static void T4() { var l = new List<int> { 3, 1, 2 }; Console.WriteLine(l.IndexOf(1) + " " + l.IndexOf(9) + " " + l.Contains(2) + " " + l.LastIndexOf(2) + " " + l.Exists(x => x > 2) + " " + l.Find(x => x < 3) + " " + l.FindIndex(x => x == 2) + " " + l.FindAll(x => x > 1).Count + " " + l.FindLast(x => x < 3)); }
  static void T5() { var l = new List<string> { "b", "a" }; l.Sort(); Console.WriteLine(string.Join("", l)); l.Sort(StringComparer.Ordinal); l.Clear(); Console.WriteLine(l.Count); }
  static void T6() { var l = new List<int> { 1, 2, 3, 4 }; Console.WriteLine(string.Join(",", l.GetRange(1, 2)) + " " + l.ToArray().Length + " " + string.Join(",", l.ConvertAll(x => x * 2)) + " " + l.TrueForAll(x => x > 0)); l.ForEach(x => Console.Write(x)); Console.WriteLine(); }
  static void T7() { var l = new List<int>(new[] { 1, 2 }); l[0] = 10; Console.WriteLine(l[0] + l[1]); try { var x = l[5]; } catch (ArgumentOutOfRangeException) { Console.WriteLine("aoor"); } }
  static void T8() { var l = new List<int> { 1, 2, 3, 4, 5 }; l.RemoveRange(1, 2); l.InsertRange(1, new[] { 9, 9 }); Console.WriteLine(string.Join(",", l)); Console.WriteLine(l.Capacity >= l.Count); }
  static void T9() { var l = new List<int> { 5, 3, 8 }; Console.WriteLine(l.BinarySearch(3) >= 0); l.Sort(); Console.WriteLine(l.BinarySearch(8)); }
  static void T10() { var l = new List<(int, string)> { (2, "b"), (1, "a") }; l.Sort(); Console.WriteLine(l[0].Item2); }
  static void T11() { var l = new List<int> { 1 }; var r = l.AsReadOnly(); Console.WriteLine(r.Count + " " + r[0]); }
  static void T12() { var l = new List<object> { 1, "a", null }; Console.WriteLine(l.Count + " " + l.Contains(null) + " " + l.IndexOf("a")); }
  static void T13() { var l = new List<string> { "x", "y" }; l.Add(null); Console.WriteLine(l.Count + " " + (l[2] == null)); }
  static void T14() { var l = new List<float> { 1.5f, 2f }; Console.WriteLine(l.Sum() + " " + l.Max() + " " + l[1]); }
  static void T15() { var a = new List<int> { 1, 2 }; var b = new List<int>(a); b.Add(3); Console.WriteLine(a.Count + " " + b.Count); }
  static void T16() { List<int> l = null; Console.WriteLine(l?.Count ?? -1); }
  static void T17() { var l = new List<int> { 4, 2, 9 }; l.Sort((a, b) => b - a); Console.WriteLine(string.Join(",", l)); l.Sort(Comparer<int>.Create((a, b) => a - b)); Console.WriteLine(string.Join(",", l)); }
  static void T18() { var l = new List<int>(); for (int i = 0; i < 1000; i++) l.Add(i); int s = 0; foreach (var x in l) s += x; Console.WriteLine(s); }
  static void T19() { var l = new List<int> { 1, 2, 3 }; for (int i = l.Count - 1; i >= 0; i--) if (l[i] == 2) l.RemoveAt(i); Console.WriteLine(string.Join(",", l)); }
  static void T20() { var l = new List<List<int>> { new() { 1 }, new() { 2, 3 } }; Console.WriteLine(l.Sum(x => x.Count) + " " + l[1][1]); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
