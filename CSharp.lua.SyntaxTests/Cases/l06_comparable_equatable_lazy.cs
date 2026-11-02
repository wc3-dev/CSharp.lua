using System;
using System.Collections.Generic;
using System.Linq;
class Ver : IComparable<Ver>, IEquatable<Ver> { public int Major, Minor; public Ver(int a, int b) { Major = a; Minor = b; }
  public int CompareTo(Ver o) { int c = Major.CompareTo(o.Major); return c != 0 ? c : Minor.CompareTo(o.Minor); }
  public bool Equals(Ver o) => o != null && Major == o.Major && Minor == o.Minor;
  public override bool Equals(object o) => Equals(o as Ver);
  public override int GetHashCode() => Major * 31 + Minor;
  public override string ToString() => Major + "." + Minor; }
class OldC : IComparable { public int V; public int CompareTo(object o) => V.CompareTo(((OldC)o).V); }
class ByLen : IComparer<string> { public int Compare(string a, string b) => a.Length - b.Length; }
class Program {
  static void T1() { var l = new List<Ver> { new Ver(2, 1), new Ver(1, 5), new Ver(1, 2) }; l.Sort(); Console.WriteLine(string.Join(" ", l)); }
  static void T2() { Console.WriteLine(new Ver(1, 2).Equals(new Ver(1, 2)) + " " + new Ver(1, 2).Equals(new Ver(1, 3)) + " " + new Ver(1, 2).Equals((object)new Ver(1, 2))); }
  static void T3() { var h = new HashSet<Ver> { new Ver(1, 1), new Ver(1, 1), new Ver(2, 2) }; Console.WriteLine(h.Count); var d = new Dictionary<Ver, string> { [new Ver(1, 1)] = "x" }; Console.WriteLine(d[new Ver(1, 1)]); }
  static void T4() { var l = new List<OldC> { new OldC { V = 3 }, new OldC { V = 1 } }; l.Sort(); Console.WriteLine(l[0].V); }
  static void T5() { var l = new List<string> { "ccc", "a", "bb" }; l.Sort(new ByLen()); Console.WriteLine(string.Join(",", l)); Console.WriteLine(string.Join(",", l.OrderBy(x => x, new ByLen()))); }
  static void T6() { var l = new List<Ver> { new Ver(3, 0), new Ver(1, 0) }; Console.WriteLine(l.Min() + " " + l.Max() + " " + l.OrderBy(x => x).First()); }
  static void T7() { var calls = 0; var lz = new Lazy<int>(() => { calls++; return 42; }); Console.WriteLine(lz.IsValueCreated + " " + lz.Value + " " + lz.Value + " " + calls + " " + lz.IsValueCreated); }
  static void T8() { int? n = 5; Console.WriteLine(n.HasValue + " " + n.Value + " " + (n ?? 0) + " " + n.GetValueOrDefault() + " " + n.Equals(5) + " " + n.ToString()); int? m = null; Console.WriteLine(m.HasValue + " " + m.GetValueOrDefault(9) + " " + (m == null) + " " + m.Equals(null) + " [" + m.ToString() + "]"); }
  static void T9() { Nullable<int> n = 3; Console.WriteLine(n + 1); int? a = null; Console.WriteLine((a + 1) == null); Console.WriteLine(Nullable.GetUnderlyingType(typeof(int?)).Name); }
  static void T10() { Func<int, int> f = x => x + 1; Action<int> a = x => Console.Write(x); Func<int> g = () => 5; Predicate<int> p = x => x > 2; Comparison<int> c = (x, y) => x - y; Converter<int, string> cv = x => "n" + x; a(f(g())); Console.WriteLine(p(3) + " " + c(5, 3) + " " + cv(1)); }
  static void T11() { Console.WriteLine(Comparer<Ver>.Default.Compare(new Ver(1, 0), new Ver(2, 0)) + " " + EqualityComparer<Ver>.Default.Equals(new Ver(1, 0), new Ver(1, 0))); }
  static void T12() { var s = new SortedSet<Ver>(new[] { new Ver(2, 0), new Ver(1, 0) }); Console.WriteLine(s.Min); }
  static void T13() { IComparable<int> c = 5; Console.WriteLine(c.CompareTo(3)); IComparable c2 = "b"; Console.WriteLine(c2.CompareTo("a")); IEquatable<string> e = "x"; Console.WriteLine(e.Equals("x")); }
  static void T14() { var t = Tuple.Create(1, "a"); var t2 = Tuple.Create(1, "a"); Console.WriteLine(t.Equals(t2) + " " + t.Item1); var vt = (1, "a"); Console.WriteLine(vt.Equals((1, "a"))); }
  static void T15() { object o = new Ver(1, 2); var c = (IComparable<Ver>)o; Console.WriteLine(c.CompareTo(new Ver(1, 1))); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
