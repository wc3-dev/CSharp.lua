using System;
interface I1 { string Who(); } interface I2 { string Who(); }
class C : I1, I2 { string I1.Who() => "i1"; string I2.Who() => "i2"; public string Who() => "c"; }
interface IP { int P { get; } }
class D : IP { int IP.P => 5; }
interface IG<T> { T Get(); }
class E : IG<int>, IG<string> { int IG<int>.Get() => 1; string IG<string>.Get() => "s"; }
class Program {
  static void T1() { var c = new C(); Console.WriteLine(c.Who() + ((I1)c).Who() + ((I2)c).Who()); }
  static void T2() { IP p = new D(); Console.WriteLine(p.P); }
  static void T3() { var e = new E(); Console.WriteLine(((IG<int>)e).Get() + ((IG<string>)e).Get()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
