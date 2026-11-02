using System;
class P { public int X; public int G(P o) => X * 10 + o.X; }
class Program { static void Main() { Console.WriteLine(new P { X = 1 }.G(new P { X = 2 }) + " " + new P { X = 3 }.G(new P { X = 4 })); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
