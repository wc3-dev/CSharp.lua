using System;
struct P { public int X; }
class Program { static T Id<T>(T x) where T : struct => x; static void Main() { var p = new P { X = 3 }; var q = Id(p); q.X = 9; Console.WriteLine(p.X + " " + q.X); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
