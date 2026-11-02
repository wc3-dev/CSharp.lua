using System;
using System.Threading;
class Program {
  static object gate = new object();
  static void T1() { lock (gate) { Console.WriteLine("locked"); } }
  static void T2() { try { int x = int.MaxValue; int y = checked(x + 1); Console.WriteLine(y); } catch (OverflowException) { Console.WriteLine("overflow"); } }
  static void T3() { int x = int.MaxValue; int y = unchecked(x + 1); Console.WriteLine(y); }
  static void T4() { checked { try { byte b = 255; b++; Console.WriteLine(b); } catch (OverflowException) { Console.WriteLine("ovf2"); } } }
  static void T5() { try { long big = 3000000000L; int z = checked((int)big); Console.WriteLine(z); } catch (OverflowException) { Console.WriteLine("ovf3"); } }
  static unsafe void T6() { int* p = stackalloc int[2]; p[0] = 4; p[1] = 5; Console.WriteLine(p[0] + p[1]); }
  static void T7() { Span<byte> sp = stackalloc byte[4]; sp[0] = 255; Console.WriteLine(sp[0] + sp.Length); }
  static void T8() { var l = new Lock(); lock (l) { Console.WriteLine("lock13"); } }
  static void T9() { Monitor.Enter(gate); Monitor.Exit(gate); Console.WriteLine("monitor"); }
  static void T10() { int x = 5; int y = unchecked((int)4294967295L); Console.WriteLine(y); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
