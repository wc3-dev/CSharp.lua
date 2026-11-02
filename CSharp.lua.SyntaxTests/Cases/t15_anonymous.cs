using System;
using System.Linq;
class Program {
  static void T1() { var a = new { X = 1, Y = "s" }; var b = new { X = 1, Y = "s" }; Console.WriteLine(a.X + a.Y + " " + a.Equals(b)); }
  static void T2() { var a = new { X = 1, Y = "s" }; Console.WriteLine(a); }
  static void T3() { var l = new[] { 3, 1, 2 }.Select(i => new { I = i, Sq = i * i }).OrderBy(z => z.I).ToList(); foreach (var z in l) Console.Write(z.I + ":" + z.Sq + " "); Console.WriteLine(); }
  static void T4() { var a = new { X = 1 }; var b = new { X = 1 }; Console.WriteLine(a.GetHashCode() == b.GetHashCode()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
