using System;
using System.Collections.Generic;

struct P {
  public int X;
  public P(int x) { X = x; }
}

class Program {
  static P field;

  static bool Get(Dictionary<int, P> d, out P p) => d.TryGetValue(1, out p);

  public static void Main() {
    var dict = new Dictionary<int, P> { [1] = new P(1) };
    dict.TryGetValue(1, out var a);
    a.X = 10;
    Console.WriteLine(dict[1].X + " " + a.X);

    P b;
    if (dict.TryGetValue(1, out b)) {
      b.X = 20;
    }
    Console.WriteLine(dict[1].X + " " + b.X);

    if (dict.TryGetValue(1, out var c) && c.X == 1) {
      c.X = 30;
    }
    Console.WriteLine(dict[1].X + " " + c.X);

    var items = new P[1];
    dict.TryGetValue(1, out items[0]);
    items[0].X = 40;
    Console.WriteLine(dict[1].X + " " + items[0].X);

    dict.TryGetValue(1, out field);
    field.X = 50;
    Console.WriteLine(dict[1].X + " " + field.X);

    Get(dict, out var e);
    e.X = 60;
    Console.WriteLine(dict[1].X + " " + e.X);

    var sorted = new SortedDictionary<int, P> { [1] = new P(2) };
    sorted.TryGetValue(1, out var s);
    s.X = 70;
    Console.WriteLine(sorted[1].X + " " + s.X);
  }
}
