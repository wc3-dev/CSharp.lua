using System;
class Program {
  static void T1() { Console.WriteLine(@"C:\path\n" + @"a""b"); }
  static void T2() { var s = """
      line1
        line2
      """; Console.WriteLine(s); }
  static void T3() { var s = """a "quoted" word"""; Console.WriteLine(s); }
  static void T4() { int x = 5; var s = $"""val={x} (not)"""; Console.WriteLine(s); }
  static void T5() { var s = $$"""{ "k": {{1 + 1}} }"""; Console.WriteLine(s); }
  static void T6() { ReadOnlySpan<byte> b = "abc"u8; Console.WriteLine(b.Length + " " + b[0]); }
  static void T7() { var s = @"multi
line"; Console.WriteLine(s.Length); }
  static void T8() { var s = "\u0041\x42\a\b\f\v\0".Length; Console.WriteLine(s); }
  static void T9() { var s = "tab\tnl\\n"; Console.WriteLine(s.Length + s); }
  static void T10() { var s = "q\"q'a"; var c = '\''; var d = '"'; Console.WriteLine(s + c + d); }
  static void T11() { Console.WriteLine("\0".Length + " " + ((int)"\0"[0]) + " " + "a\0b".Length + " " + "\r\n".Length); }
  static void T12() { string s = "ünï"; Console.WriteLine(s); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
