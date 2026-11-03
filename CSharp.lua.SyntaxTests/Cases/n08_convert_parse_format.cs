using System;
using System.Globalization;
class Program {
  static void T1() { Console.WriteLine(Convert.ToInt32("42") + Convert.ToInt32(3.7) + Convert.ToInt32(2.5) + Convert.ToInt32(true) + " " + Convert.ToInt32('A')); }
  static void T2() { Console.WriteLine(Convert.ToString(255, 2) + " " + Convert.ToString(255, 16) + " " + Convert.ToString(255, 8) + " " + Convert.ToInt32("ff", 16) + " " + Convert.ToInt32("101", 2)); }
  static void T3() { Console.WriteLine(Convert.ToDouble("1.5") + " " + Convert.ToSingle("2.5") + " " + Convert.ToBoolean("true") + " " + Convert.ToString(12) + " " + Convert.ToChar(66) + " " + Convert.ToByte(200)); }
  static void T4() { Console.WriteLine(Convert.ToBase64String(new byte[] { 1, 2, 3 }) + " " + Convert.FromBase64String("AQID").Length + " " + Convert.ToHexString(new byte[] { 255, 1 })); }
  static void T5() { Console.WriteLine(int.TryParse("  7 ", out var a) + " " + a + " " + int.TryParse("7.5", out var b) + " " + int.TryParse("", out var c) + " " + int.TryParse((string)null, out var d) + " " + int.TryParse("+5", out var e) + e + " " + int.TryParse("2147483648", out var f) + f); }
  static void T6() { Console.WriteLine(255.ToString("X") + " " + 255.ToString("x4") + " " + 5.ToString("D3") + " " + 1234567.ToString("N0") + " " + 0.5.ToString("P0") + " " + 12.ToString("00.0") + " " + (-5).ToString("D3") + " " + 5.ToString("C")); }
  static void T7() { Console.WriteLine(1.5f.ToString() + " " + 100.0.ToString() + " " + 1e-5.ToString() + " " + 123456789.0.ToString() + " " + 0.1f.ToString() + " " + 1e20f.ToString() + " " + (1/3.0).ToString()); }
  static void T8() { Console.WriteLine(double.Parse("1,5", CultureInfo.InvariantCulture) + " " + double.Parse("1.5", CultureInfo.InvariantCulture) + " " + 1.5.ToString(CultureInfo.InvariantCulture) + " " + int.Parse("12", NumberStyles.Integer)); }
  static void T9() { Console.WriteLine(bool.Parse("True") + " " + bool.TryParse("x", out var b) + " " + true.ToString() + " " + false + " " + (true ? 1 : 0)); }
  static void T10() { Console.WriteLine(long.TryParse("123456789012", out var l) + " " + l + " " + uint.Parse("4000000000") + " " + byte.Parse("255") + " " + sbyte.Parse("-128") + " " + short.Parse("-5")); }
  static void T11() { try { Convert.ToInt32("abc"); } catch (FormatException) { Console.WriteLine("fmt"); } try { int.Parse("999999999999"); } catch (OverflowException) { Console.WriteLine("ovf"); } try { Convert.ToByte(300); } catch (OverflowException) { Console.WriteLine("ovf2"); } }
  static void T12() { Console.WriteLine(string.Format("{0:F2} {1:E} {2:G} {3:0.##} {4:#,##0.00}", 3.14159, 12345.678, 0.0001, 2.5, 1234567.891)); }
  static void T13() { Console.WriteLine(((int)'a').ToString("X2") + " " + 10.ToString("B8") + " " + 3.7.ToString("0") + " " + 2.5.ToString("0") + " " + 0.05.ToString("0.0") + " " + 1e6.ToString("F0")); }
  static void T14() { Console.WriteLine(Int32.MaxValue.ToString() + " " + Int32.MinValue.ToString() + " " + long.MinValue.ToString() + " " + ulong.MaxValue.ToString() + " " + (-0.0).ToString() + " " + 1.0E+25.ToString()); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
