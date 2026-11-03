using System;
using System.Collections.Generic;
class Program { static void Main() { var a = new HashSet<int> { 1, 2, 3 }; Console.WriteLine(a.SetEquals(new[] { 3, 2, 1 })); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
