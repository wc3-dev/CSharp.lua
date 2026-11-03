using System;
class A {
  public int F = 5; public static int SF = 7; public readonly int RO = 9; public const int C = 11;
  public int Auto { get; set; } = 3; public int Ro { get; } = 4; public int Priv { get; private set; }
  int backing; public int Manual { get { return backing * 2; } set { backing = value + 1; } }
  public int Expr => F + 1; public static int SProp { get; set; } = 8;
  public int this[int i] { get { return i * F; } set { F = i + value; } }
  public string this[string s, int n] => s + n;
  public void SetPriv() { Priv = 2; }
}
class Program {
  static void T1() { var a = new A(); Console.WriteLine(a.F + " " + A.SF + " " + a.RO + " " + A.C + " " + a.Auto + " " + a.Ro + " " + a.Priv); }
  static void T2() { var a = new A(); a.Manual = 4; Console.WriteLine(a.Manual + " " + a.Expr); }
  static void T3() { var a = new A(); Console.WriteLine(a[3]); a[2] = 10; Console.WriteLine(a.F); Console.WriteLine(a["x", 2]); }
  static void T4() { A.SProp += 2; A.SF++; Console.WriteLine(A.SProp + " " + A.SF); }
  static void T5() { var a = new A(); a.Auto++; a.Auto += 5; a.F *= 2; a.F <<= 1; Console.WriteLine(a.Auto + " " + a.F); }
  static void T6() { var a = new A(); a.SetPriv(); Console.WriteLine(a.Priv); }
  static void T7() { var a = new A(); int x = a.Auto++ + ++a.Auto; Console.WriteLine(x + " " + a.Auto); }
  static void T8() { var a = new A(); a[1]++; Console.WriteLine(a.F); }
  static void T9() { var a = new A { F = 1, Auto = 2 }; Console.WriteLine(a.F + a.Auto); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
