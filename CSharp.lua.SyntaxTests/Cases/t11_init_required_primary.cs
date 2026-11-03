using System;
class P { public required string Name { get; init; } public int Age { get; init; } = 7; }
class Pt(int x, int y) { public int X { get; } = x; public int Sum() => x + y; }
class Pt2(int x) { public int X = x; }
class Pt3(int x) { public int Get() => x; }
struct Ps(int a) { public int A = a; public int Double() => a * 2; }
class Base(string n) { public string N = n; }
class Der(string n, int k) : Base(n) { public int K = k; }
class Der2(string n) : Base(n) { public string M() => n + N; }
class Program {
  static void T1() { var p = new P { Name = "n" }; Console.WriteLine(p.Name + p.Age); }
  static void T2() { var q = new Pt(1, 2); Console.WriteLine(q.X + " " + q.Sum()); }
  static void T3() { Console.WriteLine(new Pt2(5).X); }
  static void T4() { Console.WriteLine(new Pt3(5).Get()); }
  static void T5() { Console.WriteLine(new Ps(4).Double() + " " + new Ps(5).A); }
  static void T6() { var d = new Der("nn", 3); Console.WriteLine(d.N + d.K); }
  static void T7() { Console.WriteLine(new Der2("q").M()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
