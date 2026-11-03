using System;
partial class Outer { public class Inner { public int V = 3; public Outer Parent; } public int Q() => new Inner().V; }
partial class Outer { public int R() => Q() + 1; public enum E { X, Y } public struct S { public int A; } }
class Program {
  static void T1() { var o = new Outer(); var i = new Outer.Inner { Parent = o }; Console.WriteLine(o.R() + " " + i.V + " " + (i.Parent == o)); }
  static void T2() { Console.WriteLine(Outer.E.Y + " " + new Outer.S { A = 4 }.A); }
  static void T3() { Console.WriteLine(typeof(Outer.Inner).Name + " " + typeof(Outer.Inner).FullName); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
