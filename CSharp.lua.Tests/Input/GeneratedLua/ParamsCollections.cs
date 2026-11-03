using System;
using System.Collections.Generic;
using System.Linq;

delegate int Counted(params string[] names);

class Program {
  static int CountList(params List<int> items) => items.Count;
  static int SumEnumerable(params IEnumerable<int> items) => items.Sum();
  static int CountObjects(params object[] items) => items.Length;

  public static void Main() {
    Console.WriteLine(CountList(1, 2, 3) + " " + CountList() + " " + CountList(new List<int> { 4 }));
    Console.WriteLine(SumEnumerable(1, 2, 3) + " " + SumEnumerable() + " " + SumEnumerable(new[] { 5, 6 }));
    Console.WriteLine(CountObjects(1, "a") + " " + CountObjects(new object[] { 1, 2, 3 }) + " " + CountObjects(new[] { "x", "y" }));
    var lambda = (params int[] values) => values.Length;
    Console.WriteLine(lambda(1, 2, 3) + " " + lambda() + " " + lambda(new[] { 1 }));
    Counted counted = names => names.Length;
    Console.WriteLine(counted("a", "b") + " " + counted());
  }
}
