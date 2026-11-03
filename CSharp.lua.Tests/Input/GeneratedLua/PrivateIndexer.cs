using System;

class Grid {
  private readonly int[] cells = new int[4];

  private int this[int i] {
    get => cells[i];
    set => cells[i] = value;
  }

  public void Fill(Grid other) {
    this[0] = 1;
    this[1] = this[0] + 1;
    other[2] = 5;
    this[3] += 2;
    other[3]++;
  }

  public int Sum(Grid other) => this[0] + this[1] + other[2] + this[3] + other[3];
}

class Bag {
  private readonly string[] names = new string[2];

  public string this[int i] {
    get => names[i];
    private set => names[i] = value;
  }

  public void Put() {
    this[0] = "a";
    this[1] = "b";
  }
}

class Program {
  public static void Main() {
    var grid = new Grid();
    var other = new Grid();
    grid.Fill(other);
    Console.WriteLine(grid.Sum(other));
    var bag = new Bag();
    bag.Put();
    Console.WriteLine(bag[0] + bag[1]);
  }
}
