using System;
using System.Collections.Generic;

class Program {
  static readonly Dictionary<string, int> dict = new Dictionary<string, int> { ["a"] = 1 };

  static int WhenFalse(bool cond, string key) => cond ? 5 : dict.TryGetValue(key, out var v) ? v : 7;

  static int WhenTrue(bool cond, string key) => !cond ? (dict.TryGetValue(key, out var w) ? w : 8) : 6;

  static string Later(bool cond) {
    var y = cond ? 5 : dict.TryGetValue("a", out var later) ? later : 7;
    later = 4;
    return y + " " + later;
  }

  static string Coalesce(string key) {
    var s = key ?? (dict.TryGetValue("a", out var found) ? "x" + found : "y");
    found = 9;
    return s + " " + found;
  }

  static int InLoop() {
    var total = 0;
    foreach (var k in new[] { "a", "b", null }) {
      total += k == null ? 100 : dict.TryGetValue(k, out var n) ? n : 10;
    }
    return total;
  }

  public static void Main() {
    Console.WriteLine(WhenFalse(true, null) + " " + WhenFalse(false, "a") + " " + WhenFalse(false, "b"));
    Console.WriteLine(WhenTrue(true, null) + " " + WhenTrue(false, "a") + " " + WhenTrue(false, "b"));
    Console.WriteLine(Later(true) + " " + Later(false));
    Console.WriteLine(Coalesce("k") + " " + Coalesce(null));
    Console.WriteLine(InLoop());
  }
}
