using System;
using System.Collections.Generic;
class Program { static T Get<T>(List<T> l) where T : struct => l[0]; static void Main() { Console.WriteLine(Get(new List<int> { 4 })); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
