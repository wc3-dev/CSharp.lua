using System;
using System.Collections.Generic;
struct P { public int X; }
class Program { static void Main() { var l = new List<P> { new P() }; var c = l[0]; c.X = 6; Console.WriteLine(l[0].X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
