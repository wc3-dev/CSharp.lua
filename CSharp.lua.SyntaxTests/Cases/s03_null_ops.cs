using System;
class N { public int V = 1; public N Next; public string Name; public int[] Arr; public int Get() => V; }
class Program {
  static void T1() { string a = null; Console.WriteLine(a ?? "dflt"); a ??= "set"; Console.WriteLine(a); a ??= "again"; Console.WriteLine(a); }
  static void T2() { N n = null; Console.WriteLine(n?.V); Console.WriteLine(n?.Next?.Name ?? "none"); Console.WriteLine(n?.Get()); }
  static void T3() { N n = new N { Next = new N { Name = "deep" } }; Console.WriteLine(n?.Next?.Name); Console.WriteLine(n.Next?.Next?.Name == null); }
  static void T4() { N n = new N(); Console.WriteLine(n.Arr?[0]); n.Arr = new[] { 4 }; Console.WriteLine(n.Arr?[0]); }
  static void T5() { int? x = null; Console.WriteLine(x ?? 5); x = 3; Console.WriteLine(x ?? 5); int? y = x ?? null; Console.WriteLine(y); }
  static void T6() { int? a = 1, b = null; Console.WriteLine(a + b); Console.WriteLine(a + 1); Console.WriteLine((a < b) + " " + (a > b) + " " + (a == b) + " " + (b == null)); }
  static void T7() { string s = null; Console.WriteLine(s?.Length ?? -1); Console.WriteLine((s?.Length).HasValue); }
  static void T8() { N n = null; if (n != null) n.Next = null; Console.WriteLine("ok8"); }
  static void T9() { string s = "x"; string t = s!; Console.WriteLine(t); }
  static void T10() { int? a = 5; int b = a.Value; int c = (int)a; Console.WriteLine(b + c + " " + a.HasValue); int? d = null; try { var e = d.Value; } catch (InvalidOperationException) { Console.WriteLine("ioe"); } }
  static void T11() { bool? a = true, b = null; Console.WriteLine((a & b) + " " + (a | b) + " " + (!a) + " " + (b ?? false)); }
  static void T12() { var l = new System.Collections.Generic.List<string>(); string f = l.Count > 0 ? l[0] : null; Console.WriteLine(f ?? "empty"); }
  static void T13() { N n = new N(); n.Name ??= "d"; n.Name ??= "e"; Console.WriteLine(n.Name); int? c = null; c ??= 4; Console.WriteLine(c); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
