using System;

class Program {
  static string Size(int x) => x switch {
    < 0 => "negative",
    0 => "zero",
    >= 1 and <= 9 => "small",
    > 100 => "big",
    _ => "medium",
  };

  static string Odd(int x) => x switch {
    not 1 and not 3 => "not one or three",
    (1 or 3) => "one or three",
  };

  static string Named(int x) => x switch {
    var n when n % 2 == 0 => "even " + n,
    var n => "odd " + n,
  };

  static string Nulls(string s) => s switch {
    not null => "text " + s,
    null => "none",
  };

  static string Types(object o) => o switch {
    not string and not null => "not text",
    string s when s.Length > 2 => "long text",
    _ => "other",
  };

  public static void Main() {
    Console.WriteLine(Size(-5) + ", " + Size(0) + ", " + Size(4) + ", " + Size(500) + ", " + Size(50));
    Console.WriteLine(Odd(1) + ", " + Odd(2) + ", " + Odd(3));
    Console.WriteLine(Named(4) + ", " + Named(7));
    Console.WriteLine(Nulls("a") + ", " + Nulls(null));
    Console.WriteLine(Types(5) + ", " + Types("abc") + ", " + Types("a") + ", " + Types(null));
  }
}
