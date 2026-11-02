using System;
class A { public int X, Y; public A() : this(1) { Console.WriteLine("A()"); } public A(int x) : this(x, 2) { Console.WriteLine("A(int)"); } public A(int x, int y) { X = x; Y = y; Console.WriteLine("A(int,int)"); } }
class B : A { public int Z = 5; public B() : base(7) { Console.WriteLine("B() " + X + Z); } }
abstract class P { protected P() { Console.WriteLine("P ctor " + Name()); } public abstract string Name(); }
class Q : P { string n = "q"; public Q() { Console.WriteLine("Q ctor"); } public override string Name() => n; }
class Program {
  static void T1() { var a = new A(); Console.WriteLine(a.X + " " + a.Y); }
  static void T2() { var b = new B(); Console.WriteLine(b.Z); }
  static void T3() { new Q(); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
