using System;
abstract class Shape { public abstract double Area(); public virtual string Name() => "shape"; public string Describe() => Name() + ":" + Area(); public Shape Self() => this; }
class Sq : Shape { double s; public Sq(double s) { this.s = s; } public override double Area() => s * s; public override string Name() => "sq"; }
class Circ : Shape { public override double Area() => 3; }
class Sealed : Sq { public Sealed() : base(1) { } public sealed override string Name() => "sealed"; }
class Hide : Sq { public Hide() : base(2) { } public new string Name() => "hidden"; }
class Program {
  static void T1() { Shape s = new Sq(2); Console.WriteLine(s.Describe()); }
  static void T2() { Shape s = new Circ(); Console.WriteLine(s.Describe()); }
  static void T3() { Shape s = new Sealed(); Console.WriteLine(s.Name()); }
  static void T4() { Shape s = new Hide(); Console.WriteLine(s.Name() + " " + new Hide().Name()); }
  static void T5() { Shape[] a = { new Sq(1), new Circ() }; foreach (var s in a) Console.Write(s.Name() + ","); Console.WriteLine(); }
  static void T6() { Shape s = new Sq(3); Console.WriteLine((s is Sq) + " " + (s is Circ) + " " + (s.Self() == s)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
