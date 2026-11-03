using System;
using System.Collections.Generic;
class Program {
  static (int, string) F() => (1, "a");
  static (int Min, int Max) MM(int[] a) { int mn = a[0], mx = a[0]; foreach (var x in a) { if (x < mn) mn = x; if (x > mx) mx = x; } return (mn, mx); }
  static void T1() { var t = F(); Console.WriteLine(t.Item1 + t.Item2); }
  static void T2() { var (a, b) = F(); Console.WriteLine(a + b); }
  static void T3() { var r = MM(new[] { 3, 1, 4 }); Console.WriteLine(r.Min + " " + r.Max); }
  static void T4() { var r = MM(new[] { 3, 1, 4 }); Console.WriteLine(r); }
  static void T5() { (int x, int y) = (1, 2); (x, y) = (y, x); Console.WriteLine(x + "" + y); }
  static void T6() { var t2 = (A: 1, B: 2); Console.WriteLine(t2.A + t2.B); Console.WriteLine(t2 == (1, 2)); Console.WriteLine(t2.Equals((1, 2))); }
  static void T7() { var d = new Dictionary<(int, int), string>(); d[(1, 2)] = "x"; Console.WriteLine(d[(1, 2)] + d.ContainsKey((2, 1))); }
  static void T8() { var (m, _) = F(); Console.WriteLine(m); }
  static void T9() { Console.WriteLine(Tuple.Create(1, "z").Item2); }
  static void T10() { var t = (1, "a", 2.5f); Console.WriteLine(t.Item3 + " " + t); }
  static void T11() { var t = (1, 2); var u = t; u.Item1 = 9; Console.WriteLine(t.Item1 + " " + u.Item1); }
  static void T12() { var l = new List<(string Name, int Age)> { ("a", 1), ("b", 2) }; foreach (var (n, a) in l) Console.Write(n + a); Console.WriteLine(); }
  static void T13() { var t = (1, 2); Console.WriteLine(t.GetHashCode() == (1, 2).GetHashCode()); Console.WriteLine(t.CompareTo((1, 3))); }
  static void T14() { int a, b; (a, b) = (3, 4); Console.WriteLine(a * b); }
  static void T15() { var p = new Point(3, 4); var (x, y) = p; Console.WriteLine(x + y); }
  class Point { int x, y; public Point(int x, int y) { this.x = x; this.y = y; } public void Deconstruct(out int a, out int b) { a = x; b = y; } }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
