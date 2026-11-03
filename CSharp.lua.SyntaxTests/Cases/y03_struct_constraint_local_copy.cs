using System;
class Program { static T Twice<T>(T x) where T : struct { var y = x; return y; } static void Main() { Console.WriteLine(Twice(7)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
