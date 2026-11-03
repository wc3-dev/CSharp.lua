using System;
record R(int A, string B);
record Derived(int A, string B, int C) : R(A, B);
class Program { static void Main() { var d = new Derived(1, "q", 3); Console.WriteLine(d); Console.WriteLine(d == new Derived(1, "q", 3)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
