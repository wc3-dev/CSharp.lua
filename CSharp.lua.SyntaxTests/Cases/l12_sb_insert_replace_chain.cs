using System;
using System.Text;
class Program { static void Main() { var sb = new StringBuilder("hello"); sb.Insert(0, "oh ").Replace("l", "L").Remove(0, 1); Console.WriteLine(sb); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
