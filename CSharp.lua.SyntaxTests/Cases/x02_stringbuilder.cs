using System;
using System.Text;
class Program {
  static void T1() { var sb = new StringBuilder(); sb.Append("a").Append(1).Append('c').Append(2.5f).Append(true).Append((string)null); Console.WriteLine(sb.ToString() + " " + sb.Length); }
  static void T2() { var sb = new StringBuilder("hello"); sb.Insert(0, "oh ").Replace("l", "L").Remove(0, 1); Console.WriteLine(sb); }
  static void T3() { var sb = new StringBuilder(); sb.AppendLine("a"); sb.AppendLine(); sb.Append("b"); Console.WriteLine(sb.ToString().Replace("\n", "N").Replace("\r", "R")); }
  static void T4() { var sb = new StringBuilder("abc"); sb[1] = 'X'; Console.WriteLine(sb + " " + sb[2] + " " + sb.Length); sb.Length = 2; Console.WriteLine(sb); sb.Clear(); Console.WriteLine(sb.Length); }
  static void T5() { var sb = new StringBuilder(); for (int i = 0; i < 5; i++) sb.Append(i).Append(','); sb.Length--; Console.WriteLine(sb); }
  static void T6() { var sb = new StringBuilder(); sb.AppendFormat("{0}-{1:F1}", 1, 2.55); sb.Append($"[{3}]"); Console.WriteLine(sb); }
  static void T7() { var sb = new StringBuilder(); sb.Append("abc", 1, 2).Append(new[] { 'x', 'y' }).Append('z', 3); Console.WriteLine(sb); }
  static void T8() { var sb = new StringBuilder("abcdef"); Console.WriteLine(sb.ToString(1, 3) + " " + sb.Equals(new StringBuilder("abcdef")) + " " + sb.ToString().IndexOf('d')); }
  static void T9() { var sb = new StringBuilder(); sb.AppendJoin(",", new[] { 1, 2, 3 }); Console.WriteLine(sb); }
  static void T10() { var sb = new StringBuilder(16); Console.WriteLine(sb.Capacity >= 16); var sb2 = new StringBuilder("x", 10); Console.WriteLine(sb2); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
