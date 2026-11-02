using System;
struct V { public int X; public static V operator ++(V a) => new V { X = a.X + 1 }; }
class Program { static void Main() { var a = new V(); a++; Console.WriteLine(a.X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
