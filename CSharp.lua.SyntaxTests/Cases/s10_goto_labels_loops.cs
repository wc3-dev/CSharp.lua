using System;
class Program {
  static void T1() { int i = 0; top: if (i < 3) { Console.Write(i); i++; goto top; } Console.WriteLine(); }
  static void T2() { for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { if (j == 2) goto next; Console.Write(i + "" + j + " "); } next:; } Console.WriteLine(); }
  static void T3() { int i = 0; while (true) { if (++i > 3) break; if (i == 2) continue; Console.Write(i); } Console.WriteLine(); }
  static void T4() { int i = 0; do { Console.Write(i); i++; } while (i < 3); Console.WriteLine(); }
  static void T5() { for (int i = 0, j = 10; i < j; i += 3, j -= 3) Console.Write(i + ":" + j + " "); Console.WriteLine(); }
  static void T6() { for (;;) { break; } Console.WriteLine("ok"); }
  static void T7() { int n = 0; for (int i = 0; i < 5; i++) { if (i % 2 == 0) continue; n += i; } Console.WriteLine(n); }
  static void T8() { int x = 5; while (x-- > 0) { if (x == 2) break; } Console.WriteLine(x); }
  static void T9() { outer: for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { if (i == 1) goto outer2; } } outer2: Console.WriteLine("x"); }
  static void T10() { int i = 0; do { if (i == 1) { i += 5; continue; } i++; } while (i < 4); Console.WriteLine(i); }
  static void T11() { int s = 0; for (int i = 10; i > 0; i /= 2) s += i; Console.WriteLine(s); }
  static void T12() { string r = ""; for (char c = 'a'; c <= 'e'; c++) r += c; Console.WriteLine(r); }
  static void T13() { int i = 0; lbl: i++; if (i < 3) goto lbl; Console.WriteLine(i); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
