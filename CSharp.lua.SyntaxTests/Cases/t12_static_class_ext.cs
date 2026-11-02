using System;
static class Ext {
  public static int Twice(this int x) => x * 2;
  public static string Shout(this string s) => s.ToUpper() + "!";
  public static T First<T>(this T[] a) => a[0];
  public const int K = 3;
  public static int Counter;
  static Ext() { Counter = 100; }
}
class Program {
  static void T1() { Console.WriteLine(5.Twice() + " " + "hi".Shout() + " " + new[] { 4, 5 }.First()); }
  static void T2() { Console.WriteLine(Ext.K + " " + Ext.Counter); }
  static void T3() { Console.WriteLine(Ext.Twice(4) + Ext.Shout("a")); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
