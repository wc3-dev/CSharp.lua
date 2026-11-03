using System;
class Program { static void Main() { Console.WriteLine(Math.Round(2.567, 2)); Console.WriteLine(Math.Round(2.5)); Console.WriteLine(Math.Round(-0.5)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
