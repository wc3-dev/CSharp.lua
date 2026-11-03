using System;
interface IAdd<T> where T : IAdd<T> { static abstract T Zero { get; } static abstract T operator +(T a, T b); }
interface IMake<T> { static abstract T Make(); static abstract int Id { get; } }
struct M : IAdd<M> { public int V; public static M Zero => new M { V = 0 }; public static M operator +(M a, M b) => new M { V = a.V + b.V }; }
class K : IMake<K> { public static K Make() => new K(); public static int Id => 42; }
class Program {
  static T Sum<T>(T[] xs) where T : IAdd<T> { var s = T.Zero; foreach (var x in xs) s = s + x; return s; }
  static T Zero<T>() where T : IAdd<T> => T.Zero;
  static int IdOf<T>() where T : IMake<T> => T.Id;
  static T Mk<T>() where T : IMake<T> => T.Make();
  static void T1() { Console.WriteLine(Sum(new[] { new M { V = 1 }, new M { V = 2 } }).V); }
  static void T2() { Console.WriteLine(Zero<M>().V); }
  static void T3() { Console.WriteLine(IdOf<K>()); }
  static void T4() { Console.WriteLine(Mk<K>().GetType().Name); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
