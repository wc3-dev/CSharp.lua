using System;

abstract class Shape {
  internal abstract string Kind { get; }
  public abstract int Sides { get; }
  public virtual string Colour { get; } = "grey";
  public virtual int Size { get; set; } = 1;

  public string Describe() {
    Size = 5;
    return Kind + " " + this.Kind + " " + Sides + " " + this.Sides + " " + Colour + " " + Size;
  }
}

class Square : Shape {
  private int size;
  internal override string Kind => "square";
  public override int Sides => 4;
  public override string Colour => "red";
  public override int Size { get => size * 10; set => size = value; }
}

class Plain : Shape {
  internal override string Kind => "plain";
  public override int Sides { get; } = 0;
}

class Program {
  public static void Main() {
    Console.WriteLine(new Square().Describe());
    Console.WriteLine(new Plain().Describe());
  }
}
