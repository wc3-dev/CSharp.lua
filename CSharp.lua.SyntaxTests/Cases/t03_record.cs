using System;
record R(int A, string B);
record class RC(int A) { public int Twice => A * 2; }
record struct RS(int A, int B);
record Person(string Name) { public int Age { get; init; } }
record Derived(int A, string B, int C) : R(A, B);
class Program {
  static void T1() { var r = new R(1, "x"); var s = new R(1, "x"); Console.WriteLine(r + " " + (r == s) + " " + r.Equals(s) + " " + (r.GetHashCode() == s.GetHashCode()) + " " + (r != s)); }
  static void T2() { var r = new R(1, "x"); var t = r with { A = 5 }; Console.WriteLine(t + " " + r); }
  static void T3() { var r = new R(1, "x"); var (a, b) = r; Console.WriteLine(a + b); }
  static void T4() { Console.WriteLine(new RC(4).Twice); }
  static void T5() { Console.WriteLine(new RC(4)); }
  static void T6() { var rs = new RS(1, 2); var rs2 = rs with { B = 9 }; Console.WriteLine(rs + " " + rs2 + " " + (rs == rs2)); }
  static void T7() { var p = new Person("Z") { Age = 3 }; Console.WriteLine(p + " " + (p with { Age = 4 }).Age); }
  static void T8() { var d = new Derived(1, "q", 3); Console.WriteLine(d + " " + (d == new Derived(1, "q", 3)) + " " + (d.Equals(new R(1, "q")))); }
  static void T9() { var r = new R(1, null); Console.WriteLine(r); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
