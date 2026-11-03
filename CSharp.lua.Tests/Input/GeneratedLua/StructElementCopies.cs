using System;
using System.Collections.Generic;

struct P {
  public int X;
  public P(int x) { X = x; }
  public void Move() { X++; }
}

class Program {
  static void Bump(ref P p) { p.X += 100; }
  static int Keep(P p) { p.X = 50; return p.X; }
  static P First(P[] items) { return items[0]; }

  public static void Main() {
    var array = new P[] { new P(1) };
    var a = array[0];
    a.X = 6;
    a.Move();
    Bump(ref a);
    Console.WriteLine(array[0].X + " " + a.X);
    var list = new List<P> { new P(2) };
    P l;
    l = list[0];
    l.X = 7;
    Console.WriteLine(list[0].X + " " + l.X);
    var dict = new Dictionary<int, P> { [1] = new P(3) };
    var d = dict[1];
    d.Move();
    Console.WriteLine(dict[1].X + " " + d.X);
    Console.WriteLine(Keep(array[0]) + " " + array[0].X);
    var f = First(array);
    f.X = 9;
    Console.WriteLine(array[0].X + " " + f.X);
    array[0].X = 4;
    Console.WriteLine(array[0].X);
  }
}
