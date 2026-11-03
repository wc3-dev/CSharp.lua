using System;

class Program {
  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (OverflowException) {
      Console.WriteLine(name + ": overflow");
    }
  }

  public static void Main() {
    int max = int.MaxValue;
    int min = int.MinValue;
    int big = 50000;
    int one = 1;
    Show("Add", () => checked(max + one).ToString());
    Show("AddFits", () => checked(max - one + one).ToString());
    Show("Subtract", () => checked(min - one).ToString());
    Show("Multiply", () => checked(big * big).ToString());
    Show("MultiplyFits", () => checked(big * 40000).ToString());
    Show("MultiplyNegative", () => checked(min * -one).ToString());
    Show("Negate", () => checked(-min).ToString());
    Show("Block", () => {
      checked {
        int x = max;
        x++;
        return x.ToString();
      }
    });
    Show("Compound", () => {
      checked {
        int x = min;
        x -= one;
        return x.ToString();
      }
    });
    float huge = 3e9f;
    Show("Cast", () => checked((int)huge).ToString());
  }
}
