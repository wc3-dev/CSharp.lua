using System;
struct P { public int X; }
class H1 { public P F; }
class Program { static void Main() { var h = new H1(); var c = h.F; c.X = 6; Console.WriteLine(h.F.X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
