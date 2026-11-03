using System;
using System.Linq;

class Program {
  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  public static void Main() {
    Show("SplitString", () => string.Join("|", "a--b--c".Split("--")));
    Show("SplitStringOptions", () => string.Join("|", "a, b,, c".Split(", ", StringSplitOptions.RemoveEmptyEntries)));
    Show("SplitStrings", () => string.Join("|", "a+b; c".Split(new[] { "+", "; " }, StringSplitOptions.None)));
    Show("SplitStringCount", () => string.Join("|", "a--b--c".Split("--", 2)));
    Show("SplitDot", () => string.Join("|", "1.5.2".Split('.')));
    Show("StartsWithChar", () => "abc".StartsWith('a') + " " + "abc".StartsWith('b') + " " + "".StartsWith('a'));
    Show("EndsWithChar", () => "abc".EndsWith('c') + " " + "abc".EndsWith('b') + " " + "".EndsWith('c'));
    Show("IndexOfIgnoreCase", () => "Hello".IndexOf("LL", StringComparison.OrdinalIgnoreCase) + " " + "Hello".IndexOf("x", StringComparison.OrdinalIgnoreCase));
    Show("IndexOfFrom", () => "Hello hello".IndexOf("HE", 1, StringComparison.OrdinalIgnoreCase).ToString());
    Show("IndexOfOrdinal", () => "Hello".IndexOf("ll", StringComparison.Ordinal) + " " + "Hello".IndexOf("LL", StringComparison.Ordinal));
    Show("IndexOfSign", () => "a+b-c".IndexOf("-") + " " + "a+b-c".IndexOf("+b") + " " + "50%".IndexOf("%"));
    Show("ContainsSign", () => "a.b".Contains("-") + " " + "a-b".Contains("-"));
    Show("StartsWithIgnoreCase", () => "Hello".StartsWith("he", StringComparison.OrdinalIgnoreCase) + " " + "Hello".StartsWith("he", StringComparison.Ordinal));
    Show("EndsWithIgnoreCase", () => "Hello".EndsWith("LO", StringComparison.OrdinalIgnoreCase) + " " + "Hello".EndsWith("LO", StringComparison.Ordinal));
    Show("Reverse", () => new string("abc".Reverse().ToArray()));
    Show("Where", () => string.Join(",", "abc".Where(c => c != 'b')));
    Show("Take", () => string.Join(",", "abc".Take(2)));
    Show("Distinct", () => string.Join(",", "aab".Distinct()));
    Show("OrderBy", () => string.Join(",", "cab".OrderBy(c => c)));
    Show("Concat", () => string.Join(",", "ab".Concat("cd")));
    Show("ToList", () => "abc".ToList().Count.ToString());
  }
}
