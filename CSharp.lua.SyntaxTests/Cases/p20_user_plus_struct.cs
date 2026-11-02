using System;
struct V { public int X; public static V operator +(V a, V b) => new V { X = a.X + b.X }; }
class Program { static void Main() { var a = new V { X = 1 }; a += new V { X = 2 }; Console.WriteLine(a.X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
