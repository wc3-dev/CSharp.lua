using System;
class A { public static int X = Init("A.X"); public int I = Init("A.I"); static A() { Console.WriteLine("A.cctor"); } public A() { Console.WriteLine("A.ctor"); }
  public static int Init(string n) { Console.WriteLine(n); return 1; } }
class B : A { public static int Y = Init("B.Y"); public int J = Init("B.J"); static B() { Console.WriteLine("B.cctor"); } public B() : base() { Console.WriteLine("B.ctor"); } }
class Lazy { public static readonly int V = Compute(); static int Compute() { Console.WriteLine("compute"); return 42; } }
class Program {
  static void T1() { Console.WriteLine("start"); new B(); Console.WriteLine("end"); }
  static void T2() { Console.WriteLine("before"); Console.WriteLine(Lazy.V); Console.WriteLine(Lazy.V); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
