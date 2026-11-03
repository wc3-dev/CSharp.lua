using System;
using System.Text;

enum Color {
  Red,
  Green = 5,
  Blue,
}

class Program {
  public static void Main() {
    char c = 'z';
    var e = Color.Blue;
    Console.WriteLine(string.Format("{0} {1}", c, e));
    Console.WriteLine(string.Concat("a", c, e));
    Console.WriteLine(string.Join(",", c, e, 3));
    Console.WriteLine((object)c);
    Console.WriteLine((object)e);
    var builder = new StringBuilder();
    builder.Append((object)c).Append(' ').Append((object)e).AppendFormat(" {0}", e);
    Console.WriteLine(builder.ToString());
  }
}
