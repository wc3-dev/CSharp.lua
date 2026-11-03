using System;

enum Color {
  Red,
  Green = 5,
  Blue,
}

[Flags]
enum F {
  None = 0,
  A = 1,
  B = 2,
  C = 4,
  AB = A | B,
}

class Program {
  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  public static void Main() {
    var c = Color.Blue;
    int five = 5;
    Show("Name", () => c.ToString() + " " + c + " " + $"{c}");
    Show("FromVariable", () => ((Color)five).ToString() + " " + (Color)five);
    Show("Unknown", () => ((Color)99).ToString() + " " + (Color)five + " x");
    Show("ConstantCast", () => (Color)5 + " x");
    Show("FlagsCombination", () => (F.A | F.C).ToString() + " " + (F.A | F.B) + " " + F.None + " " + (F)8);
    Show("FlagsZero", () => (F)0 + " x");
    Show("Parse", () => Enum.Parse<Color>("Green").ToString() + " " + Enum.Parse<Color>("6") + " " + Enum.Parse<F>("A, C"));
    Show("Aligned", () => $"[{c,6}] [{c,-6}] [{F.A | F.C,7}]");
    Show("GetValues", () => string.Join(";", Enum.GetValues<Color>()));
    Show("GetNames", () => string.Join(";", Enum.GetNames<Color>()) + " " + string.Join(";", Enum.GetNames(typeof(F))));
    Show("Formats", () => c.ToString("D") + " " + c.ToString("X") + " " + c.ToString("G") + " " + (F.A | F.C).ToString("D") + " " + $"{c:D}");
  }
}
