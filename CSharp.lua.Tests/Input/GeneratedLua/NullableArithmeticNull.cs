using System;

class Program {
  public static void Main() {
    int? none = null;
    int? some = 1;
    float? noneF = null;
    bool no = false;
    Console.WriteLine(none + 2 == null);
    Console.WriteLine(none + 2 != null);
    Console.WriteLine(null == none * 3);
    Console.WriteLine(some + 2 == null);
    Console.WriteLine(some - 2 != null);
    Console.WriteLine(noneF / 2f == null);
    Console.WriteLine(-none == null);
    Console.WriteLine(none + 2 == some);
    Console.WriteLine(some + 1 > 0 && no);
    Console.WriteLine(no && some + 1 > 0 || true);
    if (none + 2 == null) {
      Console.WriteLine("if");
    }
  }
}
