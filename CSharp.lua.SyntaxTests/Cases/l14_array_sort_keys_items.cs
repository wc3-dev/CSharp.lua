using System;
class Program { static void Main() { var k = new[] { 3, 1, 2 }; var v = new[] { "c", "a", "b" }; Array.Sort(k, v); Console.WriteLine(string.Join("", v)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
