using System;
enum Color { Red, Green = 5, Blue }
[Flags] enum F : byte { None = 0, A = 1, B = 2, C = 4, AB = A | B }
enum L : long { Big = 5000000000 }
class Program {
  static void T1() { Console.WriteLine(Color.Green + " " + (int)Color.Blue); }
  static void T2() { Console.WriteLine((Color)5 + " " + (Color)99); }
  static void T3() { Console.WriteLine(Color.Red.ToString()); }
  static void T4() { var f = F.A | F.C; Console.WriteLine(f + " " + (int)f + " " + f.HasFlag(F.A) + " " + f.HasFlag(F.B)); }
  static void T5() { var f = F.A | F.C; Console.WriteLine((f & F.A) + " " + F.AB + " " + (F)0 + " " + (~F.A & F.AB)); }
  static void T6() { Console.WriteLine(Enum.Parse<Color>("Blue")); }
  static void T7() { Console.WriteLine(Enum.IsDefined(typeof(Color), 5)); }
  static void T8() { Console.WriteLine(string.Join(",", Enum.GetNames<Color>())); }
  static void T9() { foreach (var v in Enum.GetValues<Color>()) Console.Write(v + ";"); Console.WriteLine(); }
  static void T10() { Console.WriteLine(Color.Green.CompareTo(Color.Blue) + " " + (Color.Red < Color.Blue) + " " + Color.Red.Equals(Color.Red)); }
  static void T11() { switch (Color.Green) { case Color.Red: Console.WriteLine("r"); break; case Color.Green: Console.WriteLine("g"); break; } }
  static void T12() { Console.WriteLine(L.Big); }
  static void T13() { Console.WriteLine(Enum.TryParse<Color>("Nope", out var z) + " " + z); }
  static void T14() { Console.WriteLine(Enum.GetName(typeof(Color), 5) + " " + Enum.GetUnderlyingType(typeof(F)).Name); }
  static void T15() { Color c = Color.Red; c++; Console.WriteLine(c); }
  static void T16() { Console.WriteLine((F.A | F.B).ToString() + " " + ((F)7) + " " + ((F)8)); }
  static void T17() { Console.WriteLine($"{Color.Blue} {Color.Blue:D} {(int)Color.Blue:X}"); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
