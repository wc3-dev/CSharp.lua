using System;
class Program { static ref int First(int[] a) => ref a[0]; static void Main() { var arr = new[] { 1, 2 }; ref int r = ref First(arr); r = 9; Console.WriteLine(arr[0]); } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
