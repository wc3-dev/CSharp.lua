using System;

class Program {
  static int Later(int k) {
    switch (k) {
      case 0:
        int y = 5;
        return y;
      case 1:
        y = 7;
        return y * 2;
      default:
        return -1;
    }
  }

  static string Several(int k) {
    switch (k) {
      case 0:
        string text;
        int a = 1, b = 2;
        text = "zero";
        return text + (a + b);
      case 1:
        text = "one";
        a = 10;
        Func<int> sum = () => a + 5;
        return text + sum();
      default:
        b = 20;
        return "other" + b;
    }
  }

  static string InALoop() {
    var reads = new Func<int>[3];
    for (int i = 0; i < 3; i++) {
      switch (i) {
        case 0:
          int seen = 100;
          reads[i] = () => seen;
          break;
        default:
          seen = i;
          reads[i] = () => seen;
          break;
      }
    }
    return reads[0]() + " " + reads[1]() + " " + reads[2]();
  }

  static int Moved(int k) {
    switch (k) {
      case 0:
        int m = 3;
        int AddM(int x) => x + m;
        return AddM(1);
      default:
        m = 4;
        return AddM(2);
    }
  }

  public static void Main() {
    Console.WriteLine(Later(0) + " " + Later(1) + " " + Later(2));
    Console.WriteLine(Several(0) + " " + Several(1) + " " + Several(2));
    Console.WriteLine(InALoop());
    Console.WriteLine(Moved(0) + " " + Moved(1));
  }
}
