using System;
class Program {
  static void T1() { char c = 'a'; c++; Console.WriteLine(c + " " + (int)c + " " + (char)(c + 1) + " " + (c + 1)); }
  static void T2() { Console.WriteLine(char.IsDigit('5') + " " + char.IsLetter('x') + " " + char.IsUpper('X') + " " + char.IsLower('X') + " " + char.IsWhiteSpace(' ') + " " + char.IsLetterOrDigit('_') + " " + char.IsPunctuation('!')); }
  static void T3() { Console.WriteLine(char.ToUpper('a') + " " + char.ToLower('Q') + " " + char.ToUpperInvariant('z') + " " + char.GetNumericValue('7') + " " + ('7' - '0')); }
  static void T4() { char c = 'x'; string s = "" + c + c; Console.WriteLine(s + " " + c.ToString() + " " + (c == 'x') + " " + (c < 'y')); }
  static void T5() { Console.WriteLine((int)'A' + " " + (char)97 + " " + (char)('a' + 2) + " " + ('a' + 'b') + " " + (char)('a' + 'b' - 'a')); }
  static void T6() { char c = '5'; int d = c - '0'; Console.WriteLine(d * 2); Console.WriteLine((char)('0' + 7)); }
  static void T7() { Console.WriteLine('a'.CompareTo('b') + " " + 'a'.Equals('a') + " " + char.Parse("z") + " " + ((int)'\n') + " " + ((int)'\t') + " " + ((int)'\\') + " " + ((int)'\'')); }
  static void T8() { char[] cs = { 'a', 'b' }; Console.WriteLine(new string(cs) + " " + cs.Length + " " + new string('x', 3)); }
  static void T9() { char c = (char)65; switch (c) { case 'A': Console.WriteLine("is A"); break; } }
  static void T10() { string s = "hello"; char[] arr = s.ToCharArray(); Array.Reverse(arr); Console.WriteLine(new string(arr)); }
  static void T11() { Console.WriteLine(char.IsAscii('a') + " " + char.IsAsciiDigit('1') + " " + char.IsAsciiLetter('x') + " " + char.IsSymbol('+') + " " + char.IsControl('\n')); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
