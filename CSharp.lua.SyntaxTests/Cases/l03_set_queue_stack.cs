using System;
using System.Collections.Generic;
using System.Linq;
class Program {
  static void T1() { var h = new HashSet<int> { 1, 2, 3 }; Console.WriteLine(h.Add(2) + " " + h.Add(4) + " " + h.Count + " " + h.Contains(3) + " " + h.Remove(1) + " " + h.Remove(99) + " " + h.Count); }
  static void T2() { var a = new HashSet<int> { 1, 2, 3 }; var b = new HashSet<int> { 2, 3, 4 }; var u = new HashSet<int>(a); u.UnionWith(b); var i = new HashSet<int>(a); i.IntersectWith(b); var e = new HashSet<int>(a); e.ExceptWith(b); Console.WriteLine(u.Count + " " + i.Count + " " + e.Count + " " + a.Overlaps(b) + " " + a.IsSubsetOf(u) + " " + a.SetEquals(new[] { 3, 2, 1 })); }
  static void T3() { var h = new HashSet<string> { "a", "b" }; Console.WriteLine(string.Join(",", h.OrderBy(x => x)) + " " + h.Contains("a")); }
  static void T4() { var h = new HashSet<(int, int)> { (1, 2), (1, 2), (2, 1) }; Console.WriteLine(h.Count); }
  static void T5() { var h = new HashSet<int>(new[] { 1, 1, 2 }); h.Clear(); Console.WriteLine(h.Count); var g = new HashSet<int>(); h.UnionWith(new[] { 5 }); Console.WriteLine(h.First()); }
  static void T6() { var q = new Queue<int>(); q.Enqueue(1); q.Enqueue(2); q.Enqueue(3); Console.WriteLine(q.Peek() + " " + q.Dequeue() + " " + q.Count + " " + q.Contains(3) + " " + string.Join(",", q)); }
  static void T7() { var q = new Queue<string>(); try { q.Dequeue(); } catch (InvalidOperationException) { Console.WriteLine("empty"); } Console.WriteLine(q.TryDequeue(out var x) + " " + (x == null)); q.Enqueue("a"); Console.WriteLine(q.TryPeek(out var y) + y); }
  static void T8() { var s = new Stack<int>(); s.Push(1); s.Push(2); s.Push(3); Console.WriteLine(s.Peek() + " " + s.Pop() + " " + s.Count + " " + s.Contains(1) + " " + string.Join(",", s)); }
  static void T9() { var s = new Stack<char>(); foreach (var c in "abc") s.Push(c); var r = ""; while (s.Count > 0) r += s.Pop(); Console.WriteLine(r); try { s.Pop(); } catch (InvalidOperationException) { Console.WriteLine("empty"); } Console.WriteLine(s.TryPop(out var z) + " " + (int)z); }
  static void T10() { var s = new Stack<int>(new[] { 1, 2, 3 }); Console.WriteLine(s.Peek() + " " + s.ToArray()[0]); s.Clear(); Console.WriteLine(s.Count); }
  static void T11() { var q = new Queue<int>(new[] { 5, 6 }); q.Clear(); Console.WriteLine(q.Count); q.Enqueue(1); Console.WriteLine(q.ToArray().Length); }
  static void T12() { var l = new LinkedList<int>(); l.AddLast(1); l.AddLast(2); l.AddFirst(0); Console.WriteLine(string.Join(",", l) + " " + l.First.Value + " " + l.Last.Value + " " + l.Count); l.Remove(1); l.RemoveFirst(); Console.WriteLine(string.Join(",", l)); }
  static void T13() { var pq = new PriorityQueue<string, int>(); pq.Enqueue("c", 3); pq.Enqueue("a", 1); pq.Enqueue("b", 2); var r = ""; while (pq.Count > 0) r += pq.Dequeue(); Console.WriteLine(r); }
  static void T14() { var s = new SortedSet<int> { 3, 1, 2 }; Console.WriteLine(string.Join(",", s) + " " + s.Min + " " + s.Max); }
  static void T15() { var b = new System.Collections.BitArray(8); b[2] = true; Console.WriteLine(b[2] + " " + b.Length); }
  static void T16() { var h = new HashSet<int> { 3, 1, 2 }; var l = new List<int>(); foreach (var x in h) l.Add(x); Console.WriteLine(l.Count); }
  static void T17() { var d = new Dictionary<string, HashSet<int>>(); d["a"] = new HashSet<int>(); d["a"].Add(1); d["a"].Add(1); Console.WriteLine(d["a"].Count); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
