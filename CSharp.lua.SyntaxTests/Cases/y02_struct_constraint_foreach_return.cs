using System;
using System.Collections.Generic;
class Program { static T First<T>(IEnumerable<T> e) where T : struct { foreach (var x in e) return x; return default; } static void Main() { Console.WriteLine(First(new[] { 4, 5 })); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
