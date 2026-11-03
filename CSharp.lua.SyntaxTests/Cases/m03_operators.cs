using System;
struct V {
  public int X; public V(int x) { X = x; }
  public static V operator +(V a, V b) => new V(a.X + b.X);
  public static V operator -(V a) => new V(-a.X);
  public static V operator ++(V a) => new V(a.X + 1);
  public static bool operator ==(V a, V b) => a.X == b.X;
  public static bool operator !=(V a, V b) => a.X != b.X;
  public static bool operator <(V a, V b) => a.X < b.X;
  public static bool operator >(V a, V b) => a.X > b.X;
  public static implicit operator int(V v) => v.X;
  public static explicit operator V(int i) => new V(i);
  public static V operator *(V a, int k) => new V(a.X * k);
  public static bool operator true(V a) => a.X != 0;
  public static bool operator false(V a) => a.X == 0;
  public override bool Equals(object o) => o is V v && v.X == X;
  public override int GetHashCode() => X;
  public static V operator checked +(V a, V b) => new V(checked(a.X + b.X));
}
class Program {
  static void T1() { var a = new V(2) + new V(3); Console.WriteLine(a.X + " " + (-a).X); }
  static void T2() { var a = new V(2); a++; Console.WriteLine(a.X); var b = a++; Console.WriteLine(b.X + " " + a.X); }
  static void T3() { Console.WriteLine((new V(1) == new V(1)) + " " + (new V(1) != new V(1)) + " " + (new V(1) < new V(2)) + " " + (new V(1) > new V(2))); }
  static void T4() { int i = new V(7); V v = (V)9; Console.WriteLine(i + " " + v.X); }
  static void T5() { Console.WriteLine((new V(2) * 3).X); }
  static void T6() { var a = new V(1); a += new V(5); Console.WriteLine(a.X); }
  static void T7() { Console.WriteLine(new V(1) ? "t" : "f"); Console.WriteLine(new V(0) ? "t" : "f"); }
  static void T8() { Console.WriteLine(new V(1).Equals(new V(1)) + " " + (new V(1) == new V(2))); }
  static void T9() { Console.WriteLine(checked(new V(1) + new V(2)).X); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
