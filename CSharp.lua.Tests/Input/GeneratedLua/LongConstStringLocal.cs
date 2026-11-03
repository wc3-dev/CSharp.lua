using System;

class Program {
  const string Prefix = "abc";

  public static void Main() {
    const string Joined = Prefix + "0123456789012345678901234567890";
    Console.WriteLine(Joined);
    const string Literal = "0123456789012345678901234567890";
    Console.WriteLine(Literal);
    const string Short = Prefix + "def";
    Console.WriteLine(Short);
    const string Twice = Joined + "!";
    Console.WriteLine(Twice.Length + " " + Twice);
    Func<string> read = () => Joined;
    Console.WriteLine(read());
  }
}
