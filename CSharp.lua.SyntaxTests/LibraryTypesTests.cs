using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Types the Lua core lacked (Moonwell's MW0030 rejected them) and members of Span it lacked: each prints in Lua what
  /// it prints on .NET.
  /// </summary>
  [TestClass]
  public class LibraryTypesTests {
    [TestMethod]
    public void TestStringComparerInCollections() {
      SameAsNet.Prints(@"
var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { [""Footman""] = 1 };
counts[""FOOTMAN""] += 2;
Console.WriteLine(counts.Count + "" "" + counts[""footman""] + "" "" + counts.ContainsKey(""Knight"") + "" "" + counts.TryGetValue(""fOOTMAN"", out var n) + "" "" + n);
var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ""Arthas"", ""ARTHAS"", ""Jaina"" };
Console.WriteLine(names.Count + "" "" + names.Contains(""jaina"") + "" "" + names.Remove(""arthas"") + "" "" + names.Count);
var exact = new HashSet<string>(StringComparer.Ordinal) { ""a"", ""A"" };
Console.WriteLine(exact.Count + "" "" + exact.Contains(""a"") + "" "" + new[] { ""x"", ""X"", ""y"" }.Distinct(StringComparer.OrdinalIgnoreCase).Count());");
    }

    [TestMethod]
    public void TestStringComparerCompares() {
      SameAsNet.Prints(@"
var words = new List<string> { ""b"", ""B"", ""a"", ""C"" };
words.Sort(StringComparer.Ordinal);
Console.WriteLine(string.Join("","", words));
Console.WriteLine(string.Join("","", words.OrderBy(w => w, StringComparer.OrdinalIgnoreCase).ThenBy(w => w, StringComparer.Ordinal)));
Console.WriteLine(Math.Sign(StringComparer.Ordinal.Compare(""a"", ""B"")) + "" "" + Math.Sign(StringComparer.OrdinalIgnoreCase.Compare(""a"", ""B"")) + "" "" + StringComparer.OrdinalIgnoreCase.Compare(""x"", ""X"")
  + "" "" + Math.Sign(StringComparer.Ordinal.Compare(null, ""a"")) + "" "" + StringComparer.Ordinal.Compare(null, null));
Console.WriteLine(StringComparer.Ordinal.Equals(""a"", ""A"") + "" "" + StringComparer.OrdinalIgnoreCase.Equals(""a"", ""A"") + "" "" + StringComparer.OrdinalIgnoreCase.Equals(null, null) + "" "" + StringComparer.OrdinalIgnoreCase.Equals(""a"", null)
  + "" "" + (StringComparer.OrdinalIgnoreCase.GetHashCode(""abc"") == StringComparer.OrdinalIgnoreCase.GetHashCode(""ABC"")) + "" "" + (StringComparer.Ordinal.GetHashCode(""abc"") == StringComparer.Ordinal.GetHashCode(""abc"")));");
    }

    [TestMethod]
    public void TestBitArray() {
      SameAsNet.Prints(@"
var bits = new System.Collections.BitArray(10);
bits.Set(1, true);
bits[3] = true;
Console.WriteLine(bits.Length + "" "" + bits.Count + "" "" + bits.Get(1) + "" "" + bits[2] + "" "" + bits[3] + "" "" + bits.HasAnySet() + "" "" + bits.HasAllSet());
var other = new System.Collections.BitArray(new[] { true, true, false, false, false, false, false, false, false, false });
var and = new System.Collections.BitArray(bits).And(other);
Console.WriteLine(and[1] + "" "" + and[3] + "" "" + new System.Collections.BitArray(bits).Or(other)[0] + "" "" + new System.Collections.BitArray(bits).Xor(other)[1] + "" "" + new System.Collections.BitArray(bits).Not()[0]);
var all = new System.Collections.BitArray(3, true);
Console.WriteLine(all.HasAllSet() + "" "" + all[2]);
all.SetAll(false);
all.Length = 5;
Console.WriteLine(all.HasAnySet() + "" "" + all.Length);
var shifted = new System.Collections.BitArray(new[] { true, false, false, false }).LeftShift(2);
Console.WriteLine(shifted[0] + "" "" + shifted[2] + "" "" + new System.Collections.BitArray(new[] { false, false, true }).RightShift(2)[0]);
var count = 0;
foreach (bool bit in bits) {
  if (bit) count++;
}
Console.WriteLine(count);");
    }

    [TestMethod]
    public void TestSpanToArrayFillAndClear() {
      SameAsNet.Prints(@"
var items = new[] { 1, 2, 3, 4, 5 };
var middle = items.AsSpan(1, 3);
var copy = middle.ToArray();
middle.Fill(9);
Console.WriteLine(string.Join("","", items) + "" "" + string.Join("","", copy) + "" "" + copy.Length);
middle.Slice(1).Clear();
Console.WriteLine(string.Join("","", items));
Span<int> small = stackalloc int[3];
small.Fill(7);
Console.WriteLine(string.Join("","", small.ToArray()));
var points = new P[2];
var span = new Span<P>(points);
span.Fill(new P { X = 4 });
points[0].X = 5;
Console.WriteLine(points[0].X + "" "" + points[1].X);
var whole = new Span<int>(items);
var all = whole.ToArray();
whole.Clear();
Console.WriteLine(string.Join("","", items) + "" "" + string.Join("","", all));
var copies = span.ToArray();
copies[1].X = 6;
Array.Fill(points, new P { X = 8 });
points[1].X = 9;
Console.WriteLine(points[0].X + "" "" + points[1].X + "" "" + copies[1].X + "" "" + span[1].X);", "struct P { public int X; }\n");
    }

    [TestMethod]
    public void TestAsSpanFromAStart() {
      SameAsNet.Prints(@"
var items = new[] { 1, 2, 3, 4 };
var rest = items.AsSpan(1);
Console.WriteLine(rest.Length + "" "" + rest[0] + "" "" + string.Join("","", rest.ToArray()));
rest.Slice(1).Fill(9);
items.AsSpan(3).Clear();
Console.WriteLine(string.Join("","", items));
var text = ""hello"".AsSpan(1);
Console.WriteLine(text.Length + "" "" + text.ToString() + "" "" + ""hello"".AsSpan(1, 2).ToString() + "" "" + ""hello"".AsSpan().Length);
Console.WriteLine(string.Join("","", items.AsSpan(^2).ToArray()) + "" "" + string.Join("","", items.AsSpan(1..3).ToArray()) + "" "" + ""hello"".AsSpan(1..3).ToString() + "" "" + ""hello"".AsSpan(^1).ToString());");
    }
  }
}
