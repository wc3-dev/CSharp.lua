using System;
using System.Linq;
class Program {
  static void T1() { string s = "Hello, World"; Console.WriteLine(s.Length + " " + s.ToUpper() + " " + s.ToLower() + " " + s.Substring(7) + " " + s.Substring(0, 5) + " " + s[4]); }
  static void T2() { string s = "Hello, World"; Console.WriteLine(s.IndexOf('o') + " " + s.IndexOf("World") + " " + s.LastIndexOf('o') + " " + s.IndexOf('o', 5) + " " + s.IndexOf("zz") + " " + s.IndexOfAny(new[] { 'W', 'H' })); }
  static void T3() { string s = "Hello, World"; Console.WriteLine(s.Contains("lo,") + " " + s.StartsWith("Hel") + " " + s.EndsWith("ld") + " " + s.Contains('W') + " " + s.StartsWith('H') + " " + s.EndsWith('x')); }
  static void T4() { string s = "a,b,,c"; var p = s.Split(','); Console.WriteLine(p.Length + " " + string.Join("|", p)); var q = s.Split(',', StringSplitOptions.RemoveEmptyEntries); Console.WriteLine(q.Length); }
  static void T5() { Console.WriteLine("  pad ".Trim() + "|" + "  pad ".TrimStart() + "|" + "  pad ".TrimEnd() + "|" + "xxaxx".Trim('x') + "|" + "xxaxx".TrimStart('x')); }
  static void T6() { Console.WriteLine("abc".PadLeft(6) + "|" + "abc".PadRight(6, '.') + "|" + "abc".PadLeft(6, '0') + "|" + "abcdef".PadLeft(3)); }
  static void T7() { Console.WriteLine("a-b-c".Replace("-", "+") + " " + "aaa".Replace("a", "bb") + " " + "abc".Replace('b', 'X') + " " + "abc".Replace("x", "y") + " " + "abc".Remove(1) + " " + "abc".Remove(1, 1) + " " + "ac".Insert(1, "b")); }
  static void T8() { Console.WriteLine(string.Join(", ", new[] { "a", "b" }) + " " + string.Join("-", 1, 2, 3) + " " + string.Join("", new[] { 'x', 'y' }) + " " + string.Concat("a", "b", "c") + " " + string.Concat(new[] { "p", "q" })); }
  static void T9() { Console.WriteLine(string.IsNullOrEmpty("") + " " + string.IsNullOrEmpty(null) + " " + string.IsNullOrWhiteSpace("  ") + " " + string.IsNullOrEmpty("a") + " " + (string.Empty == "")); }
  static void T10() { Console.WriteLine("abc" == "abc" + "" + " " + ("ab" + "c" == "abc") + " " + "a".Equals("A") + " " + "a".Equals("A", StringComparison.OrdinalIgnoreCase) + " " + string.Equals("x", "x") + " " + ("a" != "b")); }
  static void T11() { Console.WriteLine("abc".CompareTo("abd") + " " + string.Compare("b", "a") + " " + "b".CompareTo("a") + " " + string.CompareOrdinal("a", "b") + " " + "".CompareTo("a")); }
  static void T12() { Console.WriteLine(new string('-', 5) + " " + new string(new[] { 'a', 'b' }) + " " + "ab".ToCharArray().Length + " " + string.Copy("x") + " " + "x".Clone()); }
  static void T13() { Console.WriteLine("a b  c".Split(' ').Length + " " + "a b  c".Split(' ', StringSplitOptions.RemoveEmptyEntries).Length + " " + "a1b2c".Split(new[] { '1', '2' }).Length + " " + "one--two".Split("--").Length + " " + "abc".Split(',').Length); }
  static void T14() { string s = "x"; s += "y"; s += 1; s += 'c'; s += 2.5f; s += true; Console.WriteLine(s); }
  static void T15() { Console.WriteLine("Hello".Reverse().Count() + " " + new string("Hello".Reverse().ToArray()) + " " + "hello".Count(c => c == 'l') + " " + "hello".First() + " " + "hello".Last() + " " + "abc".Select(c => (char)(c + 1)).Aggregate("", (a, c) => a + c)); }
  static void T16() { Console.WriteLine("hello world".Split(' ').Select(w => char.ToUpper(w[0]) + w.Substring(1)).Aggregate((a, b) => a + " " + b)); }
  static void T17() { Console.WriteLine("abc".GetHashCode() == "abc".GetHashCode()); Console.WriteLine("Hello".ToLowerInvariant() + "Hello".ToUpperInvariant()); }
  static void T18() { Console.WriteLine("a\tb".Contains('\t') + " " + "line1\nline2".Split('\n').Length + " " + "x".Length + " " + "".Length + " " + ("abc".Substring(3).Length)); }
  static void T19() { try { "abc".Substring(5); } catch (ArgumentOutOfRangeException) { Console.WriteLine("aoor"); } try { var c = "abc"[10]; } catch (IndexOutOfRangeException) { Console.WriteLine("ioor"); } }
  static void T20() { Console.WriteLine("The quick brown".IndexOf("quick", StringComparison.Ordinal) + " " + "abc".IndexOf("B", StringComparison.OrdinalIgnoreCase) + " " + "abcabc".LastIndexOf("bc") + " " + "abc".LastIndexOf('z') + " " + "abcabc".IndexOf("c", 3)); }
  static void T21() { Console.WriteLine("abc".Contains("") + " " + "".Contains("") + " " + "abc".IndexOf("") + " " + "abc".StartsWith("") + " " + "a".Split(',')[0]); }
  static void T22() { Console.WriteLine("%d %s {0}" + " " + "100%" + " " + "a\\b" + " " + "q\"q" + " " + string.Format("{0}%", 5)); }
  static void T23() { Console.WriteLine("a" + null + "b" + " " + (string)null + "|" + string.Concat((string)null, "x") + "|" + string.Join(",", new string[] { "a", null, "b" })); }
  static void T24() { string s = "aXbXc"; Console.WriteLine(s.Split('X')[1] + " " + s.Split('X', 2)[1] + " " + string.Join("", s.Split('X').Reverse())); }
  static void T25() { Console.WriteLine("abc".AsSpan().Length + " " + "abc"[1..].Length + " " + "title case".ToUpper()[0] + " " + "Ab".ToCharArray()[1]); }
  static void T26() { Console.WriteLine("test".Equals((object)"test") + " " + ((object)"a" == (object)"a") + " " + "A".ToLower().Equals("a") + " " + string.Intern("q")); }
  static void T27() { Console.WriteLine("1,2,3".Split(',').Select(int.Parse).Sum() + " " + "a=1;b=2".Split(';').Select(kv => kv.Split('=')).ToDictionary(p => p[0], p => int.Parse(p[1]))["b"]); }
  static void T28() { Console.WriteLine("abc".TrimEnd('c') + " " + "a.b.c".Split('.').Last() + " " + "path/to/file.txt".Substring("path/to/file.txt".LastIndexOf('/') + 1) + " " + "ABC".ToLower().ToUpper()); }
  static void T29() { Console.WriteLine("Hello".Normalize() + " " + "é".Length + " " + "abc".EnumerateRunes().Count()); }
  static void T30() { Console.WriteLine(string.Create(3, 'x', (span, c) => { span.Fill(c); })); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); H.R("T23", T23); H.R("T24", T24); H.R("T25", T25); H.R("T26", T26); H.R("T27", T27); H.R("T28", T28); H.R("T29", T29); H.R("T30", T30); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
