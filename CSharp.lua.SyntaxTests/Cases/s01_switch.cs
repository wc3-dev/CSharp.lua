using System;
class Program {
  static string T1_S(int x) { switch (x) { case 1: return "one"; case 2: case 3: return "two-three"; default: return "other"; } }
  static string T2_S(string s) { switch (s) { case "a": return "A"; case "b": return "B"; default: return "?"; } }
  static string T3_S(int x) => x switch { 1 => "one", 2 or 3 => "twothree", > 10 => "big", < 0 => "neg", _ => "other" };
  static string T4_S(object o) => o switch { int i when i > 5 => "bigint", int i => "int" + i, string s => "str" + s, null => "null", _ => "obj" };
  static string T5_S(char c) { switch (c) { case 'a': return "a"; case >= 'b' and <= 'z': return "lower"; default: return "x"; } }
  static void T1() { Console.WriteLine(T1_S(1) + T1_S(3) + T1_S(9)); }
  static void T2() { Console.WriteLine(T2_S("a") + T2_S("b") + T2_S("c")); }
  static void T3() { Console.WriteLine(T3_S(1) + T3_S(3) + T3_S(11) + T3_S(-1) + T3_S(5)); }
  static void T4() { Console.WriteLine(T4_S(9) + T4_S(2) + T4_S("z") + T4_S(null) + T4_S(2.5f)); }
  static void T5() { Console.WriteLine(T5_S('a') + T5_S('q') + T5_S('Z')); }
  static void T6() { int x = 2; switch (x) { case 1: Console.Write("1"); goto case 2; case 2: Console.Write("2"); goto default; default: Console.Write("d"); break; } Console.WriteLine(); }
  static void T7() { int x = 3; switch (x) { case 3: { int y = 5; Console.WriteLine(y); break; } } }
  static void T8() { var t = (1, "a"); var r = t switch { (1, "a") => "match", (_, _) => "no" }; Console.WriteLine(r); }
  static void T9() { string s = null; switch (s) { case null: Console.WriteLine("null"); break; default: Console.WriteLine("nn"); break; } }
  static void T10() { for (int i = 0; i < 4; i++) { switch (i) { case 1: continue; case 3: break; default: Console.Write(i); break; } } Console.WriteLine(); }
  static void T11() { DayOfWeek d = DayOfWeek.Friday; var s = d switch { DayOfWeek.Saturday or DayOfWeek.Sunday => "weekend", _ => "week" }; Console.WriteLine(s); }
  static void T12() { int x = 5; var s = x switch { var n when n % 2 == 0 => "even", var n => "odd" + n }; Console.WriteLine(s); }
  static void T13() { try { int x = 7; var s = x switch { 1 => "a" }; Console.WriteLine(s); } catch (Exception) { Console.WriteLine("nomatch"); } }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
