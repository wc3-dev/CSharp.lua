using System;
using System.Collections.Generic;
using System.Collections;
class Cnt : IEnumerable<int> { public IEnumerator<int> GetEnumerator() { for (int i = 0; i < 3; i++) yield return i; } IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); }
class Duck { public Enum GetEnumerator() => new Enum(); public class Enum { int i; public bool MoveNext() => ++i <= 2; public int Current => i; } }
class Program {
  static void T1() { foreach (var x in new[] { 1, 2 }) Console.Write(x); Console.WriteLine(); }
  static void T2() { foreach (var x in new List<string> { "a", "b" }) Console.Write(x); Console.WriteLine(); }
  static void T3() { foreach (var kv in new Dictionary<string, int> { ["a"] = 1 }) Console.Write(kv.Key + kv.Value); Console.WriteLine(); }
  static void T4() { foreach (var c in "abc") Console.Write(c); Console.WriteLine(); }
  static void T5() { foreach (var x in new Cnt()) Console.Write(x); Console.WriteLine(); }
  static void T6() { foreach (var x in new HashSet<int> { 3 }) Console.Write(x); Console.WriteLine(); }
  static void T7() { foreach (var x in new Queue<int>(new[] { 1, 2 })) Console.Write(x); foreach (var x in new Stack<int>(new[] { 1, 2 })) Console.Write(x); Console.WriteLine(); }
  static void T8() { foreach (var x in new int[2, 2] { { 1, 2 }, { 3, 4 } }) Console.Write(x); Console.WriteLine(); }
  static void T9() { foreach (var x in new Duck()) Console.Write(x); Console.WriteLine(); }
  static void T10() { var l = new List<int> { 1, 2, 3 }; try { foreach (var x in l) l.Add(x); } catch (InvalidOperationException) { Console.WriteLine("modified"); } }
  static void T11() { foreach (var (k, v) in new Dictionary<int, string> { [1] = "a" }) Console.Write(k + v); Console.WriteLine(); }
  static void T12() { foreach (int x in new object[] { 1, 2 }) Console.Write(x); Console.WriteLine(); }
  static void T13() { foreach (var x in new List<int>()) Console.Write(x); Console.WriteLine("empty"); }
  static void T14() { foreach (var x in new[] { 1, 2, 3 }) { if (x == 2) continue; if (x == 3) break; Console.Write(x); } Console.WriteLine(); }
  static void T15() { foreach (var s in new[] { "a", null, "c" }) Console.Write(s ?? "-"); Console.WriteLine(); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
