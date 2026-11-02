using System;
using System.Collections.Generic;
class A { public static List<A> All = new(); public A() { All.Add(this); } }
class Program { static void Main() { new A(); new A(); Console.WriteLine(A.All.Count); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
