using System;
using System.Collections.Generic;
class Program { static IEnumerable<string> It() { try { yield return "a"; yield return "b"; } finally { Console.WriteLine("fin"); } } static void Main() { foreach (var s in It()) { Console.WriteLine(s); break; } } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
