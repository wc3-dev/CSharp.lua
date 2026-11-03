using System;
class Program { static string F(object o) => o switch { int => "int", string => "string", _ => "?" }; static void Main() { Console.WriteLine(F(1)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
