using System;
class Pub {
  public event Action<int> Fired; public event EventHandler Plain;
  public void Fire(int x) { Fired?.Invoke(x); Plain?.Invoke(this, EventArgs.Empty); }
  public static event Action SE; public static void FireS() { SE?.Invoke(); }
  public event Action Custom { add { Console.WriteLine("add"); } remove { Console.WriteLine("remove"); } }
}
class Program {
  static void T1() { var p = new Pub(); p.Fired += x => Console.WriteLine("got " + x); p.Fired += x => Console.WriteLine("also " + x); p.Fire(5); }
  static void T2() { var p = new Pub(); Action<int> h = x => Console.WriteLine("h" + x); p.Fired += h; p.Fire(1); p.Fired -= h; p.Fire(2); Console.WriteLine("done"); }
  static void T3() { var p = new Pub(); p.Plain += (s, e) => Console.WriteLine(s == p); p.Fire(0); }
  static void T4() { Pub.SE += () => Console.WriteLine("static"); Pub.FireS(); }
  static void T5() { var p = new Pub(); Action a = () => { }; p.Custom += a; p.Custom -= a; }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
