using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Ranges and indexes from the end. An element read with a range (array[1..3], text[1..], list[1..]) was written as the
  /// receiver's get with a System.Range, which the core did not have; it is now one call of the core that gives a string's
  /// text, a new array of the elements, or the receiver's Slice. ^n on a string or a List was written as get(-n), outside
  /// the string or list; it is now its count less n, as it was on an array. A list pattern's slice ([var first, .. var rest])
  /// called a Slice an array or a string does not have, and now takes the same slice. The core has System.Range, with its
  /// members, and List.Slice; Index.Value and ToString of an index from the end were wrong, and Index(n, true) failed. The
  /// expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class RangesTests {
    [TestMethod]
    public void TestRangesAndIndexes() {
      var output = GameScript.Run("Ranges", "Program");
      Assert.AreEqual(
        "ArrayLast: 5\n"
        + "ArraySlice: 2,3\n"
        + "ArrayFrom: 3,4,5\n"
        + "ArrayTo: 1,2\n"
        + "ArrayFromEnd: 4,5\n"
        + "ArrayAll: 1,2,3,4,5\n"
        + "StringSlice: el\n"
        + "StringFrom: ello\n"
        + "StringFromEnd: ll\n"
        + "StringLastChar: o\n"
        + "ListLast: 3\n"
        + "ListSet: 9\n"
        + "IndexVariable: 4 2 True\n"
        + "RangeVariable: 2,3,4 ell\n"
        + "RangeParts: 1 1 True\n"
        + "Offset: 8 8\n"
        + "SliceOutOfRange: ArgumentOutOfRangeException\n"
        + "StaticRanges: 4,5 1 5\n"
        + "RangeText: 1..^1 True False\n"
        + "ListPatterns: 1:2,3 empty a|bc empty 2,3 short\n"
        + "IndexZero: 0 True\n",
        output);
    }

    // list[1..] in .NET 8 calls List.Slice, which this .NET 6 project cannot compile; the core's is checked on its own.
    [TestMethod]
    public void TestListSlice() {
      var output = CoreSystemLua.Run(@"
local list = System.List(System.Int32)()
list:Add(1)
list:Add(2)
list:Add(3)
local slice = list:Slice(1, 2)
print(slice:getCount(), slice:get(0), slice:get(1), System.Range.slice(list, System.Range(nil, -1)):getCount())");
      Assert.AreEqual("2\t2\t3\t2\n", output);
    }
  }
}
