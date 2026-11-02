using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  int f = 100;
  static void T1() { int Add(int a, int b) => a + b; Console.WriteLine(Add(1, 2)); }
  static void T2() { int total = 0; void Inc() { total++; } Inc(); Inc(); Console.WriteLine(total); }
  static void T3() { int Fact(int n) => n <= 1 ? 1 : n * Fact(n - 1); Console.WriteLine(Fact(5)); }
  static void T4() { var acts = new List<Action>(); for (int i = 0; i < 3; i++) acts.Add(() => Console.Write(i)); foreach (var a in acts) a(); Console.WriteLine(); }
  static void T5() { var acts = new List<Action>(); foreach (var i in new[] { 0, 1, 2 }) acts.Add(() => Console.Write(i)); foreach (var a in acts) a(); Console.WriteLine(); }
  static void T6() { var acts = new List<Action>(); for (int i = 0; i < 3; i++) { int j = i; acts.Add(() => Console.Write(j)); } foreach (var a in acts) a(); Console.WriteLine(); }
  static void T7() { Func<int, int> sq = static x => x * x; Console.WriteLine(sq(5)); }
  static void T8() { int n = 0; Func<int> next = () => ++n; next(); next(); Console.WriteLine(n + " " + next()); }
  static void T9() { Func<int, int> f = null; f = x => x <= 0 ? 0 : x + f(x - 1); Console.WriteLine(f(4)); }
  void T10() { Func<int> g = () => f + 1; f = 5; Console.WriteLine(g()); }
  static void T11() { Func<int, int, int> add = (a, b) => a + b; Func<int, int> inc = a => add(a, 1); Console.WriteLine(inc(4)); }
  static void T12() { Action<string> p = s => { Console.Write(s); Console.Write("!"); }; p("x"); Console.WriteLine(); }
  static void T13() { var l = new[] { 3, 1, 2 }.ToList(); l.Sort((a, b) => b.CompareTo(a)); Console.WriteLine(string.Join(",", l)); }
  static void T14() { int x = 1; { int y = 2; Func<int> f = () => x + y; x = 10; Console.WriteLine(f()); } }
  static void T15() { Func<int> make() { int c = 0; return () => ++c; } var a = make(); var b = make(); a(); a(); Console.WriteLine(a() + " " + b()); }
  static void T16() { var f = (int a, int b = 2) => a + b; Console.WriteLine(f(1)); }
  static void T17() { var f = (params int[] xs) => xs.Length; Console.WriteLine(f(1, 2, 3)); }
  static void T18() { var f = int (int a) => a * 2; Console.WriteLine(f(4)); }
  static void T19() { int k = 3; static int Twice(int a) => a * 2; Console.WriteLine(Twice(k)); }
  static void T20() { Func<int, bool> even = x => x % 2 == 0; Console.WriteLine(new[] { 1, 2, 3, 4 }.Count(even)); }
  static void T21() { Action a = () => { }; Action b = a; Console.WriteLine(a == b); }
  static void T22() { Func<Func<int>> f = () => () => 5; Console.WriteLine(f()()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); new Program().T10(); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
