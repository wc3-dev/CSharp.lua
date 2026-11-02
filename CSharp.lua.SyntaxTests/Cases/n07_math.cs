using System;
class Program {
  static void T1() { Console.WriteLine(Math.Floor(2.7) + " " + Math.Ceiling(2.1) + " " + Math.Floor(-2.7) + " " + Math.Ceiling(-2.1) + " " + Math.Truncate(-2.7)); }
  static void T2() { Console.WriteLine(Math.Sqrt(16) + " " + Math.Pow(2, 10) + " " + Math.Pow(2, 0.5) + " " + Math.Pow(2, -1)); }
  static void T3() { Console.WriteLine(Math.Sin(0) + " " + Math.Cos(0) + " " + Math.Tan(0) + " " + Math.Atan2(1, 1) + " " + Math.Atan(1) + " " + Math.Asin(1) + " " + Math.Acos(1)); }
  static void T4() { Console.WriteLine(Math.Exp(0) + " " + Math.Log(1) + " " + Math.Log10(1000) + " " + Math.Log(8, 2) + " " + Math.Log2(8)); }
  static void T5() { Console.WriteLine(Math.PI + " " + Math.E + " " + Math.Tau); }
  static void T6() { Console.WriteLine(Math.Abs(-2.5) + " " + Math.Abs(-2.5f) + " " + Math.Max(1.5, 2) + " " + Math.Min(-1f, 2f) + " " + Math.Sign(-2.5) + " " + Math.Clamp(5.5, 1, 3)); }
  static void T7() { Console.WriteLine(Math.Floor(2.7f) + " " + Math.Sqrt(2f) + " " + (float)Math.Sqrt(2) + " " + MathF.Sqrt(2f)); }
  static void T8() { Console.WriteLine(MathF.PI + " " + MathF.Sin(1f) + " " + MathF.Cos(1f) + " " + MathF.Atan2(1f, 2f) + " " + MathF.Pow(2f, 3f) + " " + MathF.Abs(-1f) + " " + MathF.Floor(1.5f) + " " + MathF.Round(2.5f) + " " + MathF.Max(1f, 2f)); }
  static void T9() { Console.WriteLine(Math.DivRem(17, 5, out int r) + " " + r); Console.WriteLine(Math.BigMul(100000, 100000)); }
  static void T10() { Console.WriteLine(Math.Sinh(1) + " " + Math.Cosh(1) + " " + Math.Tanh(1) + " " + Math.Cbrt(27)); }
  static void T11() { Console.WriteLine(Math.IEEERemainder(10, 3) + " " + Math.Round(1234.5678, -2) + " " + Math.ScaleB(1, 3) + " " + Math.FusedMultiplyAdd(2, 3, 4)); }
  static void T12() { Console.WriteLine(Math.Sqrt(-1) + " " + Math.Log(0) + " " + Math.Log(-1) + " " + Math.Pow(0, 0)); }
  static void T13() { Console.WriteLine(Math.Ceiling(5 / 2.0) + " " + (int)Math.Ceiling(5 / 2.0) + " " + (int)Math.Floor(-0.5) + " " + (int)Math.Round(0.5) + " " + (int)Math.Round(1.5)); }
  static void T14() { Console.WriteLine(Math.Min(int.MaxValue, 5L) + " " + Math.Max((byte)3, (byte)9) + " " + Math.Abs(-7L) + " " + Math.Abs((short)-3)); }
  static void T15() { Console.WriteLine(Math.Pow(10, 9) + " " + Math.Pow(10, 15) + " " + Math.Pow(10, 16) + " " + (int)Math.Pow(2, 30) + " " + (long)Math.Pow(2, 40)); }
  static void T16() { Console.WriteLine(Math.Round(1.005, 2) + " " + Math.Round(2.345f, 2) + " " + Math.Round(-0.5) + " " + Math.Round(0.4999999)); }
  static void T17() { var r = new Random(42); int a = r.Next(); int b = r.Next(10); double c = r.NextDouble(); Console.WriteLine((a >= 0) + " " + (b < 10) + " " + (c < 1)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
