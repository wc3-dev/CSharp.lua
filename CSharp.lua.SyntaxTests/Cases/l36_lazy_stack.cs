using System;
using System.Collections.Generic;
class Program { static void Main() { var s = new Stack<char>(); s.Push('a'); s.TryPop(out var z); Console.WriteLine(z); var q = new Queue<int>(); q.Enqueue(1); Console.WriteLine(q.TryPeek(out var y) + " " + y); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
