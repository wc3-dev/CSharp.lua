using System;
using System.Linq;
class Program { static void Main() { Console.WriteLine(string.Join(",", new[] { "b", "B", "a", "A" }.OrderBy(x => x))); Console.WriteLine(string.Join(",", new[] { "b", "B", "a", "A" }.OrderBy(x => x, StringComparer.Ordinal))); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
