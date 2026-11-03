using System;
class Program {
  static void T1() { float a = 0.1f, b = 0.2f; Console.WriteLine(a + b); Console.WriteLine((double)a + " " + (double)(a + b)); }
  static void T2() { double a = 0.1, b = 0.2; Console.WriteLine(a + b); Console.WriteLine(a + b == 0.3); }
  static void T3() { float f = 1f / 3; double d = 1.0 / 3; Console.WriteLine(f + " " + d); }
  static void T4() { Console.WriteLine(float.MaxValue + " " + float.MinValue + " " + float.Epsilon + " " + double.MaxValue + " " + double.Epsilon); }
  static void T5() { Console.WriteLine(float.NaN + " " + float.PositiveInfinity + " " + float.NegativeInfinity + " " + (0f / 0f) + " " + (1f / 0f) + " " + (-1f / 0f)); }
  static void T6() { float n = float.NaN; Console.WriteLine((n == n) + " " + float.IsNaN(n) + " " + (n != n) + " " + (n < 1) + " " + float.IsInfinity(1f / 0)); }
  static void T7() { Console.WriteLine(1e10f + " " + 1.5e-5f + " " + 123456789f + " " + 0.00001f + " " + 1e-10 + " " + 1e21 + " " + 1e15 + " " + 123456789012345.0); }
  static void T8() { Console.WriteLine(100f + " " + 100.0 + " " + 1.0f + " " + (float)3 + " " + -0f + " " + (0f == -0f)); }
  static void T9() { Console.WriteLine(3.14159265358979 + " " + 2.718281828f + " " + 1.0 / 7 + " " + 1f / 7); }
  static void T10() { Console.WriteLine((int)3.7f + " " + (int)-3.7f + " " + (int)3.7 + " " + (long)1e10 + " " + unchecked((int)1e10) + " " + unchecked((int)float.NaN)); }
  static void T11() { Console.WriteLine(5 / 2f + " " + 5 % 2.5f + " " + 5.5f % 2 + " " + -5.5 % 2 + " " + 5.5 % -2); }
  static void T12() { float f = 16777216f; Console.WriteLine(f + 1 + " " + (f + 1 == f)); double d = 9007199254740992.0; Console.WriteLine(d + 1 == d); }
  static void T13() { Console.WriteLine(float.Parse("1.5") + double.Parse("2.25") + " " + float.TryParse("abc", out var r) + r + " " + double.Parse("1e3")); }
  static void T14() { float f = 2.5f; f *= 2; f += 1; f /= 4; f -= 0.5f; Console.WriteLine(f); f++; Console.WriteLine(f); }
  static void T15() { float a = 3; double b = a; float c = (float)b; Console.WriteLine(a + " " + b + " " + c + " " + (a == b)); }
  static void T16() { Console.WriteLine((float)0.1 + " " + (double)0.1f + " " + (float)1e40 + " " + (float)16777217); }
  static void T17() { Console.WriteLine(1.0f * 3 + " " + 6.0f / 2 + " " + (2f + 1) + " " + (10 - 2.5f)); }
  static void T18() { float x = 7; int i = (int)(x / 2); Console.WriteLine(i + " " + x / 2); }
  static void T19() { double d = 1234.5678; Console.WriteLine(d.ToString("F2") + " " + d.ToString("0.0") + " " + ((float)d).ToString("F1") + " " + d.ToString("N1") + " " + d.ToString("E3")); }
  static void T20() { Console.WriteLine(Math.Round(2.5) + " " + Math.Round(3.5) + " " + Math.Round(-2.5) + " " + Math.Round(2.567, 2) + " " + Math.Round(2.5, MidpointRounding.AwayFromZero)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
