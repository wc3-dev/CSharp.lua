using System;
class P { public int X; public override bool Equals(object o) => o is P p && p.X == X; public override int GetHashCode() => X; public override string ToString() => "P" + X; }
class Q { }
class Program {
  static void T1() { Console.WriteLine(new P { X = 1 }.Equals(new P { X = 1 }) + " " + object.Equals(new P { X = 2 }, new P { X = 2 }) + " " + new P { X = 3 }.GetHashCode()); }
  static void T2() { object o = new P { X = 5 }; Console.WriteLine(o.ToString() + " " + o); }
  static void T3() { var d = new System.Collections.Generic.Dictionary<P, int>(); d[new P { X = 1 }] = 10; Console.WriteLine(d[new P { X = 1 }]); }
  static void T4() { var q = new Q(); Console.WriteLine(q.ToString() + " " + q.Equals(q) + " " + (q.GetHashCode() == q.GetHashCode())); }
  static void T5() { string s = null; object o = s; Console.WriteLine(o == null); Console.WriteLine(object.ReferenceEquals(null, null)); }
  static void T6() { Console.WriteLine(new object() != null); Console.WriteLine(1.Equals(1) + " " + "a".Equals("a") + " " + 1.5f.Equals(1.5f) + " " + 'a'.Equals('a')); }
  static void T7() { object a = 1, b = 1; Console.WriteLine((a == b) + " " + a.Equals(b)); }
  static void T8() { object a = "x", b = "x"; Console.WriteLine(a.Equals(b)); int i = 5; object bx = i; Console.WriteLine((int)bx + 1); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
