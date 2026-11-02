using System;
class Program {
  static void T1() { int a = 7, b = 2; Console.WriteLine((a + b) + " " + (a - b) + " " + (a * b) + " " + (a / b) + " " + (a % b)); }
  static void T2() { int a = -7, b = 2; Console.WriteLine((a / b) + " " + (a % b) + " " + (7 / -2) + " " + (7 % -2) + " " + (-7 / -2) + " " + (-7 % -2)); }
  static void T3() { int z = 0; try { Console.WriteLine(1 / z); } catch (DivideByZeroException) { Console.WriteLine("dbz"); } }
  static void T4() { int z = 0; try { Console.WriteLine(1 % z); } catch (DivideByZeroException) { Console.WriteLine("dbz"); } }
  static void T5() { int m = int.MaxValue; Console.WriteLine(m + 1); Console.WriteLine(unchecked(int.MinValue - 1)); Console.WriteLine(m * 2); }
  static void T6() { Console.WriteLine(int.MaxValue + " " + int.MinValue + " " + unchecked(-int.MinValue) + " " + Math.Abs(int.MaxValue)); }
  static void T7() { int a = 100000; Console.WriteLine(a * a); Console.WriteLine(a * a / a); }
  static void T8() { int x = 5; x++; x--; ++x; Console.WriteLine(x + " " + (-x) + " " + (+x)); }
  static void T9() { Console.WriteLine(7 / 2 * 2.0 + " " + 7 / 2.0 + " " + (double)7 / 2); }
  static void T10() { int a = 1_000_000; Console.WriteLine(a + " " + 0x7F + " " + 0b1010 + " " + 017); }
  static void T11() { int mn = int.MinValue; int d = -1; try { Console.WriteLine(mn / d); } catch (OverflowException) { Console.WriteLine("ovf"); } }
  static void T12() { Console.WriteLine(int.Parse("123") + int.Parse("-5") + " " + int.TryParse("x", out var r) + r); }
  static void T13() { int h = 17; h = h * 31 + 5; h = h * 31 + 123456789; Console.WriteLine(h); }
  static void T14() { int x = 1 << 30; Console.WriteLine(x + x); Console.WriteLine(x * 4); Console.WriteLine(x << 1); }
  static void T15() { Console.WriteLine(Math.Abs(-5) + " " + Math.Max(3, 8) + " " + Math.Min(3, 8) + " " + Math.Sign(-9) + " " + Math.Clamp(15, 0, 10)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
