using System;
using System.Collections.Generic;
abstract class Handle { static Dictionary<int, Handle> known = new(); protected Handle(int id) { known[id] = this; } }
class Unit : Handle { public Unit(int id) : base(id) { } }
class Program { static void Main() { var d = new Dictionary<Unit, int>(); var a = new Unit(10); d[a] = 1; Console.WriteLine(d[a]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
