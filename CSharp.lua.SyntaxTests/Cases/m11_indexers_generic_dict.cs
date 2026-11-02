using System;
using System.Collections.Generic;
class Grid { int[,] d = new int[2, 3]; public int this[int r, int c] { get => d[r, c]; set => d[r, c] = value; } }
class Bag<T> { List<T> l = new List<T>(); public T this[int i] { get => l[i]; set { while (l.Count <= i) l.Add(default(T)); l[i] = value; } } public int Count => l.Count; }
class Program {
  static void T1() { var g = new Grid(); g[1, 2] = 5; Console.WriteLine(g[1, 2] + " " + g[0, 0]); }
  static void T2() { var b = new Bag<string>(); b[2] = "x"; Console.WriteLine(b.Count + " " + b[2] + (b[0] == null)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
