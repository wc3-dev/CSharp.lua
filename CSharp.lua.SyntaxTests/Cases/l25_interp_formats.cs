using System;
class Program { static void Main() { Console.WriteLine($"{0.5f:P0}"); Console.WriteLine($"{1234567:N0}"); Console.WriteLine($"{5:00}"); Console.WriteLine($"{12345.678f:E2}"); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
