using System;
using System.Linq;

class Groups {
  public static T Zero<T>(T x) => default(T);
  public static string Both<T>(T a, T b) => a + "," + b + "," + default(T);
  public static string Three<T>(T a, T b, T c) => a + "," + b + "," + c + "," + default(T);
  public static T Make<T>() => default(T);
  public static string Indexed<T>(T x, int i) => x + ":" + i + ":" + default(T);
}

class Program {
  static Func<int, int> Property { get; } = Groups.Zero<int>;
  static Func<int, int> Arrow => Groups.Zero<int>;

  static T Zero<T>(T x) => default(T);
  static T Apply<T>(Func<T, T> f, T x) => f(x);

  public static void Main() {
    bool yes = true;
    Func<int, int> a = yes ? Zero<int> : null;
    Func<int, int> b = yes ? Groups.Zero<int> : null;
    Func<int, int> c = Groups.Zero<int>;
    Console.WriteLine(a(5) + "," + b(6) + "," + c(7));
    Func<int, int, string> d = Groups.Both<int>;
    Console.WriteLine(d(1, 2));
    Func<int, int, int, string> e = Groups.Three<int>;
    Console.WriteLine(e(1, 2, 3));
    Func<int> f = Groups.Make<int>;
    Console.WriteLine(f());
    Console.WriteLine(new[] { 1, 2 }.Select(Groups.Zero<int>).Last());
    Console.WriteLine(new[] { 1, 2 }.Select(Groups.Indexed<int>).Last());
    Console.WriteLine(Apply(Groups.Zero<int>, 8));
    var g = (Func<int, int>)Groups.Zero<int>;
    Func<int, int> h = (Groups.Zero<int>);
    var i = new Func<int, int>(Groups.Zero<int>);
    Func<int, int>[] j = { Groups.Zero<int> };
    Func<Func<int, int>> k = () => Groups.Zero<int>;
    Console.WriteLine(Property(1) + "," + Arrow(1) + "," + g(1) + "," + h(1) + "," + i(1) + "," + j[0](1) + "," + k()(1));
  }
}
