using System;
[Flags] enum F : byte { None = 0, A = 1, B = 2, C = 4, AB = A | B }
class Program { static void Main() { Console.WriteLine((~F.A & F.AB) + " x"); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
