using System;
class Program { static void Main() { var a = new[] { 5, 3 }; (a[0], a[1]) = (a[1], a[0]); Console.WriteLine(a[0] + "" + a[1]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
