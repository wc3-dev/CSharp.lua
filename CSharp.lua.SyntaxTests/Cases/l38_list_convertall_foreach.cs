using System;
using System.Collections.Generic;
class Program { static void Main() { var l = new List<int> { 1, 2 }; Console.WriteLine(string.Join(",", l.ConvertAll(x => x * 2))); l.ForEach(x => Console.Write(x)); Console.WriteLine(); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
