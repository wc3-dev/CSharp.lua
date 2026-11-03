using System;
class Program { static void Main() { var a = new { X = 1 }; var b = new { X = 1 }; Console.WriteLine(a.Equals(b) + " " + (a.GetHashCode() == b.GetHashCode())); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
