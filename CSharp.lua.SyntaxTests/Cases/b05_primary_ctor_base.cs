using System;
class Base(string n) { public string N = n; }
class Der(string n, int k) : Base(n) { public int K = k; }
class Program { static void Main() { var d = new Der("nn", 3); Console.WriteLine(d.N + d.K); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
