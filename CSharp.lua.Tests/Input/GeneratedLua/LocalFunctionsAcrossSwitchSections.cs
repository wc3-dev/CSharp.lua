using System;

class Program {
  static string Run(int k) {
    switch (k) {
      case 0:
        int Twice(int x) => x * 2;
        return "zero " + Twice(1);
      case 1:
        return "one " + Twice(5);
      default:
        return "other " + Triple(2);
    }

    static int Triple(int x) => x * 3;
  }

  static string Statements(int k) {
    var text = "";
    switch (k) {
      case 0:
        text = Shout("a");
        break;
      case 1:
        string Shout(string s) => Loud(s) + "!";
        string Loud(string s) => s.ToUpper();
        text = Shout("b");
        break;
      default:
        text = Shout("c") + Shout("d");
        break;
    }
    return text;
  }

  static string Label(int k) {
    int limit = 10;
    switch (k) {
      case var n when Big(n):
        return "big";
      case 0:
        bool Big(int n) => n > limit;
        return "zero";
      default:
        return "small";
    }
  }

  public static void Main() {
    Console.WriteLine(Run(0) + ", " + Run(1) + ", " + Run(2));
    Console.WriteLine(Statements(0) + " " + Statements(1) + " " + Statements(2));
    Console.WriteLine(Label(0) + " " + Label(5) + " " + Label(50));
  }
}
