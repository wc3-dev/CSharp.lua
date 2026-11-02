using System;
using System.Collections.Generic;
interface IOut<out T> { T Get(); }
interface IIn<in T> { void Put(T t); }
class S : IOut<string> { public string Get() => "s"; }
class P : IIn<object> { public void Put(object o) { Console.WriteLine("put " + o); } }
class Program {
  static void T1() { IOut<object> o = new S(); Console.WriteLine(o.Get()); }
  static void T2() { IIn<string> i = new P(); i.Put("z"); }
  static void T3() { IEnumerable<object> e = new List<string> { "a", "b" }; foreach (var x in e) Console.Write(x); Console.WriteLine(); }
  static void T4() { Func<object, string> f = ob => "f" + ob; Func<string, object> g = f; Console.WriteLine(g("q")); }
  static void T5() { object[] arr = new string[] { "a" }; Console.WriteLine(arr.Length + " " + arr[0]); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
