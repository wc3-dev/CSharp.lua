using System;
using System.Collections.Generic;
class Program { static int C(params List<int> xs) => xs.Count; static void Main() { Console.WriteLine(C(1, 2, 3)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
