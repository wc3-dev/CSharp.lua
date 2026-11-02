using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  static void T1() { int[] a = [1, 2, 3]; List<int> l = [4, 5]; Console.WriteLine(a.Length + " " + l.Count); }
  static void T2() { int[] a = [1, 2]; int[] b = [..a, 3, ..a]; Console.WriteLine(string.Join(",", b)); }
  static void T3() { List<string> l = ["a", .. new[] { "b", "c" }]; Console.WriteLine(string.Join("", l)); }
  static void T4() { IEnumerable<int> e = [1, 2, 3]; Console.WriteLine(e.Sum()); }
  static void T5() { int[] empty = []; Console.WriteLine(empty.Length); }
  static void T6() { HashSet<int> h = [1, 1, 2]; Console.WriteLine(h.Count); }
  static void T7() { Span<int> s = [1, 2, 3]; Console.WriteLine(s.Length); }
  static void T8() { int[][] j = [[1], [2, 3]]; Console.WriteLine(j[1].Length); }
  static int Sum(int[] a) { int s = 0; foreach (var x in a) s += x; return s; }
  static void T9() { Console.WriteLine(Sum([1, 2, 3])); }
  static void T10() { var d = new Dictionary<string, int[]> { ["a"] = [1, 2] }; Console.WriteLine(d["a"].Length); }
  static int Count(params int[] xs) => xs.Length;
  static int CountList(params List<int> xs) => xs.Count;
  static void T11() { Console.WriteLine(Count() + " " + Count(1, 2, 3) + " " + Count(new[] { 1, 2 })); }
  static void T12() { Console.WriteLine(CountList(1, 2, 3)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
