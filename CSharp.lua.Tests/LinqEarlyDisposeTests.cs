using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A LINQ method of the core that gives a value before its source ends (First, Any, All, Contains, ElementAt, Single,
  /// SequenceEqual) left the source's enumerator as it was, so an iterator below it never ran its finally blocks, where a
  /// foreach left early and a LINQ chain already disposed theirs. Each now disposes the enumerators it read, as .NET's do.
  /// The expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class LinqEarlyDisposeTests {
    [TestMethod]
    public void TestLinqDisposesAnIteratorItStopsEarly() {
      var output = GameScript.Run("LinqEarlyDispose", "Program");
      Assert.AreEqual(
        "First 1 finally\n"
        + "FirstWhere 2 finally\n"
        + "FirstOrDefault 2 finally\n"
        + "Any True finally\n"
        + "AnyWhere True finally\n"
        + "All False finally\n"
        + "Contains True finally\n"
        + "ElementAt 2 finally\n"
        + "ElementAtOrDefault 1 finally\n"
        + "SequenceEqual False finally\n"
        + "SequenceEqualShort False finally\n"
        + "Single throws finally\n"
        + "SingleWhere throws finally\n"
        + "TakeFirst 1 finally\n"
        + "WhereFirst 2 finally\n"
        + "Last 3 finally\n"
        + "Count 3 finally\n",
        output);
    }
  }
}
