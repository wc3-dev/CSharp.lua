using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// A value in an object or collection initializer converts to its member's, indexer's or Add's type as anywhere else:
  /// a user-defined implicit conversion is called, and an int given to a float is a float (16777217 is no float, so .NET
  /// keeps 16777216). The compiler left both out there, so new JsonObject { ["gold"] = 25 } handed its indexer a bare
  /// number. Each prints in Lua what it prints on .NET.
  /// </summary>
  [TestClass]
  public class InitializerConversionTests {
    private const string Members = @"
class N {
  public readonly string Text;
  public N(string text) { Text = text; }
  public static implicit operator N(int value) => new N(""int "" + value);
  public static implicit operator N(string value) => new N(""text "" + value);
}

class Holder : IEnumerable<N> {
  public N Node;
  public float Real;
  public readonly List<N> Items = new List<N>();
  public N this[string key] { get => Items[0]; set => Items.Add(value); }
  public void Add(N item) => Items.Add(item);
  public void Add(N first, N second) { Items.Add(first); Items.Add(second); }
  public IEnumerator<N> GetEnumerator() => Items.GetEnumerator();
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
";

    [TestMethod]
    public void TestAnInitializerConvertsItsValues() {
      SameAsNet.Prints(@"
Console.WriteLine(new Holder { Real = 16777217 }.Real + "" "" + new List<float> { 16777217 }[0]);
var members = new Holder { Node = 3, [""k""] = ""v"" };
var items = new Holder { 4, ""w"", { 5, ""x"" } };
var indexed = new Dictionary<string, N> { [""a""] = 6 };
var added = new Dictionary<string, N> { { ""b"", 7 } };
var list = new List<N> { 8 };
Console.WriteLine(members.Node.Text + "", "" + members.Items[0].Text + "", "" + items.Items[0].Text + "", "" + items.Items[1].Text + "", "" + items.Items[2].Text + "", "" + items.Items[3].Text);
Console.WriteLine(indexed[""a""].Text + "", "" + added[""b""].Text + "", "" + list[0].Text);", Members);
    }
  }
}
