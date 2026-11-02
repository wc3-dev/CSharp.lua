using System;
class Program { static void Main() { byte b = 250; b += 10; Console.WriteLine(b); short s = 32767; s++; Console.WriteLine(s); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
