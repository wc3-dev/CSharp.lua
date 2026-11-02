using System;
class Program {
  static void T1() { decimal a = 0.1m, b = 0.2m; Console.WriteLine(a + b); Console.WriteLine(a + b == 0.3m); }
  static void T2() { decimal d = 10m / 3; Console.WriteLine(d); Console.WriteLine(Math.Round(d, 2)); }
  static void T3() { decimal d = 1.50m; Console.WriteLine(d + " " + (d * 2) + " " + (int)d + " " + (double)d); }
  static void T4() { Console.WriteLine(decimal.MaxValue + " " + decimal.Parse("123.456") + " " + decimal.Zero + " " + decimal.One); }
  static void T5() { decimal d = 5; d++; d *= 1.1m; Console.WriteLine(d); }
  static void T6() { Console.WriteLine(decimal.Round(2.5m) + " " + decimal.Floor(-1.5m) + " " + decimal.Ceiling(1.2m) + " " + decimal.Truncate(2.9m) + " " + Math.Abs(-2.5m)); }
  static void T7() { decimal a = 79228162514264337593543950335m; Console.WriteLine(a); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
