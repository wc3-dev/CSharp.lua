using System;
class Program { static void Main() { decimal a = 0.1m, b = 0.2m; Console.WriteLine(a + b == 0.3m); Console.WriteLine(decimal.MaxValue); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
