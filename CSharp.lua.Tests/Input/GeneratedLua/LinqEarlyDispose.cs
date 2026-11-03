using System;
using System.Collections.Generic;
using System.Linq;

class Program {
  static readonly List<string> Log = new();

  static IEnumerable<int> Numbers() {
    try {
      yield return 1;
      yield return 2;
      yield return 3;
    } finally {
      Log.Add("finally");
    }
  }

  static void Show(string name, object result) {
    Console.WriteLine(name + " " + result + " " + string.Join(",", Log));
    Log.Clear();
  }

  public static void Main() {
    Show("First", Numbers().First());
    Show("FirstWhere", Numbers().First(i => i == 2));
    Show("FirstOrDefault", Numbers().FirstOrDefault(i => i == 2));
    Show("Any", Numbers().Any());
    Show("AnyWhere", Numbers().Any(i => i == 1));
    Show("All", Numbers().All(i => i == 5));
    Show("Contains", Numbers().Contains(2));
    Show("ElementAt", Numbers().ElementAt(1));
    Show("ElementAtOrDefault", Numbers().ElementAtOrDefault(0));
    Show("SequenceEqual", Numbers().SequenceEqual(new[] { 1, 5, 3 }));
    Show("SequenceEqualShort", new[] { 1, 2 }.SequenceEqual(Numbers()));
    try {
      Numbers().Single();
    } catch (InvalidOperationException) {
      Show("Single", "throws");
    }
    try {
      Numbers().Single(i => i > 1);
    } catch (InvalidOperationException) {
      Show("SingleWhere", "throws");
    }
    Show("TakeFirst", Numbers().Take(2).First());
    Show("WhereFirst", Numbers().Where(i => i > 1).First());
    Show("Last", Numbers().Last());
    Show("Count", Numbers().Count());
  }
}
