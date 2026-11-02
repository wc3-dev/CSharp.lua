using System;
class Program {
  struct V { public float X; public static V operator *(V a, float f) => new V { X = a.X * f }; }
  int count = 3;
  float f1 = 5; float f2;
  float G() => count;
  void A() { float a = 1f; float b = 16777216f + 1f; float c = (float)count; float d = count; Console.WriteLine(a + " " + b + " " + c + " " + d + " " + f1 + " " + f2 + " " + G()); }
  void B() { Func<float> fn = () => count; Console.WriteLine(fn()); }
  void C() { var v = new V { X = 2 } * count; Console.WriteLine(v.X); }
  void D() { Console.WriteLine(10f / 4 + " " + 7 / 2 + " " + 7 / 2f); }
  void E() { int i = 0; Console.WriteLine(i + " " + default(int)); }
  void F() { Console.WriteLine(1.5f + 2.5f); Console.WriteLine(4f); }
  void Run() { H.R("A", A); H.R("B", B); H.R("C", C); H.R("D", D); H.R("E", E); H.R("F", F); }
  static void Main() { new Program().Run(); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
