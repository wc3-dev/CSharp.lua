using System;
using System.Collections.Generic;
using System.Linq;

struct P {
  public int X;
  public P(int x) { X = x; }
  public void Move() { X++; }
}

struct Outer {
  public P Inner;
}

delegate void Change(P p);

class Holder {
  public P Value;
  public P Auto { get; set; }
  public static P Shared;
  public P Arrow() => Value;
  public P Getter => Value;
  public P Returned() { return this.Value; }
  public void BumpOwn() { Program.Bump(this.Value); }
  public List<P> ListOwn() { return new List<P> { Value, this.Value }; }
  public IEnumerable<P> Each() { yield return Value; }
}

class Program {
  static void Line(string name, int kept, int changed) { Console.WriteLine(name + " " + kept + " " + changed); }
  public static void Bump(P p) { p.X = 9; }
  static void BumpRef(ref P p) { p.X = 9; }
  static P Give(Holder h) { return h.Value; }

  // A struct put into a collection is a copy: changing the variable afterwards leaves the element as it was.
  static void Collections() {
    { var a = new P(1); var list = new List<P> { a }; a.X = 9; Line("list-initializer", list[0].X, a.X); }
    { var a = new P(1); var dict = new Dictionary<int, P> { [0] = a }; a.X = 9; Line("dict-index-initializer", dict[0].X, a.X); }
    { var a = new P(1); var dict = new Dictionary<int, P> { { 0, a } }; a.X = 9; Line("dict-add-initializer", dict[0].X, a.X); }
    { var a = new P(1); List<P> list = [a]; a.X = 9; Line("collection-expression-list", list[0].X, a.X); }
    { var a = new P(1); P[] arr = [a]; a.X = 9; Line("collection-expression-array", arr[0].X, a.X); }
    { var a = new P(1); var arr = new P[] { a }; a.X = 9; Line("array-initializer", arr[0].X, a.X); }
    { var a = new P(1); var arr = new[] { a }; a.X = 9; Line("implicit-array-initializer", arr[0].X, a.X); }
    { var a = new P(1); P[] arr = { a }; a.X = 9; Line("bare-array-initializer", arr[0].X, a.X); }
    { var a = new P(1); var arr = new P[1, 1] { { a } }; a.X = 9; Line("array-2d-initializer", arr[0, 0].X, a.X); }
    { var a = new P(1); var list = new List<P> { new P(0) }; list[0] = a; a.X = 9; Line("list-index-store", list[0].X, a.X); }
    { var a = new P(1); var dict = new Dictionary<int, P>(); dict[3] = a; a.X = 9; Line("dict-index-store", dict[3].X, a.X); }
    { var a = new P(1); var t = (new P(0), 1); t.Item1 = a; a.X = 9; Line("tuple-item-store", t.Item1.X, a.X); }
    { var h = new Holder(); h.Value = new P(1); var list = new List<P> { h.Value }; h.Value.X = 9; Line("list-initializer-field", list[0].X, h.Value.X); }
    { var h = new Holder(); h.Value = new P(1); var list = h.ListOwn(); h.Value.X = 9; Line("list-initializer-own-field", list[0].X + list[1].X, h.Value.X); }
    { var h = new Holder(); h.Value = new P(1); P[] arr = [h.Value]; h.Value.X = 9; Line("collection-expression-field", arr[0].X, h.Value.X); }
    { var h = new Holder(); h.Value = new P(1); var list = new List<P>(); list.Add(h.Value); h.Value.X = 9; Line("add-field", list[0].X, h.Value.X); }
    { var arr = new P[] { new P(1) }; var list = new List<P> { arr[0] }; arr[0].X = 9; Line("list-initializer-element", list[0].X, arr[0].X); }
    { var arr = new P[] { new P(1) }; var list = new List<P> { new P(0) }; list[0] = arr[0]; arr[0].X = 9; Line("list-index-store-element", list[0].X, arr[0].X); }
    { var a = new P(1); var list = new List<P> { new P(0) }; list[0] = a.X > 0 ? a : new P(0); a.X = 9; Line("list-index-store-conditional", list[0].X, a.X); }
  }

  // A struct given to a delegate is the delegate's own: changing it leaves the one the caller, or the collection, has.
  static void Delegates() {
    { var a = new P(1); Action<P> f = p => p.X = 9; f(a); Line("lambda", a.X, 9); }
    { var a = new P(1); Action<P> f = p => { p.Move(); }; f(a); Line("lambda-method", a.X, 2); }
    { var a = new P(1); Action<P> f = delegate (P p) { p.X = 9; }; f(a); Line("anonymous-method", a.X, 9); }
    { var a = new P(1); Func<P, int, int> f = (p, n) => { p.X = n; return p.X; }; var got = f(a, 9); Line("lambda-two-parameters", a.X, got); }
    { var list = new List<P> { new P(1) }; list.ForEach(p => p.X = 9); Line("for-each-lambda", list[0].X, 9); }
    { var list = new List<P> { new P(1) }; list.ForEach(p => p.Move()); Line("for-each-lambda-method", list[0].X, 2); }
    { var list = new List<P> { new P(1) }; var got = list.Select(p => { p.X = 9; return p; }).ToList(); Line("select-lambda", list[0].X, got[0].X); }
    { var list = new List<P> { new P(1) }; var got = list.Select(p => p).ToList(); var c = got[0]; c.X = 9; got[0] = c; Line("select-identity", list[0].X, got[0].X); }
    { var a = new P(1); Action<P> f = Bump; f(a); Line("method-group", a.X, 9); }
    { var a = new P(1); Change f = p => p.X = 9; f(a); Line("own-delegate", a.X, 9); }
    { var a = new P(1); Func<P, Func<int>> f = p => () => p.X; var read = f(a); a.X = 9; Line("lambda-capture", read(), a.X); }
    { var arr = new P[] { new P(1) }; var reads = arr.Select(p => (Func<int>)(() => p.X)).ToList(); arr[0].X = 9; Line("select-capture", reads[0](), arr[0].X); }
    { var a = new P(1); Action<P> f = p => p.X = 9; f.Invoke(a); Line("lambda-invoke", a.X, 9); }
    { var a = new P(1); Action<P> f = p => p.X = 9; f?.Invoke(a); Line("lambda-conditional-invoke", a.X, 9); }
    { var list = new List<P> { new P(3), new P(1) }; list.Sort((l, r) => l.X.CompareTo(r.X)); Line("sort-lambda", list[0].X, list[1].X + 6); }
  }

  // A field, an element or a parameter given on is a copy, whatever stands around it.
  static void Stores() {
    { var h = new Holder(); h.Value = new P(1); Bump(h.Value); Line("argument-field", h.Value.X, 9); }
    { var h = new Holder(); h.Value = new P(1); h.BumpOwn(); Line("argument-own-field", h.Value.X, 9); }
    { Holder.Shared = new P(1); Bump(Holder.Shared); Line("argument-static-field", Holder.Shared.X, 9); }
    { var o = new Outer(); o.Inner = new P(1); Bump(o.Inner); Line("argument-nested", o.Inner.X, 9); }
    { var h = new Holder(); h.Value = new P(1); var c = Give(h); c.X = 9; Line("return-field", h.Value.X, c.X); }
    { var h = new Holder(); h.Value = new P(1); var c = h.Returned(); c.X = 9; Line("return-this-field", h.Value.X, c.X); }
    { var h = new Holder(); h.Value = new P(1); var c = h.Arrow(); c.X = 9; Line("arrow-method", h.Value.X, c.X); }
    { var h = new Holder(); h.Value = new P(1); var c = h.Getter; c.X = 9; Line("arrow-property", h.Value.X, c.X); }
    { var h = new Holder(); h.Value = new P(1); var c = h.Each().ToArray(); c[0].X = 9; Line("yield-field", h.Value.X, c[0].X); }
    { var h = new Holder(); h.Value = new P(1); var t = (h.Value, 1); h.Value.X = 9; Line("tuple-field", t.Item1.X, h.Value.X); }
    { var a = new P(1); Func<P> f = () => a; var c = f(); c.X = 9; Line("lambda-result", a.X, c.X); }
    { var a = new P(1); Func<P, P> f = p => p; var c = f(a); c.X = 9; Line("lambda-result-parameter", a.X, c.X); }
    { var a = new P(1); var b = new P(2); var c = a.X > 0 ? a : b; c.X = 9; Line("conditional", a.X, c.X); }
    { var a = new P(1); var c = a.X switch { 1 => a, _ => new P(0) }; c.X = 9; Line("switch-expression", a.X, c.X); }
    { var a = new P(1); var c = (a); c.X = 9; Line("parenthesized", a.X, c.X); }
    { var a = new P(1); P b; b = (a); b.X = 9; Line("assign-parenthesized", a.X, b.X); }
    { var a = new P(1); P? n = a; var c = n ?? new P(0); c.X = 9; Line("coalesce", n.Value.X, c.X); }
    { var a = new P(1); P? n = null; var c = n ?? a; c.X = 9; Line("coalesce-right", a.X, c.X); }
  }

  // A change made in place is still a change to the original.
  static void InPlace() {
    { var h = new Holder(); h.Value = new P(1); h.Value.X = 9; h.Value.Move(); (h.Value).Move(); Line("field-in-place", 11, h.Value.X); }
    { var h = new Holder(); h.Value = new P(1); BumpRef(ref h.Value); Line("field-by-ref", 9, h.Value.X); }
    { var o = new Outer(); o.Inner = new P(1); o.Inner.X = 9; o.Inner.Move(); Line("nested-in-place", 10, o.Inner.X); }
    { var arr = new P[] { new P(1) }; arr[0].X = 9; arr[0].Move(); Line("element-in-place", 10, arr[0].X); }
    { var a = new P(1); ref P r = ref a; r.X = 9; Line("ref-local", 9, a.X); }
  }

  public static void Main() {
    Collections();
    Delegates();
    Stores();
    InPlace();
  }
}
