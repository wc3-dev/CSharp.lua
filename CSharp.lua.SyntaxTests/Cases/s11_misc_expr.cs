using System;
class P { public int X; }
class Program {
  const int K = 5; const string KS = "k" + "s";
  static void T1() { Console.WriteLine(nameof(P) + " " + nameof(P.X) + " " + nameof(Console.WriteLine) + " " + nameof(T1)); }
  static void T2() { Console.WriteLine(typeof(int).Name + " " + typeof(P).Name + " " + typeof(string[]).Name); }
  static void T3() { Console.WriteLine(default(int) + " " + default(string) + " " + default(bool) + " " + (default(P) == null) + " " + default(char).Equals('\0')); }
  static void T4() { const int L = K * 2; Console.WriteLine(L + KS); }
  static void T5() { int x = 5; Console.WriteLine(x > 3 ? "big" : "small"); var s = x > 3 ? x > 4 ? "a" : "b" : "c"; Console.WriteLine(s); }
  static void T6() { int a = 5, b = 3; Console.WriteLine((a & b) + " " + (a | b) + " " + (a ^ b) + " " + (~a) + " " + (a << 2) + " " + (a >> 1)); }
  static void T7() { bool t = true, f = false; Console.WriteLine((t && f) + " " + (t || f) + " " + (t ^ f) + " " + (!t) + " " + (t & f) + " " + (t | f)); }
  static void T8() { int x = 5; x += 3; x -= 1; x *= 2; x /= 3; x %= 3; x <<= 3; x >>= 1; x |= 1; x &= 0xFF; x ^= 2; Console.WriteLine(x); }
  static void T9() { int x = 5; int y = x++ + ++x; Console.WriteLine(x + " " + y); int z = x-- - --x; Console.WriteLine(x + " " + z); }
  static void T10() { var p = new P(); p.X = 5; p.X++; ++p.X; p.X += p.X; Console.WriteLine(p.X); }
  static void T11() { object o = 5; var s = (o as string) ?? "nope"; Console.WriteLine(s); }
  static void T12() { int a = 3; a = a == 3 ? 4 : 5; Console.WriteLine(a); }
  static void T13() { string s = "a" + 1 + 2 + 'c' + 1.5f + true + null; Console.WriteLine(s); }
  static void T14() { int i = 1; string s = i + 2 + "x" + i + 2; Console.WriteLine(s); }
  static void T15() { var x = new[] { 1, 2 }; var (a, b) = (x[0], x[1]); Console.WriteLine(a + b); }
  static void T16() { Console.WriteLine(sizeof(int) + " " + sizeof(byte) + " " + sizeof(char)); }
  static void T17() { int x = 5; x = x switch { 5 => 6, _ => 0 }; Console.WriteLine(x); }
  static void T18() { var s = $"{K}{KS}"; Console.WriteLine(s); }
  static void T19() { int a = 10; int b = (a > 5) ? (a < 20 ? 1 : 2) : 3; Console.WriteLine(b); }
  static void T20() { long l = int.MaxValue; l++; Console.WriteLine(l); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
