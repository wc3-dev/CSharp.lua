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

class Holder {
  public Pair Pair;
  public (int, int) Tuple;
}

class Program {
  static string Is(Pair p) => p is (1, 2) ? "one-two" : "not";
  static string Record(Point p) => p is (0, var y) ? "y " + y : "not";
  static string Nullable(Cell? c) => c is (1, _) ? "one" : "not";
  static string Object(object o) => o is Pair (3, 4) ? "pair" : "not";
  static string Nested(Holder h) => h is { Pair: (5, 6) } ? "nested" : "not";
  static string NestedTuple(Holder h) => h is { Tuple: (7, 8) } ? "tuple" : "not";
  static string Named((int A, int B) t) => t is (A: 1, B: 2) ? "named" : "not";
  static string NamedRecord(Point p) => p is (X: 3, Y: _) ? "named record" : "not";

  public static void Main() {
    Console.WriteLine(Is(new Pair { X = 1, Y = 2 }) + " " + Is(null) + " " + Is(new Pair()));
    Console.WriteLine(Record(new Point(0, 5)) + " " + Record(null) + " " + Record(new Point(1, 1)));
    Console.WriteLine(Nullable(new Cell { X = 1 }) + " " + Nullable(null) + " " + Nullable(new Cell()));
    Console.WriteLine(Object(new Pair { X = 3, Y = 4 }) + " " + Object(null) + " " + Object("x"));
    Console.WriteLine(Nested(new Holder { Pair = new Pair { X = 5, Y = 6 } }) + " " + Nested(new Holder()) + " " + Nested(null));
    Console.WriteLine(NestedTuple(new Holder { Tuple = (7, 8) }) + " " + NestedTuple(new Holder()));
    Console.WriteLine(Named((1, 2)) + " " + Named((2, 1)));
    Console.WriteLine(NamedRecord(new Point(3, 9)) + " " + NamedRecord(new Point(4, 9)) + " " + NamedRecord(null));
  }
}
