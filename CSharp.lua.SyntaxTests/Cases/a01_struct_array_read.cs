using System;
struct P { public int X; }
class Program { static void Main() { var arr = new P[1]; var c = arr[0]; c.X = 6; Console.WriteLine(arr[0].X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
