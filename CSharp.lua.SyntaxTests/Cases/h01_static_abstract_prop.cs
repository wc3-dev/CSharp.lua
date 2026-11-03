using System;
interface IMake<T> { static abstract int Id { get; } }
class K : IMake<K> { public static int Id => 42; }
class Program { static int IdOf<T>() where T : IMake<T> => T.Id; static void Main() { Console.WriteLine(IdOf<K>()); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
