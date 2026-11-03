using System;

class Point {
  public int X;
  public int Y;
  public Point Inner;
}

class Box {
  public int Size { get; set; }
}

class Program {
  static string Members(Point p) => p switch {
    { X: var x, Y: 0 } => "on the axis at " + x,
    { X: int x, Inner: { Y: var y } } => "inner " + x + " " + y,
    { Y: var y } => "y " + y,
    null => "null",
  };

  static string Case(Point p) {
    switch (p) {
      case { X: var x } when x > 5:
        return "far " + x;
      case { Inner.X: var ix }:
        return "inner x " + ix;
      default:
        return "other";
    }
  }

  static string Is(Point p) {
    if (p is { X: var x, Y: > 0 and var y }) {
      return "is " + x + " " + y;
    }
    return "is not";
  }

  static string Kept(Point p) {
    if (p is { X: var x }) {
      p.X = 99;
      return "kept " + x;
    }
    return "none";
  }

  static string Typed(object o) => o is Box { Size: var size } ? "box " + size : "not a box";

  public static void Main() {
    Console.WriteLine(Members(new Point { X = 3 }) + ", " + Members(new Point { X = 2, Y = 1, Inner = new Point { Y = 8 } }) + ", " + Members(new Point { X = 1, Y = 4 }) + ", " + Members(null));
    Console.WriteLine(Case(new Point { X = 9 }) + ", " + Case(new Point { X = 1, Inner = new Point { X = 6 } }) + ", " + Case(new Point()) + ", " + Case(null));
    Console.WriteLine(Is(new Point { X = 1, Y = 2 }) + ", " + Is(new Point { X = 1 }) + ", " + Is(null));
    Console.WriteLine(Kept(new Point { X = 5 }));
    Console.WriteLine(Typed(new Box { Size = 4 }) + ", " + Typed("text") + ", " + Typed(null));
  }
}
