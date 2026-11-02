using System;
using static System.Math;
using Alias = System.Collections.Generic.List<int>;
using Pt = (int X, int Y);
class Program {
  static void T1() { Console.WriteLine(Abs(-3) + Max(1, 2) + " " + PI.ToString().Length); }
  static void T2() { Alias a = new Alias { 1, 2 }; Console.WriteLine(a.Count); }
  static void T3() { Pt p = (1, 2); Console.WriteLine(p.X + p.Y); }
  static void T4() { Console.WriteLine(global::System.Math.Abs(-1)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
