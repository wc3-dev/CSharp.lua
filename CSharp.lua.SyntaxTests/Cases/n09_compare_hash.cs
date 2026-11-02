using System;
class Program {
  static void T1() { Console.WriteLine(1.CompareTo(2) + " " + 2.CompareTo(2) + " " + "a".CompareTo("b") + " " + 1.5f.CompareTo(0.5f) + " " + 'b'.CompareTo('a') + " " + true.CompareTo(false)); }
  static void T2() { Console.WriteLine(5.GetHashCode() + " " + 'a'.GetHashCode() + " " + true.GetHashCode() + " " + "abc".GetHashCode().Equals("abc".GetHashCode()) + " " + 5L.GetHashCode() + " " + (1.5f).GetHashCode().Equals((1.5f).GetHashCode())); }
  static void T3() { Console.WriteLine(int.Equals(1, 1) + " " + object.Equals(1, 1) + " " + object.Equals(1, 1L) + " " + 1.Equals(1.0) + " " + ((object)1).Equals(1)); }
  static void T4() { Console.WriteLine(HashCode.Combine(1, 2) == HashCode.Combine(1, 2)); }
  static void T5() { Console.WriteLine(Math.Max(float.NaN, 1f) + " " + Math.Min(0.0, -0.0) + " " + float.IsNaN(Math.Max(float.NaN, 1f))); }
  static void T6() { int a = 5; long b = 5; double c = 5.0; float d = 5f; Console.WriteLine((a == b) + " " + (b == c) + " " + (c == d) + " " + (a < c) + " " + ((long)a == b)); }
  static void T7() { Console.WriteLine(string.Compare("a", "B") + " " + string.Compare("a", "B", StringComparison.OrdinalIgnoreCase) + " " + string.CompareOrdinal("a", "B") + " " + "apple".CompareTo("Apple")); }
  static void T8() { Console.WriteLine(System.Collections.Generic.Comparer<int>.Default.Compare(3, 4) + " " + System.Collections.Generic.EqualityComparer<string>.Default.Equals("a", "a")); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
