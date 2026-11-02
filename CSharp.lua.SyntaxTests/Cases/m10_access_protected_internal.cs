using System;
class A { protected int p = 1; internal int i = 2; private int q = 3; protected internal int pi = 4; private protected int pp = 5; public int Q() => q; }
class B : A { public int Sum() => p + i + pi + pp; }
class Program {
  static void T1() { var b = new B(); Console.WriteLine(b.Sum() + " " + b.i + " " + b.Q()); }
  static void Main() { H.R("T1", T1); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
