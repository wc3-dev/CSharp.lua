using System;
interface I { int V(); int W() => V() + 1; static int Z = 4; static int Make() => 9; }
class C : I { public int V() => 5; }
class D : I { public int V() => 7; public int W() => 100; }
interface IA { string Name { get; } }
class N : IA { public string Name => "n"; }
interface IB { int Q { get; set; } void Do() { Console.WriteLine("default Do"); } }
class E : IB { public int Q { get; set; } }
class Program {
  static void T1() { I c = new C(); I d = new D(); Console.WriteLine(c.W() + " " + d.W()); }
  static void T2() { Console.WriteLine(I.Z + " " + I.Make()); }
  static void T3() { IA a = new N(); Console.WriteLine(a.Name); }
  static void T4() { IB b = new E(); b.Do(); b.Q = 3; Console.WriteLine(b.Q); }
  static void T5() { object o = new C(); Console.WriteLine((o is I) + " " + (o is IA) + " " + (o as I).V()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
