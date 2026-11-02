using System;
using System.Collections.Generic;
class P { public int X; public string N; public P Inner; }
record Pt(int X, int Y);
class Program {
  static void T1() { object o = new P { X = 3, N = "n" }; Console.WriteLine(o is P { X: 3 }); Console.WriteLine(o is P { X: > 5 }); Console.WriteLine(o is P { N: "n", X: < 10 }); }
  static void T2() { var p = new P { Inner = new P { X = 9 } }; Console.WriteLine(p is { Inner.X: 9 }); Console.WriteLine(p is { Inner: null }); }
  static void T3() { int[] a = { 1, 2, 3 }; Console.WriteLine(a is [1, 2, 3]); Console.WriteLine(a is [1, ..]); Console.WriteLine(a is [_, _]); Console.WriteLine(a is [.., 3]); }
  static void T4() { int[] a = { 1, 2, 3, 4 }; if (a is [var first, .. var rest]) Console.WriteLine(first + " " + rest.Length); }
  static void T5() { object o = 5; Console.WriteLine((o is int) + " " + (o is not string) + " " + (o is int i && i > 3) + " " + (o is not null)); }
  static void T6() { object o = "s"; if (o is string s && s.Length == 1) Console.WriteLine("one char " + s); if (!(o is int n)) Console.WriteLine("not int"); }
  static void T7() { var p = new Pt(1, 2); Console.WriteLine(p is (1, 2)); Console.WriteLine(p is { X: 1, Y: var y } ? y : -1); Console.WriteLine(p is Pt(var a, var b) ? a + b : 0); }
  static void T8() { int x = 5; Console.WriteLine(x is > 1 and < 10); Console.WriteLine(x is 1 or 2 or 5); Console.WriteLine(x is not (1 or 2)); }
  static void T9() { (int, int) t = (1, 2); Console.WriteLine(t is (1, _)); Console.WriteLine(t is (> 0, > 5)); }
  static void T10() { string s = "abc"; Console.WriteLine(s is { Length: 3 }); Console.WriteLine(s is "abc"); Console.WriteLine(s is ['a', .., 'c']); }
  static void T11() { object[] os = { 1, "a", 2.5f, null, new P(), 'c', true }; foreach (var o in os) Console.Write(T11_Kind(o) + ","); Console.WriteLine(); }
  static string T11_Kind(object o) => o switch { int => "int", string => "string", float => "float", null => "null", P => "P", char => "char", bool => "bool", _ => "?" };
  static void T12() { object o = 3; var r = o switch { int i when i > 5 => "gt5", int i when i > 2 => "gt2", _ => "else" }; Console.WriteLine(r); }
  static void T13() { List<int> l = new() { 1, 2 }; Console.WriteLine(l is [1, 2]); Console.WriteLine(l is { Count: 2 }); }
  static void T14() { char c = 'x'; Console.WriteLine(c is >= 'a' and <= 'z'); Console.WriteLine(c is 'x' or 'y'); }
  static void T15() { int? n = 3; Console.WriteLine(n is 3); Console.WriteLine(n is null); Console.WriteLine(n is int v ? v : -1); n = null; Console.WriteLine(n is null); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
