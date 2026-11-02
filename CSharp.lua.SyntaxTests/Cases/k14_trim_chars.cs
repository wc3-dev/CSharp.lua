using System;
class Program { static void Main() { Console.WriteLine("xxaxx".Trim('x') + "|" + "xxaxx".TrimStart('x') + "|" + "xxaxx".TrimEnd('x')); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
