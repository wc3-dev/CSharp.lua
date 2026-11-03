using System;
using System.Collections.Generic;
struct P { public int X, Y; public P(int x, int y) { X = x; Y = y; } public void Move() { X++; } public override string ToString() => "(" + X + "," + Y + ")"; }
class Holder { public P Field; public P Prop { get; set; } }
class Program {
  static void Bump(P p) { p.X += 100; }
  static void BumpRef(ref P p) { p.X += 100; }
  static void T1() { var a = new P(1, 2); var b = a; b.X = 9; Console.WriteLine(a + " " + b); }
  static void T2() { var a = new P(1, 2); Bump(a); Console.WriteLine(a); BumpRef(ref a); Console.WriteLine(a); a.Move(); Console.WriteLine(a); }
  static void T3() { P[] arr = new P[2]; arr[0].X = 5; var c = arr[0]; c.X = 6; Console.WriteLine(arr[0] + " " + c + " " + arr[1]); }
  static void T4() { var l = new List<P>(); l.Add(new P(1, 1)); var x = l[0]; x.X = -1; Console.WriteLine(l[0] + " " + x); }
  static void T5() { var a = new P(1, 2); var l = new List<P>(); l.Add(a); a.X = 50; Console.WriteLine(l[0] + " " + a); }
  static void T6() { var h = new Holder(); h.Field = new P(1, 2); var c = h.Field; c.X = 7; Console.WriteLine(h.Field + " " + c); }
  static void T7() { var h = new Holder(); h.Prop = new P(1, 2); var c = h.Prop; c.X = 7; Console.WriteLine(h.Prop + " " + c); }
  static void T8() { var a = new P(1, 2); var h = new Holder(); h.Field = a; a.X = 9; Console.WriteLine(h.Field + " " + a); }
  static void T9() { var a = new P(1, 2); Console.WriteLine(a.Equals(new P(1, 2)) + " " + a.Equals(a) + " " + default(P) + " " + (a.GetHashCode() == new P(1, 2).GetHashCode())); }
  static void T10() { var d = new Dictionary<int, P>(); d[1] = new P(1, 1); var c = d[1]; c.X = 5; Console.WriteLine(d[1] + " " + c); }
  static P Ret(P p) { p.X = 99; return p; }
  static void T11() { var a = new P(1, 2); var b = Ret(a); Console.WriteLine(a + " " + b); }
  static void T12() { var a = new P(1, 2); P b; b = a; b.Y = 0; Console.WriteLine(a + " " + b); }
  static void T13() { P[] arr = { new P(1, 1), new P(2, 2) }; foreach (var p in arr) { var q = p; q.X = 0; } Console.WriteLine(arr[0] + " " + arr[1]); }
  static void T14() { var a = new P(1, 2); object o = a; a.X = 5; Console.WriteLine(((P)o).X); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
