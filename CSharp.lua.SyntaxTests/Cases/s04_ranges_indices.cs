using System;
using System.Collections.Generic;
class Program {
  static void T1() { int[] a = { 1, 2, 3, 4, 5 }; Console.WriteLine(a[^1] + " " + a[^2]); }
  static void T2() { int[] a = { 1, 2, 3, 4, 5 }; var s = a[1..3]; Console.WriteLine(s.Length + " " + s[0] + s[1]); }
  static void T3() { int[] a = { 1, 2, 3, 4, 5 }; var s = a[..2]; var t = a[3..]; var u = a[^2..]; Console.WriteLine(s.Length + " " + t.Length + " " + u.Length + " " + u[0]); }
  static void T4() { string s = "hello"; Console.WriteLine(s[1..3] + " " + s[^1] + " " + s[..2] + " " + s[2..]); }
  static void T5() { Index i = ^2; Range r = 1..3; Console.WriteLine(i.Value + " " + i.IsFromEnd + " " + r.Start.Value + " " + r.End.Value); }
  static void T6() { var l = new List<int> { 1, 2, 3 }; Console.WriteLine(l[^1]); }
  static void T7() { int[] a = { 1, 2, 3, 4, 5 }; Range r = 1..^1; var s = a[r]; Console.WriteLine(s.Length); }
  static void T8() { int[] a = { 1, 2, 3 }; a[^1] = 9; Console.WriteLine(a[2]); }
  static void T9() { string s = "hello"; Index i = 1; Console.WriteLine(s[i]); Console.WriteLine(s.Substring(1).Length); }
  static void T10() { int[] a = { 1, 2, 3 }; var all = a[..]; all[0] = 9; Console.WriteLine(a[0]); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
