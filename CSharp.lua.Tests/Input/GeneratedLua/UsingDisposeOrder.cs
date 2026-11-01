using System;

class Res : IDisposable {
  private readonly string name;
  private readonly bool fails;

  public Res(string name, bool fails = false) {
    this.name = name;
    this.fails = fails;
  }

  public void Dispose() {
    Console.WriteLine("dispose " + name);
    if (fails) {
      throw new InvalidOperationException("from " + name);
    }
  }
}

class Program {
  static void OneDeclaration() {
    using Res a = new Res("a"), b = new Res("b");
    Console.WriteLine("one declaration");
  }

  static void OneStatement() {
    using (Res a = new Res("a"), b = new Res("b")) {
      Console.WriteLine("one statement");
    }
  }

  static void NextToEachOther() {
    using var a = new Res("a");
    using var b = new Res("b");
    Console.WriteLine("next to each other");
  }

  static void Three() {
    using Res a = new Res("a"), b = new Res("b"), c = new Res("c");
    Console.WriteLine("three");
  }

  static int Returns() {
    using Res a = new Res("a"), b = new Res("b");
    return 7;
  }

  static void NoFirst() {
    using Res a = null, b = new Res("b");
    Console.WriteLine("no first");
  }

  static void NoLast() {
    using Res a = new Res("a"), b = null;
    Console.WriteLine("no last");
  }

  static void BodyFails() {
    using Res a = new Res("a"), b = new Res("b");
    throw new InvalidOperationException("from body");
  }

  static void LastDisposeFails() {
    using Res a = new Res("a"), b = new Res("b", true);
    Console.WriteLine("last fails");
  }

  static void FirstDisposeFails() {
    using Res a = new Res("a", true), b = new Res("b");
    Console.WriteLine("first fails");
  }

  static void BothDisposesFail() {
    using Res a = new Res("a", true), b = new Res("b", true);
    Console.WriteLine("both fail");
  }

  static void BodyAndDisposeFail() {
    using Res a = new Res("a"), b = new Res("b", true);
    throw new InvalidOperationException("from body");
  }

  static void Catches(Action action) {
    try {
      action();
      Console.WriteLine("no error");
    } catch (InvalidOperationException e) {
      Console.WriteLine("caught " + e.Message);
    }
  }

  public static void Main() {
    OneDeclaration();
    OneStatement();
    NextToEachOther();
    Three();
    Console.WriteLine(Returns());
    NoFirst();
    NoLast();
    Catches(BodyFails);
    Catches(LastDisposeFails);
    Catches(FirstDisposeFails);
    Catches(BothDisposesFail);
    Catches(BodyAndDisposeFail);
  }
}
