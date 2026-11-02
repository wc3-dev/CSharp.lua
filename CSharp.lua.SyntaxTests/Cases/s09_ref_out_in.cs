using System;
using System.Collections.Generic;
struct S { public int A; }
class Program {
  static void Inc(ref int x) { x++; } static void Set(out int x) { x = 42; } static int Read(in int x) => x + 1;
  static bool TryGet(int k, out string v) { v = k > 0 ? "pos" : null; return k > 0; }
  static ref int First(int[] a) => ref a[0];
  static void Swap<T>(ref T a, ref T b) { var t = a; a = b; b = t; }
  static void T1() { int a = 1; Inc(ref a); Inc(ref a); Console.WriteLine(a); }
  static void T2() { Set(out int x); Console.WriteLine(x); Set(out var y); Console.WriteLine(y); }
  static void T3() { int a = 5; Console.WriteLine(Read(in a) + Read(a)); }
  static void T4() { if (TryGet(1, out var v)) Console.WriteLine(v); if (!TryGet(-1, out _)) Console.WriteLine("no"); }
  static void T5() { var arr = new[] { 1, 2 }; Inc(ref arr[1]); Console.WriteLine(arr[1]); }
  static void T6() { var arr = new[] { 1, 2 }; ref int r = ref First(arr); r = 9; Console.WriteLine(arr[0]); }
  static void T7() { int a = 1, b = 2; Swap(ref a, ref b); Console.WriteLine(a + "" + b); }
  class H2 { public int F; }
  static void T8() { var h = new H2(); Inc(ref h.F); Console.WriteLine(h.F); }
  static void T9() { var s = new S(); ref int r = ref s.A; r = 7; Console.WriteLine(s.A); }
  static void T10() { int.TryParse("12", out int n); Console.WriteLine(n); var d = new Dictionary<int, int> { [1] = 5 }; d.TryGetValue(1, out var q); Console.WriteLine(q); }
  static void T11() { string s = "a"; Mod(ref s); Console.WriteLine(s); }
  static void Mod(ref string s) { s += "b"; }
  static void T12() { int x = 0; Func<int> f = () => x; Inc(ref x); Console.WriteLine(f()); }
  static void T13() { Span<int> sp = new int[] { 1, 2 }; ref int r = ref sp[0]; r = 5; Console.WriteLine(sp[0]); }
  static void T14() { int x = 3; ref readonly int rr = ref x; Console.WriteLine(rr); }
  static void T15() { int i = 0; int[] a = new int[3]; a[i++] = i; a[i++] = i; Console.WriteLine(string.Join(",", a)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
