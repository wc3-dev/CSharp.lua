using System;

struct Point {
  public int X;
}

class Program {
  static int[] numbers = { 1, 2, 3 };

  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  static void Twice(ref int value) {
    value *= 2;
  }

  public static void Main() {
    Show("Write", () => {
      var a = new[] { 1, 2, 3 };
      ref int r = ref a[1];
      r = 5;
      return a[1] + " " + r;
    });
    Show("Compound", () => {
      var a = new[] { 1, 2, 3 };
      ref int r = ref a[2];
      r += 10;
      r++;
      ++r;
      r--;
      return a[2] + " " + r;
    });
    Show("IndexOnce", () => {
      var a = new[] { 1, 2, 3 };
      var i = 0;
      ref int r = ref a[i++];
      i = 2;
      r = 9;
      return a[0] + " " + a[2] + " " + i;
    });
    Show("ArrayOnce", () => {
      var a = new[] { 1, 2 };
      var old = a;
      ref int r = ref a[0];
      a = new[] { 7, 8 };
      r = 4;
      return old[0] + " " + a[0];
    });
    Show("FromEnd", () => {
      var a = new[] { 1, 2, 3 };
      ref int r = ref a[^1];
      r = 0;
      return a[0] + "," + a[1] + "," + a[2];
    });
    Show("RefArgument", () => {
      var a = new[] { 1, 2, 3 };
      ref int r = ref a[0];
      Twice(ref r);
      return a[0] + " " + r;
    });
    Show("RefOfRef", () => {
      var a = new[] { 1, 2, 3 };
      ref int r = ref a[1];
      ref int s = ref r;
      s = 6;
      return a[1] + " " + r;
    });
    Show("StructElement", () => {
      var points = new Point[2];
      ref Point p = ref points[1];
      p.X = 3;
      p.X += 1;
      return points[1].X + " " + p.X;
    });
    Show("StructCopy", () => {
      var points = new Point[2];
      ref Point p = ref points[0];
      var q = p;
      q.X = 9;
      return points[0].X + " " + q.X;
    });
    Show("ReadOnly", () => {
      var a = new[] { 4, 5 };
      ref readonly int r = ref a[0];
      a[0] = 8;
      return r.ToString();
    });
    Show("Grid", () => {
      var grid = new int[2, 2];
      ref int r = ref grid[1, 0];
      r = 7;
      return grid[1, 0] + " " + r;
    });
    Show("Static", () => {
      ref int r = ref numbers[0];
      r = 11;
      return numbers[0] + " " + r;
    });
    Show("Loop", () => {
      var a = new[] { 1, 2, 3 };
      for (var i = 0; i < a.Length; i++) {
        ref int r = ref a[i];
        r *= 10;
      }
      return a[0] + "," + a[1] + "," + a[2];
    });
    Show("OutOfRange", () => {
      var a = new[] { 1 };
      try {
        ref int r = ref a[3];
        return "no";
      } catch (Exception) {
        return "thrown";
      }
    });
    Show("Span", () => {
      Span<int> span = new[] { 1, 2 };
      ref int r = ref span[0];
      r = 5;
      return span[0] + " " + r;
    });
  }
}
