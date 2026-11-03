using System;
class Program { static void Main() { Console.WriteLine(string.Format("{0,5}|{1:F2}|{2:#,##0.00}", "ab", 3.14159f, 1234567.891)); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
