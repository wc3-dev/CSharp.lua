using System;
interface IG<T> { T Get(); }
class E : IG<int>, IG<string> { int IG<int>.Get() => 1; string IG<string>.Get() => "s"; }
class Program { static void Main() { var e = new E(); Console.WriteLine(((IG<int>)e).Get() + ((IG<string>)e).Get()); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
