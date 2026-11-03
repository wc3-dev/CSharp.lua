using System;

class Pair {
  public int X;
  public int Y;

  public void Deconstruct(out int x, out int y) {
    x = X;
    y = Y;
  }
}

record Point(int X, int Y);

struct Cell {
  public int X;
  public int Y;

  public void Deconstruct(out int x, out int y) {
    x = X;
    y = Y;
  }
}

class Program {
  static string Arm(Pair p) => p switch {
    (1, 2) => "one-two",
    null => "null",
    _ => "other",
  };

  static string Case(Pair p) {
    switch (p) {
      case (1, 2):
        return "one-two";
      case null:
        return "null";
      default:
        return "other";
    }
  }

  static string Record(Point p) => p switch {
    (0, var y) => "y " + y,
    null => "null",
    _ => "other",
  };

  static string Nullable(Cell? c) => c switch {
    (1, _) => "one",
    null => "null",
    _ => "other",
  };

  static string Object(object o) => o switch {
    Pair (3, 4) => "pair",
    null => "null",
    _ => "other",
  };

  static string Two(object o) => o switch {
    Pair (1, 2) => "pair",
    Point (3, 4) => "point",
    null => "null",
    _ => "other",
  };

  public static void Main() {
    Console.WriteLine(Arm(new Pair { X = 1, Y = 2 }) + " " + Arm(null) + " " + Arm(new Pair()));
    Console.WriteLine(Case(new Pair { X = 1, Y = 2 }) + " " + Case(null) + " " + Case(new Pair()));
    Console.WriteLine(Record(new Point(0, 5)) + " " + Record(null) + " " + Record(new Point(1, 1)));
    Console.WriteLine(Nullable(new Cell { X = 1 }) + " " + Nullable(null) + " " + Nullable(new Cell()));
    Console.WriteLine(Object(new Pair { X = 3, Y = 4 }) + " " + Object(null) + " " + Object("x"));
    Console.WriteLine(Two(new Pair { X = 1, Y = 2 }) + " " + Two(new Point(3, 4)) + " " + Two(null) + " " + Two("x") + " " + Two(new Point(1, 2)));
  }
}
