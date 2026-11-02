using System;
class Program { static void Main() { Console.WriteLine(255.ToString("X") + " " + 5.ToString("D3") + " " + 1234567.ToString("N0") + " " + 12.ToString("00.0") + " " + (-5).ToString("D3")); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
