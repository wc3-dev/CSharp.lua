using System;
class P { public int X; }
class Program { static int F(P a0, P a1) => a0.X * 1 + a1.X * 10; static void Main() { Console.WriteLine(F(new P { X = 1 }, new P { X = 2 })); } }


static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
