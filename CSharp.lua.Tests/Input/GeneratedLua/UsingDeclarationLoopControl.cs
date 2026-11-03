using System;
using System.Collections.Generic;

class Res : IDisposable {
  private readonly string name_;

  public Res(string name) {
    name_ = name;
  }

  public void Dispose() {
    Program.Log.Add("-" + name_);
  }
}

class Program {
  public static readonly List<string> Log = new();

  static void For() {
    for (int i = 0; i < 4; i++) {
      using var r = new Res("f" + i);
      if (i == 1) continue;
      if (i == 3) break;
      Log.Add("f" + i);
    }
  }

  static void While() {
    int i = 0;
    while (true) {
      i++;
      using var r = new Res("w" + i);
      if (i == 2) continue;
      if (i == 3) break;
      Log.Add("w" + i);
    }
  }

  static void Foreach() {
    foreach (var s in new[] { "a", "b", "c" }) {
      using var r = new Res(s);
      if (s == "a") continue;
      if (s == "c") break;
      Log.Add(s);
    }
  }

  static void Do() {
    int i = 0;
    do {
      i++;
      using var r = new Res("d" + i);
      if (i == 1) continue;
      if (i == 2) break;
      Log.Add("d" + i);
    } while (i < 5);
  }

  static void TwoDeclarations() {
    for (int i = 0; i < 3; i++) {
      using var a = new Res("a" + i);
      Log.Add("x" + i);
      using var b = new Res("b" + i);
      if (i == 0) continue;
      if (i == 1) break;
      Log.Add("y" + i);
    }
  }

  static void InnerBlock() {
    for (int i = 0; i < 3; i++) {
      Log.Add("i" + i);
      if (i >= 0) {
        using var r = new Res("i" + i);
        if (i == 0) continue;
        if (i == 1) break;
      }
    }
  }

  static void NestedLoop() {
    for (int i = 0; i < 2; i++) {
      using var r = new Res("o" + i);
      for (int j = 0; j < 3; j++) {
        using var s = new Res("n" + i + j);
        if (j == 0) continue;
        if (j == 1) break;
      }
      if (i == 0) continue;
      Log.Add("o" + i);
    }
  }

  static void SwitchInLoop() {
    for (int i = 0; i < 3; i++) {
      using var r = new Res("s" + i);
      switch (i) {
        case 0:
          continue;
        case 1:
          break;
        default:
          Log.Add("d");
          break;
      }
      Log.Add("s" + i);
    }
  }

  static int Returns() {
    for (int i = 0; i < 5; i++) {
      using var r = new Res("r" + i);
      if (i == 0) continue;
      if (i == 2) return i;
    }
    return -1;
  }

  static void InTry() {
    for (int i = 0; i < 3; i++) {
      try {
        using var r = new Res("t" + i);
        if (i == 0) continue;
        if (i == 1) break;
      } finally {
        Log.Add("finally" + i);
      }
    }
  }

  static void TryAfter() {
    for (int i = 0; i < 3; i++) {
      using var r = new Res("u" + i);
      try {
        if (i == 0) continue;
        if (i == 1) break;
      } finally {
        Log.Add("finally" + i);
      }
    }
  }

  static void TryInTry() {
    for (int i = 0; i < 3; i++) {
      try {
        try {
          if (i == 0) continue;
          if (i == 1) break;
        } finally {
          Log.Add("inner" + i);
        }
      } finally {
        Log.Add("outer" + i);
      }
    }
  }

  static void Show(string name) {
    Console.WriteLine(name + ": " + string.Join(" ", Log));
    Log.Clear();
  }

  public static void Main() {
    For();
    Show("for");
    While();
    Show("while");
    Foreach();
    Show("foreach");
    Do();
    Show("do");
    TwoDeclarations();
    Show("two");
    InnerBlock();
    Show("inner");
    NestedLoop();
    Show("nested");
    SwitchInLoop();
    Show("switch");
    Log.Add("returned " + Returns());
    Show("return");
    InTry();
    Show("in try");
    TryAfter();
    Show("try after");
    TryInTry();
    Show("try in try");
  }
}
