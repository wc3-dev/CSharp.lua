using System;
class Program { static void Main() { Console.WriteLine("a b  c".Split(' ', StringSplitOptions.RemoveEmptyEntries).Length); Console.WriteLine("one--two".Split("--").Length); Console.WriteLine("a1b2c".Split(new[] { '1', '2' }).Length); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
