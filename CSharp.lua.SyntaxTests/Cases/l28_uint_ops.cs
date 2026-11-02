using System;
class Program { static void Main() { uint u = 0xFFFFFFFF; Console.WriteLine(u << 4); Console.WriteLine(~u); uint v = 0; v--; Console.WriteLine(v); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
