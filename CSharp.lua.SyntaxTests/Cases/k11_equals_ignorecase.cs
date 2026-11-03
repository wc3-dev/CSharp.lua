using System;
class Program { static void Main() { Console.WriteLine("a".Equals("A", StringComparison.OrdinalIgnoreCase)); Console.WriteLine(string.Equals("a", "A", StringComparison.OrdinalIgnoreCase)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
