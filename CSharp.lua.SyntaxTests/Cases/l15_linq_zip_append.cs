using System;
using System.Linq;
class Program { static void Main() { var a = new[] { 1, 2 }; var b = new[] { "x", "y" }; Console.WriteLine(string.Join(",", a.Zip(b, (i, s) => i + s))); Console.WriteLine(string.Join(",", a.Append(9).Prepend(0))); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
