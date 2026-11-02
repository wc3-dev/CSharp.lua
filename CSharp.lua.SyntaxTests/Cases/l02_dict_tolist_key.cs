using System;
using System.Collections.Generic;
using System.Linq;
class Program { static void Main() { var d = new Dictionary<string, int> { ["a"] = 1 }; var l = d.ToList(); Console.WriteLine(l[0].Key + l[0].Value); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
