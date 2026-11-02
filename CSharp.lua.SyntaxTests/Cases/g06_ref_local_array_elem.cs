using System;
class Program { static void Main() { var a = new[] { 1, 2 }; ref int r = ref a[1]; r = 5; Console.WriteLine(a[1]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
