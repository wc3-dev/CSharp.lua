using System;
class Program { static string F(int x) => x switch { 1 => "one", > 10 => "big", _ => "other" }; static void Main() { Console.WriteLine(F(11)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
