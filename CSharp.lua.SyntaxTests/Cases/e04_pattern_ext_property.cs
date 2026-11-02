using System;
class P { public int X; public P Inner; }
class Program { static void Main() { var p = new P { Inner = new P { X = 9 } }; Console.WriteLine(p is { Inner.X: 9 }); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
