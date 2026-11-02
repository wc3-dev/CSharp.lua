using System;
class R : IDisposable { string n; public R(string n) { this.n = n; } public void Dispose() { Console.WriteLine("close " + n); } }
class Program { static void Main() { using var a = new R("a"); using var b = new R("b"); Console.WriteLine("body"); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
