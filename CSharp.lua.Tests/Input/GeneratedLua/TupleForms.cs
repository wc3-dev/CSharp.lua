using System;

class Holder {
  public int Count { get; set; }
}

class Program {
  static (int, string) Pair() => (3, "c");

  public static void Main() {
    var (a, b) = (1, 2);
    Console.WriteLine(a + b);
    var (s, t) = ("x", 2.5f);
    Console.WriteLine(s + " " + t);
    (var u, var v) = (4, 5);
    Console.WriteLine(u * v);
    var items = new[] { 1, 2, 3 };
    (items[0], items[2]) = (items[2], items[0]);
    Console.WriteLine(items[0] + " " + items[2]);
    var holder = new Holder();
    int x = 1, y = 2;
    (holder.Count, x, y) = (x, y, 9);
    Console.WriteLine(holder.Count + " " + x + " " + y);
    var pair = (1, "one");
    Console.WriteLine(pair);
    Console.WriteLine("pair " + pair);
    Console.WriteLine($"{pair}");
    Console.WriteLine(Pair().ToString());
    (int, (string, bool)) nested = (7, ("n", true));
    Console.WriteLine(nested);
    (float, char) other = (1.5f, 'z');
    Console.WriteLine(other);
  }
}
