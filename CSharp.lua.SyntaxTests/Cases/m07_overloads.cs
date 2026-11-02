using System;
class Program {
  static string F(int x) => "int"; static string F(long x) => "long"; static string F(float x) => "float"; static string F(string x) => "string"; static string F(object x) => "object"; static string F(int x, int y) => "int,int";
  static string G(params int[] xs) => "params" + xs.Length; static string G(int x) => "one";
  static string H2(int a, int b = 5, string c = "d") => a + "," + b + "," + c;
  static void T1() { Console.WriteLine(F(1) + F(1L) + F(1f) + F("s") + F(new object()) + F(1, 2)); }
  static void T2() { Console.WriteLine(F((short)1) + F('c') + F(2.5) + F(null as string)); }
  static void T3() { Console.WriteLine(G() + G(1) + G(1, 2) + G(new[] { 1, 2, 3 })); }
  static void T4() { Console.WriteLine(H2(1) + " " + H2(1, 2) + " " + H2(1, c: "z") + " " + H2(c: "q", a: 3, b: 4)); }
  static void T5() { Console.WriteLine(string.Concat("a", "b") + string.Concat("a", "b", "c") + string.Concat(new[] { "x", "y" })); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
