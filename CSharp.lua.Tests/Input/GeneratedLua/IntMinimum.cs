using System;
static class K { public const int Low = int.MinValue; }
enum Bits { Low = 1, Sign = 1 << 31 }
enum Wide : long { Least = int.MinValue }
class Program {
  const int Field = -2147483648;
  static int Given(int value = int.MinValue) => value;
  static void Main() {
    var a = int.MinValue;
    Console.WriteLine(a);
    Console.WriteLine((a | 1) + " " + (a ^ -1) + " " + (a >> 31));
    Console.WriteLine(Field | 1);
    Console.WriteLine(K.Low ^ -1);
    const int local = -2147483648;
    Console.WriteLine(local & 0x7fffffff);
    Console.WriteLine(1 << 31);
    Console.WriteLine(unchecked((int)0x80000000) == int.MinValue);
    Console.WriteLine(Given());
    long wide = -2147483648L;
    Console.WriteLine(wide);
    var written = -2147483648;
    Console.WriteLine(written | 1);
    var bits = Bits.Sign;
    Console.WriteLine((int)(bits | Bits.Low) + " " + (long)Wide.Least + " " + bits.ToString() + " " + Wide.Least.ToString());
    switch (a) {
      case int.MinValue:
        Console.WriteLine("lowest");
        break;
    }
  }
}
