using System;
class Program { static void Main() { int x = int.MaxValue; try { int y = checked(x + 1); Console.WriteLine(y); } catch (OverflowException) { Console.WriteLine("overflow"); } } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
