using System;
class Base(string n) { public string N = n; }
class Der2(string n) : Base(n) { public string M() => n + N; }
class Program { static void Main() { Console.WriteLine(new Der2("q").M()); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
