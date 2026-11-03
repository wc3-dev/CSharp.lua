using System;
using System.Collections.Generic;
struct S { public int A; public int B; }
class C { public int A; public List<int> L = new(); public C Inner; }
class Program {
  static void T1() { var s = new S { A = 1, B = 2 }; var t = s with { B = 5 }; Console.WriteLine(s.A + "" + s.B + " " + t.A + t.B); }
  static void T2() { var c = new C { A = 1, L = { 1, 2 }, Inner = new C { A = 2 } }; Console.WriteLine(c.L.Count + " " + c.Inner.A); }
  static void T3() { var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }; Console.WriteLine(d.Count); var e = new Dictionary<string, int> { { "x", 1 } }; Console.WriteLine(e["x"]); }
  static void T4() { var a = new int[] { 1, 2, 3 }; var b = new[] { "a", "b" }; var c = new int[3]; var d = new int[2, 2] { { 1, 2 }, { 3, 4 } }; Console.WriteLine(a.Length + b.Length + c.Length + d.Length + " " + d[1, 0]); }
  static void T5() { C c = new(); C[] cs = { new(), new() }; List<C> l = new() { new C() }; Console.WriteLine(cs.Length + l.Count); }
  static void T6() { var c = new C(); c.Inner = new C(); c.Inner.A = 4; Console.WriteLine(c.Inner.A); }
  static void T7() { S s = default; s.A = 3; Console.WriteLine(s.A + s.B); S t = new(); Console.WriteLine(t.A); }
  static void T8() { var o = new { A = 1 }; var p = o with { A = 2 }; Console.WriteLine(o.A + " " + p.A); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
