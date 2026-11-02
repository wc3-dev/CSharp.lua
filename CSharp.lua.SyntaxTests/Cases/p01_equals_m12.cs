using System;
class P { public int X; public override bool Equals(object o) => o is P p && p.X == X; public override int GetHashCode() => X; public override string ToString() => "P" + X; }
class Program { static void Main() { Console.WriteLine(new P { X = 1 }.Equals(new P { X = 1 })); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
