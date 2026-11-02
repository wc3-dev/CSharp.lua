using System;
using System.Threading.Tasks;
class Program {
  static async Task<int> Add(int a) { await Task.Delay(1); return a + 1; }
  static void T1() { var t = Add(1); Console.WriteLine(t.Result); }
  static void T2() { Console.WriteLine("dynamic-skipped"); }
  static void T3() { var d = DateTime.Now; Console.WriteLine(d.Year > 2000); }
  static void T4() { var g = Guid.NewGuid(); Console.WriteLine(g != Guid.Empty); }
  static void T5() { var sw = System.Diagnostics.Stopwatch.StartNew(); Console.WriteLine(sw.ElapsedMilliseconds >= 0); }
  static void T6() { Console.WriteLine(typeof(Program).GetMethods().Length > 0); }
  static void T7() { Console.WriteLine(Environment.NewLine.Length > 0); }
  static void T8() { var t = new System.Threading.Thread(() => Console.WriteLine("thread")); t.Start(); t.Join(); }
  static void T9() { Console.WriteLine(GC.GetTotalMemory(false) > 0); }
  static void T10() { var r = new Random(1); Console.WriteLine(r.Next(5, 10) >= 5); }
  static void T11() { Console.WriteLine(TimeSpan.FromSeconds(90).TotalMinutes); }
  static void T12() { Console.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.00}", 1.5)); }
  static void T13() { var o = new object(); var wr = new WeakReference(o); Console.WriteLine(wr.IsAlive); }
  static void T14() { Console.WriteLine(Environment.TickCount != 0 || true); }
  static void T15() { Func<int> f = () => 3; Console.WriteLine(f.Method != null); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); H.R("T13", T13); H.R("T14", T14); H.R("T15", T15); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
