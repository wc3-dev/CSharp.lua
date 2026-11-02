using System;
struct M { public int V; public static M operator +(M a, M b) => new M { V = a.V + b.V }; }
class Program { static void Main() { Console.WriteLine((new M { V = 1 } + new M { V = 2 }).V); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
