using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  static IEnumerable<int> Count(int n) { for (int i = 0; i < n; i++) yield return i; }
  static IEnumerable<int> Evens(IEnumerable<int> src) { foreach (var x in src) if (x % 2 == 0) yield return x; }
  static IEnumerable<int> WithBreak() { yield return 1; yield return 2; yield break; }
  static IEnumerable<string> Try() { try { yield return "a"; yield return "b"; } finally { Console.WriteLine("fin"); } }
  static IEnumerator<int> Enum() { yield return 7; yield return 8; }
  static IEnumerable<int> Fib() { int a = 0, b = 1; while (true) { yield return a; (a, b) = (b, a + b); } }
  static IEnumerable<int> Rec(int n) { if (n > 0) { yield return n; foreach (var x in Rec(n - 1)) yield return x; } }
  static IEnumerable<int> Lazy() { Console.WriteLine("start"); yield return 1; Console.WriteLine("end"); }
  static void T1() { foreach (var x in Count(3)) Console.Write(x); Console.WriteLine(); }
  static void T2() { Console.WriteLine(string.Join(",", Evens(Count(10)))); }
  static void T3() { Console.WriteLine(WithBreak().Count()); }
  static void T4() { foreach (var s in Try()) Console.WriteLine(s); }
  static void T5() { var e = Enum(); while (e.MoveNext()) Console.Write(e.Current); Console.WriteLine(); }
  static void T6() { Console.WriteLine(string.Join(",", Fib().Take(8))); }
  static void T7() { Console.WriteLine(string.Join(",", Rec(3))); }
  static void T8() { var e = Lazy(); Console.WriteLine("created"); foreach (var x in e) Console.WriteLine(x); }
  static void T9() { foreach (var s in Try()) { Console.WriteLine(s); break; } }
  static void T10() { var e = Count(2); Console.WriteLine(e.Count() + e.Count()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
