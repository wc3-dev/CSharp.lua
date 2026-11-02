using System;
using System.Collections.Generic;
class Handle { static Dictionary<int, Handle> known = new(); public Handle() { known[1] = this; } }
class Program { static void Main() { var a = new Handle(); var d = new Dictionary<int, Handle>(); d[1] = a; Console.WriteLine(d.Count); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
