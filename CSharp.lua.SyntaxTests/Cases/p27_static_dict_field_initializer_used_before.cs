using System;
using System.Collections.Generic;
class Program { static Dictionary<int, string> d = new(); static void Main() { d[1] = "a"; Console.WriteLine(d[1]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
