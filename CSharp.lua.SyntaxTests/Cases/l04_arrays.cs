using System;
using System.Linq;
class Program {
  static void T1() { int[] a = new int[3]; a[1] = 5; Console.WriteLine(a.Length + " " + a[0] + a[1] + " " + string.Join(",", a)); }
  static void T2() { int[] a = { 3, 1, 2 }; Array.Sort(a); Console.WriteLine(string.Join(",", a)); Array.Reverse(a); Console.WriteLine(string.Join(",", a)); Console.WriteLine(Array.IndexOf(a, 2) + " " + Array.IndexOf(a, 9)); }
  static void T3() { var a = new int[2, 3]; a[1, 2] = 7; Console.WriteLine(a.Length + " " + a.GetLength(0) + " " + a.GetLength(1) + " " + a[1, 2] + " " + a.Rank); }
  static void T4() { var j = new int[3][]; j[0] = new[] { 1 }; j[1] = new[] { 2, 3 }; j[2] = new int[0]; Console.WriteLine(j.Length + " " + j[1].Length + " " + j[2].Length + " " + j.Sum(x => x.Length)); }
  static void T5() { int[][] j = { new[] { 1, 2 }, new[] { 3 } }; Console.WriteLine(j[0][1] + j[1][0]); }
  static void T6() { var a = new int[5]; Array.Fill(a, 7); Array.Clear(a, 0, 2); Console.WriteLine(string.Join(",", a)); }
  static void T7() { var a = new[] { 1, 2, 3, 4 }; var b = new int[4]; Array.Copy(a, b, 4); Array.Copy(a, 1, b, 0, 2); Console.WriteLine(string.Join(",", b)); var c = (int[])a.Clone(); c[0] = 9; Console.WriteLine(a[0]); }
  static void T8() { var a = new[] { 1, 2, 3 }; Console.WriteLine(Array.Exists(a, x => x > 2) + " " + Array.Find(a, x => x > 1) + " " + Array.FindIndex(a, x => x == 3) + " " + Array.TrueForAll(a, x => x > 0) + " " + string.Join(",", Array.FindAll(a, x => x != 2))); }
  static void T9() { var a = new string[2]; Console.WriteLine((a[0] == null) + " " + a.Length); var b = new bool[2]; Console.WriteLine(b[0]); var c = new float[2]; Console.WriteLine(c[0]); var d = new char[1]; Console.WriteLine((int)d[0]); }
  static void T10() { var a = new int[3]; try { a[3] = 1; } catch (IndexOutOfRangeException) { Console.WriteLine("ioor"); } try { var x = a[-1]; } catch (IndexOutOfRangeException) { Console.WriteLine("ioor2"); } }
  static void T11() { int[] a = { 1, 2, 3 }; int[] b = a; b[0] = 9; Console.WriteLine(a[0] + " " + (a == b) + " " + a.Equals(b) + " " + (a == new[] { 9, 2, 3 })); }
  static void T12() { var a = new int[3, 3]; for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) a[i, j] = i * j; int s = 0; foreach (var x in a) s += x; Console.WriteLine(s); }
  static void T13() { var a = Enumerable.Range(1, 5).ToArray(); Console.WriteLine(a.Sum() + " " + a.Length); Array.Resize(ref a, 3); Console.WriteLine(a.Length + " " + a[2]); }
  static void T14() { var a = new[] { "b", "a", "c" }; Array.Sort(a); Console.WriteLine(string.Join("", a)); Array.Sort(a, (x, y) => y.CompareTo(x)); Console.WriteLine(string.Join("", a)); }
  static void T15() { var k = new[] { 3, 1, 2 }; var v = new[] { "c", "a", "b" }; Array.Sort(k, v); Console.WriteLine(string.Join("", v)); }
  static void T16() { var a = new int[2][]; Console.WriteLine(a[0] == null); var b = new int[2, 2] { { 1, 2 }, { 3, 4 } }; Console.WriteLine(b[1, 1]); }
  static void T17() { Array arr = new int[] { 1, 2 }; Console.WriteLine(arr.Length + " " + arr.GetValue(1)); arr.SetValue(5, 0); Console.WriteLine(((int[])arr)[0]); }
  static void T18() { var a = new[] { 1, 2, 3 }; Console.WriteLine(a.Contains(2) + " " + a.First() + " " + a.Last() + " " + a.Max() + " " + a.Min() + " " + a.Average() + " " + a.Reverse().First()); }
  static void T19() { var a = new int[2, 2]; a[0, 0] = 1; var b = a.Clone() as int[,]; b[0, 0] = 2; Console.WriteLine(a[0, 0] + " " + b[0, 0]); }
  static void T20() { var a = new double[3]; a[0] = 1.5; Console.WriteLine(a.Sum() + " " + a.Length); var l = new long[2]; l[1] = 5; Console.WriteLine(l.Sum()); }
  static void T21() { var a = new byte[] { 1, 2, 255 }; int s = 0; foreach (var x in a) s += x; Console.WriteLine(s + " " + a[2]); }
  static void T22() { var a = new[] { 1, 2, 3, 4, 5, 6 }; Console.WriteLine(string.Join(",", a.Skip(1).Take(3)) + " " + a.Where(x => x % 2 == 0).Count()); }
  static void T23() { var a = new int[] { 5, 3 }; var (x, y) = (a[0], a[1]); (a[0], a[1]) = (a[1], a[0]); Console.WriteLine(a[0] + "" + a[1]); }
  static void T24() { var a = Array.Empty<int>(); Console.WriteLine(a.Length); var b = new int[0]; Console.WriteLine(b.Length + " " + b.Sum()); }
  static void T25() { var a = new int[] { 1, 2, 3 }; Console.WriteLine(a.AsSpan().Length + " " + a.Take(2).Sum() + " " + a[^1] + " " + Array.LastIndexOf(a, 3) + " " + Array.BinarySearch(a, 2)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); H.R("T23", T23); H.R("T24", T24); H.R("T25", T25); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
