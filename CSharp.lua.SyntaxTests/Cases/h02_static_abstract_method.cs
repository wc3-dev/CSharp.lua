using System;
interface IMake<T> { static abstract T Make(); }
class K : IMake<K> { public static K Make() => new K(); }
class Program { static T Mk<T>() where T : IMake<T> => T.Make(); static void Main() { Console.WriteLine(Mk<K>().GetType().Name); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
