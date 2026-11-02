using System;
class Program {
  static void T1() { int a = 0b1100, b = 0b1010; Console.WriteLine((a & b) + " " + (a | b) + " " + (a ^ b) + " " + (~a)); }
  static void T2() { Console.WriteLine((1 << 4) + " " + (256 >> 4) + " " + (-16 >> 2) + " " + (-1 >> 31) + " " + (1 << 31) + " " + (1 << 32) + " " + (1 << 33)); }
  static void T3() { uint u = 0xFFFFFFFF; Console.WriteLine(u + " " + (u >> 28) + " " + (u << 4) + " " + (~u) + " " + (u & 0xFF)); }
  static void T4() { int x = unchecked((int)0x80000000); Console.WriteLine(x + " " + (x >> 31) + " " + ((uint)x >> 31)); }
  static void T5() { int flags = 0; flags |= 1 << 3; flags |= 1 << 5; Console.WriteLine(flags + " " + ((flags & (1 << 3)) != 0) + " " + ((flags & (1 << 4)) != 0)); flags &= ~(1 << 3); Console.WriteLine(flags); }
  static void T6() { byte b = 0xF0; Console.WriteLine((b >> 4) + " " + (b << 4) + " " + (byte)(b << 4) + " " + (byte)~b); }
  static void T7() { Console.WriteLine(0xFF & -1); Console.WriteLine(-1 & 0x7FFFFFFF); Console.WriteLine(-8 >> 1); Console.WriteLine(-8 >>> 1); }
  static void T8() { int c = 0, v = 0b10110110; while (v != 0) { c += v & 1; v >>= 1; } Console.WriteLine(c); }
  static void T9() { Console.WriteLine(System.Numerics.BitOperations.PopCount(255u) + " " + System.Numerics.BitOperations.LeadingZeroCount(1u) + " " + System.Numerics.BitOperations.TrailingZeroCount(8)); }
  static void T10() { long l = 1L << 40; Console.WriteLine(l + " " + (l >> 38) + " " + (l | 1)); }
  static void T11() { ulong u = 0xFFFFFFFFFFFFFFFF; Console.WriteLine(u + " " + (u >> 60) + " " + (u / 2)); }
  static void T12() { short s = unchecked((short)0x8000); Console.WriteLine(s + " " + (s >> 1) + " " + (ushort)s); }
  static void T13() { int a = 5; a <<= 2; a >>= 1; a ^= 3; a |= 8; a &= 0xE; Console.WriteLine(a); }
  static void T14() { Console.WriteLine(int.RotateLeft(1, 33) + " " + uint.RotateRight(1u, 1)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
