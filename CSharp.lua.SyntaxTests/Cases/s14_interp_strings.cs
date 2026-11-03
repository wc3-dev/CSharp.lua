using System;
using System.Globalization;
class Program {
  static void T1() { int a = 5; string s = "x"; Console.WriteLine($"{a} {s} {a + 1} {s.ToUpper()}"); }
  static void T2() { Console.WriteLine($"[{5,4}] [{5,-4}] [{"ab",5}] [{"ab",-5}]"); }
  static void T3() { Console.WriteLine($"{255:X} {255:x4} {10:D3} {1234.5f:F1} {0.5f:P0} {1234567:N0} {5:00}"); }
  static void T4() { Console.WriteLine($"{{literal}} {1}"); }
  static void T5() { var s = $"""raw {1 + 1} "q" """; Console.WriteLine(s); }
  static void T6() { Console.WriteLine($"{(1 > 0 ? "y" : "n")}"); }
  static void T7() { string n = null; Console.WriteLine($"[{n}]"); object o = null; Console.WriteLine($"[{o}]"); }
  static void T8() { Console.WriteLine($"{true} {'c'} {1.5f} {2L} {(byte)3}"); }
  static void T9() { Console.WriteLine(string.Format("{0} {1} {0}", "a", 2)); Console.WriteLine(string.Format("{0,5}|{1:F2}", "ab", 3.14159f)); }
  static void T10() { var x = 3; Console.WriteLine($"{x:F2} {x,5:F1} {x:E2}"); }
  static void T11() { Console.WriteLine($"{1.0f} {100f} {1e10f} {0.1f + 0.2f} {1f / 3}"); }
  static void T12() { Console.WriteLine(string.Concat(1, 'a', 2.5f, true)); Console.WriteLine("" + 1.0f + 2.0f); }
  static void T13() { Console.WriteLine($"{DayOfWeek.Monday} {DayOfWeek.Monday:D}"); }
  static void T14() { string s = $"{1}" + $"{2}"; Console.WriteLine(s); FormattableString fs = $"{1}"; Console.WriteLine(fs.Format); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
