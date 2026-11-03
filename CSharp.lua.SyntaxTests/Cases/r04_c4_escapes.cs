using System;
class Program {
  const string Esc = "\x1b[0m";
  static void A() { Console.WriteLine(Esc.Length + " " + (int)Esc[0]); }
  static void B() { string a = "a\0" + "1"; Console.WriteLine(a.Length + " " + (int)a[1]); }
  static void C() { string b = "\e" + "y"; Console.WriteLine(b.Length + " " + (int)b[0]); }
  static void D() { string c = "tab\there\\ \"q\" \x41"; Console.WriteLine(c.Length + " " + c); }
  static void E() { string raw = """
      raw "quoted" \n text
      """; Console.WriteLine(raw); }
  static void F() { Console.WriteLine($"x{1}%d{2}%s"); }
  static void G() { var u8 = "hi"u8; Console.WriteLine(u8.Length); }
  static void I() { string s = "caf\u00e9"; Console.WriteLine(s.Length + " " + s.ToUpper()); }
  static void J() { char c = '\u00e9'; Console.WriteLine((int)c + " " + c.ToString().Length); }
  static void K() { string s = "\u00e9a"; Console.WriteLine(s[1] + " " + s.IndexOf('a') + " " + s.Substring(1)); }
  static void Main() { H.R("A", A); H.R("B", B); H.R("C", C); H.R("D", D); H.R("E", E); H.R("F", F); H.R("G", G); H.R("I", I); H.R("J", J); H.R("K", K); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
