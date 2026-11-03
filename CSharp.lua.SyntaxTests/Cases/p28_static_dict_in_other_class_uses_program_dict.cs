using System;
using System.Collections.Generic;
class A { public static Dictionary<int, A> All = new(); }
class B { public static Dictionary<int, A> Other = new(); }
class Program { static void Main() { A.All[1] = new A(); B.Other[2] = A.All[1]; Console.WriteLine(B.Other.Count); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
