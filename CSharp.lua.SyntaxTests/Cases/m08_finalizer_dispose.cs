using System;
class R : IDisposable { string n; public R(string n) { this.n = n; Console.WriteLine("open " + n); } public void Dispose() { Console.WriteLine("close " + n); } }
class Program {
  static void T1() { using (var r = new R("a")) { Console.WriteLine("body"); } Console.WriteLine("after"); }
  static void T2() { using var r = new R("b"); using var s = new R("c"); Console.WriteLine("body2"); }
  static void T3() { try { using (new R("d")) { throw new Exception("x"); } } catch (Exception e) { Console.WriteLine("caught " + e.Message); } }
  static void T4() { using (new R("e")) using (new R("f")) { Console.WriteLine("nest"); } }
  static void T5() { R r = null; using (r) { Console.WriteLine("null ok"); } }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
