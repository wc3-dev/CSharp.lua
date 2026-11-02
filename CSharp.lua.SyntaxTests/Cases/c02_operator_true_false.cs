using System;
struct V { public int X; public static bool operator true(V a) => a.X != 0; public static bool operator false(V a) => a.X == 0; }
class Program { static void Main() { Console.WriteLine(new V { X = 0 } ? "t" : "f"); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
