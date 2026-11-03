using System;
using System.Collections.Generic;
class Program { static void Main() { var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Ab"] = 1 }; Console.WriteLine(d["aB"]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
