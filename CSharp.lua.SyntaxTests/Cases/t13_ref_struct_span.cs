using System;
ref struct R { public int A; public Span<int> S; public int Sum() { int t = A; foreach (var x in S) t += x; return t; } }
class Program {
  static void T1() { int[] arr = { 1, 2, 3 }; var r = new R { A = 10, S = arr }; Console.WriteLine(r.Sum()); }
  static void T2() { int[] arr = { 1, 2, 3 }; Span<int> sp = arr.AsSpan(1, 2); sp[0] = 20; Console.WriteLine(arr[1] + " " + sp.Length); }
  static void T3() { int[] arr = { 1, 2, 3 }; Span<int> sp = arr; var sl = sp.Slice(1); Console.WriteLine(sl.Length + " " + sl[0]); }
  static void T4() { int[] arr = { 1, 2, 3 }; Span<int> sp = arr; sp.Fill(7); Console.WriteLine(arr[0] + "" + arr[2]); }
  static void T5() { Span<int> sp = stackalloc int[3]; sp[1] = 4; Console.WriteLine(sp.Length + " " + sp[1] + " " + sp[0]); }
  static void T6() { ReadOnlySpan<char> s = "hello".AsSpan(); Console.WriteLine(s.Length + " " + s[1]); }
  static void T7() { int[] arr = { 3, 1, 2 }; Span<int> sp = arr; sp.Sort(); Console.WriteLine(arr[0] + "" + arr[1] + arr[2]); }
  static void T8() { int[] arr = { 1, 2, 3 }; Span<int> sp = arr; Console.WriteLine(sp.ToArray().Length + " " + sp.IsEmpty); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
