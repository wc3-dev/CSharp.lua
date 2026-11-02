using System;
delegate int Op(int a, int b);
class Program {
  static int Add(int a, int b) => a + b;
  int k = 10; int AddK(int a, int b) => a + b + k;
  static void T1() { Op o = Add; Op p = new Program().AddK; Op q = (a, b) => a * b; Console.WriteLine(o(1, 2) + " " + p(1, 2) + " " + q(3, 4)); }
  static void T2() { Op o = Add; Op p = new Program().AddK; Op all = o + p; Console.WriteLine(all(1, 1)); }
  static void T3() { Op o = Add; Op p = new Program().AddK; Op all = o + p; Console.WriteLine(all.GetInvocationList().Length); all -= o; Console.WriteLine(all.GetInvocationList().Length); }
  static void T4() { Action act = null; act += () => Console.Write("a"); act += () => Console.Write("b"); act(); Console.WriteLine(); }
  static void T5() { Func<int, Func<int, int>> curry = a => b => a + b; Console.WriteLine(curry(3)(4)); }
  static void T6() { Op n = null; Console.WriteLine(n == null); Console.WriteLine(n?.Invoke(1, 2)); }
  static void T7() { Op o = Add; Console.WriteLine(o.Invoke(2, 3) + " " + o.Target == null); }
  static void T8() { Op o = Add; Op p = Add; Console.WriteLine(o == p); }
  static void T9() { Action a = () => { }; Console.WriteLine(a != null); a = null; Console.WriteLine(a == null); }
  static void T10() { Op o = Add; var r = o.BeginInvoke == null; Console.WriteLine("x"); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
