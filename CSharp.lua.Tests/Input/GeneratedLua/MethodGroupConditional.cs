using System;

static class Reactions {
  public static void OnMoved() {
    Console.WriteLine("moved");
  }
}

class Other {
  public void Run() {
    Console.WriteLine("run");
  }

  public static int Twice(int x) => x * 2;
}

class Program {
  static void Hello() {
    Console.WriteLine("hello");
  }

  public static void Main() {
    bool yes = true;
    Action a = yes ? Reactions.OnMoved : null;
    a?.Invoke();
    var o = new Other();
    Action b = yes ? o.Run : null;
    b?.Invoke();
    Action c = yes ? Hello : null;
    c?.Invoke();
    Func<int, int> f = yes ? Other.Twice : null;
    Console.WriteLine(f(4));
    Action d = !yes ? Reactions.OnMoved : null;
    Console.WriteLine(d == null);
    Func<int, int> g = yes ? x => x + 1 : null;
    Console.WriteLine(g(1));
    Action h = yes ? delegate { Console.WriteLine("anonymous"); } : null;
    h();
  }
}
