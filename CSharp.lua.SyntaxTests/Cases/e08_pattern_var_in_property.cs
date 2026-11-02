using System;
class P { public int X, Y; }
class Program { static void Main() { var p = new P { X = 1, Y = 7 }; Console.WriteLine(p is { X: 1, Y: var y } ? y : -1); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
