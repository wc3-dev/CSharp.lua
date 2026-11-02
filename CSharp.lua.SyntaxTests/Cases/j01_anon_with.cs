using System;
class Program { static void Main() { var o = new { A = 1 }; var p = o with { A = 2 }; Console.WriteLine(o.A + " " + p.A); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
