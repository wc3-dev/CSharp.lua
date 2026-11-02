using System;
using System.Collections.Generic;
class Handle { static Dictionary<string, int> known = new(); }
class Program { static void Main() { var d = new Dictionary<Handle, int>(); var a = new Handle(); d[a] = 2; Console.WriteLine(d[a]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
