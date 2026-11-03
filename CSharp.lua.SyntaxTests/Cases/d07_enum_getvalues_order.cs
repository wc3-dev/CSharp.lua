using System;
enum Color { Red, Green = 5, Blue }
class Program { static void Main() { foreach (var v in Enum.GetValues<Color>()) Console.Write(v + ";"); Console.WriteLine(); Console.WriteLine(string.Join(",", Enum.GetNames<Color>())); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
