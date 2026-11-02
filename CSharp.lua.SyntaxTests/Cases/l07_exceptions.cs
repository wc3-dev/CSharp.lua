using System;
using System.Collections.Generic;
class MyEx : Exception { public int Code; public MyEx(string m, int code) : base(m) { Code = code; } }
class Sub : MyEx { public Sub() : base("sub", 7) { } }
class Program {
  static void T1() { try { throw new Exception("boom"); } catch (Exception e) { Console.WriteLine(e.Message); } }
  static void T2() { try { Console.WriteLine("try"); } catch { Console.WriteLine("catch"); } finally { Console.WriteLine("finally"); } }
  static void T3() { try { throw new MyEx("m", 5); } catch (MyEx e) { Console.WriteLine(e.Message + e.Code); } }
  static void T4() { try { throw new Sub(); } catch (MyEx e) { Console.WriteLine(e.GetType().Name + e.Code); } }
  static void T5() { try { try { throw new Exception("in"); } finally { Console.WriteLine("inner finally"); } } catch (Exception e) { Console.WriteLine("outer " + e.Message); } }
  static void T6() { try { throw new Exception("x"); } catch (Exception e) when (e.Message == "y") { Console.WriteLine("y"); } catch (Exception e) when (e.Message == "x") { Console.WriteLine("filter x"); } }
  static void T7() { try { try { throw new Exception("a"); } catch (Exception e) { throw new InvalidOperationException("b", e); } } catch (Exception e) { Console.WriteLine(e.Message + " " + e.InnerException.Message + " " + e.GetType().Name); } }
  static void T8() { try { try { throw new Exception("rethrow"); } catch { throw; } } catch (Exception e) { Console.WriteLine(e.Message); } }
  static int F() { try { return 1; } finally { Console.WriteLine("f-finally"); } }
  static void T9() { Console.WriteLine(F()); }
  static int G() { try { throw new Exception(); } catch { return 2; } finally { Console.WriteLine("g-finally"); } }
  static void T10() { Console.WriteLine(G()); }
  static void T11() { string s = null; try { var l = s.Length; } catch (NullReferenceException) { Console.WriteLine("nre"); } }
  static void T12() { try { object o = "s"; var i = (int)o; } catch (InvalidCastException) { Console.WriteLine("ice"); } }
  static void T13() { int x = 5; var y = x > 3 ? 1 : throw new Exception(); string s = null; var z = s ?? throw new ArgumentNullException("s"); }
  static void T14() { try { string s = null; var z = s ?? throw new ArgumentNullException("s"); } catch (ArgumentNullException e) { Console.WriteLine("ane " + e.ParamName); } }
  static void T15() { try { throw new ArgumentException("bad", "p"); } catch (ArgumentException e) { Console.WriteLine(e.Message.StartsWith("bad") + " " + e.ParamName); } }
  static void T16() { for (int i = 0; i < 3; i++) { try { if (i == 1) continue; if (i == 2) break; Console.Write("b" + i); } finally { Console.Write("f" + i); } } Console.WriteLine(); }
  static void T17() { try { throw new NotImplementedException(); } catch (NotSupportedException) { Console.WriteLine("wrong"); } catch (NotImplementedException) { Console.WriteLine("nie"); } }
  static void T18() { try { throw new InvalidOperationException("ioe"); } catch (Exception e) when (e is InvalidOperationException ioe) { Console.WriteLine(ioe.Message); } }
  static void T19() { Exception e = new Exception("m"); Console.WriteLine(e.ToString().Contains("m") + " " + (e.InnerException == null) + " " + e.GetType().FullName); }
  static void T20() { try { throw new MyEx("custom", 1); } catch (Exception e) { Console.WriteLine((e is MyEx) + " " + ((MyEx)e).Code + " " + e.Message); } }
  static void T21() { try { var a = new int[2]; a[5] = 1; } catch (Exception e) { Console.WriteLine(e is IndexOutOfRangeException); } }
  static void T22() { try { throw new Exception(); } catch (Exception e) { Console.WriteLine(e.Message.Length > 0); } }
  static void T23() { try { int.Parse("x"); } catch (FormatException e) { Console.WriteLine("fe"); } try { Convert.ToInt32(null as string); Console.WriteLine("null ok"); } catch { } }
  static void T24() { try { try { throw new Exception("1"); } finally { throw new Exception("2"); } } catch (Exception e) { Console.WriteLine(e.Message); } }
  static void T25() { try { throw new OperationCanceledException(); } catch (OperationCanceledException) { Console.WriteLine("oce"); } try { throw new TimeoutException(); } catch (Exception e) { Console.WriteLine(e.GetType().Name); } }
  static void T26() { try { throw new KeyNotFoundException(); } catch (Exception e) { Console.WriteLine(e.GetType().Name); } try { throw new ArithmeticException(); } catch (Exception e) { Console.WriteLine(e.GetType().Name); } }
  static void T27() { try { int[] a = null; var l = a.Length; } catch (Exception e) { Console.WriteLine(e.GetType().Name); } }
  static void T28() { var e = new MyEx("d", 2) { Source = "src" }; Console.WriteLine(e.Source + e.Data.Count + (e.StackTrace == null)); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); H.R("T16", T16); H.R("T17", T17); H.R("T18", T18); H.R("T19", T19); H.R("T20", T20); H.R("T21", T21); H.R("T22", T22); H.R("T23", T23); H.R("T24", T24); H.R("T25", T25); H.R("T26", T26); H.R("T27", T27); H.R("T28", T28); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
