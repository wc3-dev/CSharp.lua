using System;
using System.Collections.Generic;
using System.Linq;
abstract class Comp<TOwner> { public TOwner Owner; public static int Made; protected Comp() { Made++; } public abstract string Kind { get; } }
class Burn : Comp<string> { public override string Kind => "burn"; }
class Chill : Comp<int> { public override string Kind => "chill"; }
class Reg<T> where T : class { static Dictionary<string, T> items = new(); public static void Put(string k, T v) { items[k] = v; } public static T Get(string k) => items.TryGetValue(k, out var v) ? v : null; public static int Count => items.Count; }
class Gen<T> { public T Val; public static readonly string Name = typeof(T).Name; public static T Make(Func<T> f) => f(); }
class Tree<T> where T : IComparable<T> { class Node { public T V; public Node L, R; } Node root; public int Count;
  public void Add(T v) { root = Add(root, v); } Node Add(Node n, T v) { if (n == null) { Count++; return new Node { V = v }; } int c = v.CompareTo(n.V); if (c < 0) n.L = Add(n.L, v); else if (c > 0) n.R = Add(n.R, v); return n; }
  public IEnumerable<T> InOrder() { return Walk(root); } IEnumerable<T> Walk(Node n) { if (n == null) yield break; foreach (var x in Walk(n.L)) yield return x; yield return n.V; foreach (var x in Walk(n.R)) yield return x; } }
interface IRepo<T> { void Add(T t); IEnumerable<T> All(); }
class MemRepo<T> : IRepo<T> { List<T> l = new(); public void Add(T t) { l.Add(t); } public IEnumerable<T> All() => l; }
class Program {
  static void T1() { new Burn(); new Burn(); new Chill(); Console.WriteLine(Comp<string>.Made + " " + Comp<int>.Made); }
  static void T2() { Reg<string>.Put("a", "x"); Reg<object>.Put("a", new object()); Console.WriteLine(Reg<string>.Get("a") + " " + Reg<string>.Count + " " + Reg<object>.Count + " " + (Reg<string>.Get("zz") == null)); }
  static void T3() { Console.WriteLine(Gen<int>.Name + " " + Gen<string>.Name + " " + Gen<int>.Make(() => 7)); }
  static void T4() { var t = new Tree<int>(); foreach (var x in new[] { 5, 3, 8, 1, 4, 5 }) t.Add(x); Console.WriteLine(string.Join(",", t.InOrder()) + " " + t.Count); }
  static void T5() { var t = new Tree<string>(); t.Add("b"); t.Add("a"); Console.WriteLine(string.Join(",", t.InOrder())); }
  static void T6() { IRepo<int> r = new MemRepo<int>(); r.Add(1); r.Add(2); Console.WriteLine(r.All().Sum()); }
  static void T7() { Comp<string> c = new Burn { Owner = "me" }; Console.WriteLine(c.Kind + c.Owner); }
  static void T8() { var l = new List<Comp<string>> { new Burn(), new Burn() }; Console.WriteLine(l.Count); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
