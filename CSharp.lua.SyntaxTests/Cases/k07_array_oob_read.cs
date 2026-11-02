using System;
class Program { static void Main() { var a = new int[3]; try { var x = a[5]; Console.WriteLine("no error " + x); } catch (IndexOutOfRangeException) { Console.WriteLine("ioor"); } catch (Exception) { Console.WriteLine("other"); } } }

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
