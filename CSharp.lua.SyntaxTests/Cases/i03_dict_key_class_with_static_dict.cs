using System;
using System.Collections.Generic;
class Handle { static Dictionary<int, Handle> known = new(); public Handle(int id) { known[id] = this; } }
class Program { static void Main() { var d = new Dictionary<Handle, int>(); var a = new Handle(1); d[a] = 2; Console.WriteLine(d[a]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
