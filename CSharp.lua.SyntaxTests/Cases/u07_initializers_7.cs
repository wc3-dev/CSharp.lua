using System;
class P { public int X; }
class Program { static int F(P a0, P a1, P a2, P a3, P a4, P a5, P a6) => a0.X * 1 + a1.X * 10 + a2.X * 100 + a3.X * 1000 + a4.X * 10000 + a5.X * 100000 + a6.X * 1000000; static void Main() { Console.WriteLine(F(new P { X = 1 }, new P { X = 2 }, new P { X = 3 }, new P { X = 4 }, new P { X = 5 }, new P { X = 6 }, new P { X = 7 })); } }


static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
