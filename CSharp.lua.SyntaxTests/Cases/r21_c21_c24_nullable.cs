using System;
class Program {
  static void A() { int? n = 5; int? m = null; bool? t = true; float? f = 1.5f; char? ch = 'A'; Console.WriteLine("n=" + n + "|" + m + "|" + t + "|" + f + "|" + ch); }
  static void B() { int? n = 5; int? m = null; string s = "x"; s += n; s += m; Console.WriteLine(s); }
  static void C() { int? n = 5; int? m = null; float? f = 1.5f; Console.WriteLine($"{n}|{m}|{n,3}|{f:F1}"); }
  static void D() { int? n = 5; int? m = null; Console.WriteLine(n.GetValueOrDefault() + m.GetValueOrDefault() + " " + m.GetValueOrDefault(7)); }
  static void E() { DayOfWeek? e = DayOfWeek.Monday; DayOfWeek? e2 = null; Console.WriteLine(e?.ToString() + "|" + e2?.ToString() + "|" + e); }
  static void F() { int? n = 5; int? m = null; bool c = true; int? r = c ? m : 3; int? r2 = c ? n : 3; Console.WriteLine(r.HasValue + " " + r2); }
  static void G() { bool? bf = false; bool? rr = true ? bf : null; Console.WriteLine(rr); }
  static void Main() { H.R("A", A); H.R("B", B); H.R("C", C); H.R("D", D); H.R("E", E); H.R("F", F); H.R("G", G); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
