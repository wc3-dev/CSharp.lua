using System;
using System.Collections.Generic;
abstract class Base { static Dictionary<int, Base> all = new(); protected Base(int i) { all[i] = this; } public static int N => all.Count; }
class D : Base { public D(int i) : base(i) { } }
class Program { static void Main() { new D(1); new D(2); Console.WriteLine(Base.N); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
