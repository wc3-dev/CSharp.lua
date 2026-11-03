using System;
using System.Collections.Generic;
class Box<T> { public T V; public static int Count; public Box(T v) { V = v; Count++; } public override string ToString() => "Box(" + V + ")"; }
class Pair<A, B> { public A F; public B S; }
class Cmp<T> where T : IComparable<T> { public T Max(T a, T b) => a.CompareTo(b) >= 0 ? a : b; }
class New<T> where T : new() { public T Make() => new T(); }
class Program {
  static T Id<T>(T x) => x;
  static void Swap<T>(ref T a, ref T b) { var t = a; a = b; b = t; }
  static string Name<T>() => typeof(T).Name;
  static void T1() { var b1 = new Box<int>(1); var b2 = new Box<string>("s"); var b3 = new Box<int>(2); Console.WriteLine(b1 + " " + b2 + " " + Box<int>.Count + " " + Box<string>.Count); }
  static void T2() { Console.WriteLine(Id(5) + " " + Id("x") + " " + Id<float>(2)); }
  static void T3() { int x = 1, y = 2; Swap(ref x, ref y); Console.WriteLine(x + "" + y); }
  static void T4() { Console.WriteLine(new Cmp<int>().Max(3, 9) + " " + new Cmp<string>().Max("a", "b")); }
  static void T5() { Console.WriteLine(new New<List<int>>().Make().Count); }
  static void T6() { Console.WriteLine(Name<int>() + " " + Name<string>() + " " + Name<List<int>>()); }
  static void T7() { var p = new Pair<int, string> { F = 1, S = "z" }; Console.WriteLine(p.F + p.S); }
  static void T8() { Console.WriteLine(default(Box<int>) == null); }
  static void T9() { Console.WriteLine(typeof(Box<int>).Name + " " + new Box<int>(1).GetType().Name); }
  static void T10() { object o = new Box<int>(1); Console.WriteLine((o is Box<int>) + " " + (o is Box<string>)); }
  static void T11() { var d = new Dictionary<string, List<int>>(); d["a"] = new List<int> { 1 }; d["a"].Add(2); Console.WriteLine(d["a"].Count); }
  static void T12() { Console.WriteLine(Default<int>() + " " + Default<string>() + " " + Default<float>() + " " + Default<bool>()); }
  static T Default<T>() => default(T);
  static void T13() { Console.WriteLine(Eq(1, 1) + " " + Eq("a", "b") + " " + Eq<object>(null, null)); }
  static bool Eq<T>(T a, T b) => EqualityComparer<T>.Default.Equals(a, b);
  static void T14() { Console.WriteLine(Max(3, 7) + " " + Max("a", "z") + " " + Max(2.5f, 1.5f)); }
  static T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) > 0 ? a : b;
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
