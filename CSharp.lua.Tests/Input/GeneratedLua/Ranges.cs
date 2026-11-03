using System;
using System.Collections.Generic;

class Program {
  static void Show(string name, Func<string> f) {
    try {
      Console.WriteLine(name + ": " + f());
    } catch (Exception e) {
      Console.WriteLine(name + ": " + e.GetType().Name);
    }
  }

  public static void Main() {
    var array = new[] { 1, 2, 3, 4, 5 };
    var list = new List<int> { 1, 2, 3 };
    var text = "hello";
    Show("ArrayLast", () => array[^1].ToString());
    Show("ArraySlice", () => string.Join(",", array[1..3]));
    Show("ArrayFrom", () => string.Join(",", array[2..]));
    Show("ArrayTo", () => string.Join(",", array[..2]));
    Show("ArrayFromEnd", () => string.Join(",", array[^2..]));
    Show("ArrayAll", () => string.Join(",", array[..]));
    Show("StringSlice", () => text[1..3]);
    Show("StringFrom", () => text[1..]);
    Show("StringFromEnd", () => text[^3..^1]);
    Show("StringLastChar", () => text[^1].ToString());
    Show("ListLast", () => list[^1].ToString());
    Show("ListSet", () => { list[^1] = 9; return list[2].ToString(); });
    Index i = ^2;
    Range r = 1..^1;
    Show("IndexVariable", () => array[i] + " " + i.Value + " " + i.IsFromEnd);
    Show("RangeVariable", () => string.Join(",", array[r]) + " " + text[r]);
    Show("RangeParts", () => r.Start.Value + " " + r.End.Value + " " + r.End.IsFromEnd);
    Show("Offset", () => i.GetOffset(10) + " " + r.GetOffsetAndLength(10).Length);
    Show("SliceOutOfRange", () => string.Join(",", array[3..9]));
    Show("StaticRanges", () => string.Join(",", array[Range.StartAt(3)]) + " " + string.Join(",", array[Range.EndAt(1)]) + " " + array[Range.All].Length);
    Show("RangeText", () => r.ToString() + " " + (1..^1).Equals(r) + " " + (1..2).Equals(r));
    Show("IndexZero", () => text[^0..].Length + " " + Index.End.IsFromEnd);
  }
}
