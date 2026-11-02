using System;
class Program { static void Main() { int[] a = { 1, 2, 3 }; Console.WriteLine(a[^1]); a[^1] = 9; Console.WriteLine(a[2]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
