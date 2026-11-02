using System;
using System.Collections.Generic;
class Handle { static Dictionary<int, Handle> known = new(); public Handle(int id) { known[id] = this; } public static int Count => known.Count; }
class Program { static void Main() { new Handle(1); Console.WriteLine(Handle.Count); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
