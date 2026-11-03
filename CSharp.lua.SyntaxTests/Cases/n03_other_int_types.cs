using System;
class Program {
  static void T1() { byte b = 250; b += 3; Console.WriteLine(b); b += 10; Console.WriteLine(b); }
  static void T2() { sbyte s = 120; s += 10; Console.WriteLine(s); }
  static void T3() { short s = 32767; s++; Console.WriteLine(s); ushort u = 0; u--; Console.WriteLine(u); }
  static void T4() { uint u = 0; u--; Console.WriteLine(u); uint v = 4000000000; v += 500000000; Console.WriteLine(v); }
  static void T5() { uint a = 3, b = 5; Console.WriteLine((a - b) + " " + (a * b) + " " + (b / a) + " " + (b % a)); }
  static void T6() { long l = long.MaxValue; l++; Console.WriteLine(l); Console.WriteLine(long.MinValue + " " + long.MaxValue); }
  static void T7() { long a = 3000000000; long b = 4; Console.WriteLine(a * b + " " + a / b + " " + a % 7); }
  static void T8() { ulong u = ulong.MaxValue; Console.WriteLine(u + " " + (u + 1) + " " + ulong.MaxValue / 3); }
  static void T9() { Console.WriteLine(byte.MaxValue + " " + sbyte.MinValue + " " + short.MaxValue + " " + ushort.MaxValue + " " + uint.MaxValue + " " + char.MaxValue.GetHashCode()); }
  static void T10() { int i = 300; Console.WriteLine((byte)i + " " + (sbyte)i + " " + unchecked((short)70000) + " " + unchecked((ushort)70000) + " " + (char)65); }
  static void T11() { long l = 5000000000; Console.WriteLine((int)l + " " + (uint)l + " " + (short)l); }
  static void T12() { uint u = 4000000000; Console.WriteLine((int)u + " " + (long)u + " " + (float)u + " " + (double)u); }
  static void T13() { int i = -1; Console.WriteLine((uint)i + " " + (ulong)i + " " + (long)i); }
  static void T14() { Console.WriteLine((long)3.99 + " " + (int)-3.99 + " " + (int)3.5f + " " + (byte)2.9 + " " + unchecked((int)double.MaxValue)); }
  static void T15() { Console.WriteLine(long.Parse("9000000000") + 1 + " " + ulong.Parse("18000000000000000000")); }
  static void T16() { long a = int.MaxValue; a *= 2; Console.WriteLine(a); int b = (int)(a % 100); Console.WriteLine(b); }
  static void T17() { uint x = 123456789; x = x * 1664525 + 1013904223; Console.WriteLine(x); x = x * 1664525 + 1013904223; Console.WriteLine(x); }
  static void T18() { ulong h = 14695981039346656037; h ^= 97; h *= 1099511628211; Console.WriteLine(h); }
  static void T19() { long a = 1L << 62; Console.WriteLine(a + a); Console.WriteLine(a * 4); }
  static void T20() { long x = 10; int y = 3; Console.WriteLine(x / y + " " + x % y + " " + (x > y)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
