using System;
using System.Collections.Generic;
class F { ~F() { Console.WriteLine("finalized"); } }
class A { public virtual string Who() => "A"; public string Call() => Who(); }
class B : A { public override string Who() => "B>" + base.Who(); }
partial class PM { partial void Hook(int x); public void Run() { Hook(1); Console.WriteLine("ran"); } }
partial class PM { partial void Hook(int x) { Console.WriteLine("hook " + x); } }
class Op { public int V; public static bool operator ==(Op a, Op b) => ReferenceEquals(a, b) || (!ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.V == b.V); public static bool operator !=(Op a, Op b) => !(a == b); public override bool Equals(object o) => this == (o as Op); public override int GetHashCode() => V; }
class Program {
  static void T1() { new F(); Console.WriteLine("created"); }
  static void T2() { Console.WriteLine(new B().Call()); }
  static void T3() { new PM().Run(); }
  static void T4() { Op a = null; Console.WriteLine((a == null) + " " + (new Op { V = 1 } == new Op { V = 1 }) + " " + (new Op { V = 1 } != null)); }
  static void T5() { const int K = 3; const string S = "k" + "s"; Console.WriteLine(K + S); }
  static void T6() { var l = new List<int>(); Func<int, int> f = static x => x + 1; l.Add(f(1)); Console.WriteLine(l[0]); }
  static void T7() { int x = 5; Console.WriteLine(x is int); object o = x; Console.WriteLine(o is int i ? i : -1); }
  static void T8() { Console.WriteLine(CountT<int>(new[] { 1, 2 }) + " " + CountT<string>(new[] { "a" })); }
  static int CountT<T>(T[] a) where T : notnull => a.Length;
  static void T9() { Console.WriteLine(Make<List<int>>().Count + " " + Make<Op>().V); }
  static T Make<T>() where T : class, new() => new T();
  static void T10() { Console.WriteLine(Zero<int>() + " " + Zero<float>()); }
  static T Zero<T>() where T : struct => default;
  static void T11() { Console.WriteLine(First<int>(new[] { 4, 5 })); }
  static T First<T>(IEnumerable<T> e) where T : unmanaged { foreach (var x in e) return x; return default; }
  static void T12() { A a = new B(); Console.WriteLine(a is B b ? b.Who() : "no"); }
  static void T13() { var o = new object(); lock (o) { lock (o) { Console.WriteLine("nested"); } } }
  static void T14() { Console.WriteLine(Describe(1) + Describe("s") + Describe(2.5f)); }
  static string Describe<T>(T v) => typeof(T).Name + ":" + v + ";";
  static void T15() { string s = null; Console.WriteLine(s?.Length ?? 0); Console.WriteLine(s is null); Console.WriteLine(s == null); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
