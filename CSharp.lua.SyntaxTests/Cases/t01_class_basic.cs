using System;
class A { public int X; public static int S = 5; public A(int x) { X = x; } public int Twice() => X * 2; public override string ToString() => "A" + X; }
class B : A { public B() : base(10) { } public new int Twice() => X * 3; }
class Program {
  static void T1() { var a = new A(3); A b = new B(); Console.WriteLine(a.Twice() + " " + b.Twice() + " " + ((B)b).Twice() + " " + A.S + " " + a + " " + b); }
  static void T2() { var a = new A(3); A b = new B(); Console.WriteLine(a.Equals(a) + " " + a.Equals(new A(3)) + " " + (a == a) + " " + ReferenceEquals(a, b)); }
  static void T3() { var a = new A(3); A b = new B(); Console.WriteLine(a.GetType().Name + " " + b.GetType().Name + " " + (b is A) + " " + (a is B)); }
  static void T4() { object o = new A(1); Console.WriteLine(o.GetType() == typeof(A)); Console.WriteLine(o is A aa ? aa.X : -1); Console.WriteLine((o as B) == null); }
  static void T5() { Console.WriteLine(typeof(A).FullName + " " + typeof(int).Name + " " + typeof(string).Name + " " + new A(1).GetType().FullName); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
