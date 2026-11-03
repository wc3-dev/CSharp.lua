using System;

class Counter {
  public int Value;

  public static Counter operator ++(Counter c) => new Counter { Value = c.Value + 1 };
  public static Counter operator --(Counter c) => new Counter { Value = c.Value - 1 };
  public static bool operator true(Counter c) => c.Value != 0;
  public static bool operator false(Counter c) => c.Value == 0;
  public static Counter operator &(Counter a, Counter b) => new Counter { Value = a.Value & b.Value };
  public static Counter operator |(Counter a, Counter b) => new Counter { Value = a.Value | b.Value };
}

struct Vec {
  public int X;

  public static Vec operator ++(Vec v) => new Vec { X = v.X + 10 };
}

class Holder {
  public Counter Field = new Counter();
  public Counter Prop { get; set; } = new Counter();
}

record Base(int A);
record Derived(int A, string B) : Base(A);
record Third(int A, string B, bool C) : Derived(A, B);

class Program {
  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  public static void Main() {
    Show("Increment", () => {
      var c = new Counter { Value = 1 };
      c++;
      ++c;
      var d = c++;
      var e = ++c;
      return c.Value + " " + d.Value + " " + e.Value;
    });
    Show("Decrement", () => {
      var c = new Counter { Value = 5 };
      c--;
      return (--c).Value.ToString();
    });
    Show("StructIncrement", () => {
      var v = new Vec { X = 1 };
      v++;
      return v.X.ToString();
    });
    Show("Members", () => {
      var h = new Holder();
      h.Field++;
      h.Prop++;
      ++h.Prop;
      var items = new[] { new Counter() };
      items[0]++;
      return h.Field.Value + " " + h.Prop.Value + " " + items[0].Value;
    });
    Show("DerivedRecord", () => (new Derived(1, "q") == new Derived(1, "q")) + " " + (new Derived(1, "q") != new Derived(1, "r")) + " " + (new Third(1, "q", true) == new Third(1, "q", true)));
    Show("DerivedText", () => new Third(1, "q", true) + " " + new Derived(1, "q").Equals(new Base(1)) + " " + new Derived(1, "q").Equals(new Derived(1, "q")));
    Show("TrueFalse", () => {
      var zero = new Counter();
      var one = new Counter { Value = 1 };
      var two = new Counter { Value = 2 };
      var text = (zero ? "t" : "f") + (one ? "t" : "f") + ((one && zero) ? "t" : "f") + ((zero || one) ? "t" : "f") + ((one && two) ? "t" : "f");
      if (one) {
        text += " if";
      }
      var count = new Counter { Value = 3 };
      while (count) {
        count--;
      }
      var down = new Counter { Value = 2 };
      do {
        down--;
      } while (down);
      var steps = 0;
      for (var left = new Counter { Value = 2 }; left; left--) {
        steps++;
      }
      text += " " + down.Value + steps;
      var calls = 0;
      Counter Called() {
        calls++;
        return one;
      }
      var shortAnd = zero && Called();
      var shortOr = one || Called();
      return text + " " + count.Value + " " + calls + " " + shortAnd.Value + " " + shortOr.Value + " " + (one && two).Value + " " + (zero || two).Value;
    });
    Show("AnonymousWith", () => {
      var a = new { X = 1, Y = "a" };
      var b = a with { X = 2 };
      return a.X + " " + b.X + " " + b.Y;
    });
  }
}
