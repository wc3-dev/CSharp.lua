using System;
using System.Collections.Generic;
class Program { static void Main() { var d = new Dictionary<string, int> { ["x"] = 1 }; d.Remove("x", out var rem); Console.WriteLine(rem); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
