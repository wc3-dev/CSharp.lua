using System;
class Program { static void Main() { Console.WriteLine("Abc".StartsWith("ab", StringComparison.OrdinalIgnoreCase)); Console.WriteLine("Abc".EndsWith("BC", StringComparison.OrdinalIgnoreCase)); Console.WriteLine("Abc".Contains("bC", StringComparison.OrdinalIgnoreCase)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
