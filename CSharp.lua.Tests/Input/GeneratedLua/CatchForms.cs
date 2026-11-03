using System;
using System.Collections.Generic;

class Box {
  public int Size;

  public void Grow() {
    Size++;
  }
}

class Program {
  static void Show(string name, Action a) {
    try {
      a();
      Console.WriteLine(name + ": no error");
    } catch (NullReferenceException) {
      Console.WriteLine(name + ": null");
    } catch (IndexOutOfRangeException) {
      Console.WriteLine(name + ": index");
    } catch (DivideByZeroException) {
      Console.WriteLine(name + ": divide");
    } catch (ArgumentOutOfRangeException) {
      Console.WriteLine(name + ": argument");
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  public static void Main() {
    Box box = null;
    string text = null;
    Action action = null;
    var array = new int[3];
    var list = new List<int> { 1 };
    int zero = 0;
    Show("Field", () => Console.WriteLine(box.Size));
    Show("Method", () => box.Grow());
    Show("Length", () => Console.WriteLine(text.Length));
    Show("Delegate", () => action());
    Show("ArrayRead", () => Console.WriteLine(array[5]));
    Show("ArrayWrite", () => array[3] = 1);
    Show("ListRead", () => Console.WriteLine(list[5]));
    Show("Divide", () => Console.WriteLine(1 / zero));
    Show("Modulo", () => Console.WriteLine(1 % zero));
    try {
      throw new InvalidOperationException("ioe");
    } catch (Exception e) when (e is InvalidOperationException ioe && ioe.Message.Length > 1) {
      Console.WriteLine("filter: " + ioe.Message);
    }
  }
}
