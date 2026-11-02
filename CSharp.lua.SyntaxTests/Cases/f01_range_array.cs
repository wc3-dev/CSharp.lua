using System;
class Program { static void Main() { int[] a = { 1, 2, 3, 4, 5 }; var s = a[1..3]; Console.WriteLine(s.Length); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
