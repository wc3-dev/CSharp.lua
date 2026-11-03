using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// params that is not a plain array of a method of the code's own. params List&lt;int&gt; or IEnumerable&lt;int&gt; (C# 13)
  /// stopped the compiler: the call packed the arguments as if the parameter were an array. They are now packed as a
  /// collection expression of the parameter's type is, and an argument that already is of that type is passed as it is. A
  /// lambda's or a delegate's params was not packed at all, so the function got the first argument for the array. The
  /// expected lines are what the sample prints on .NET.
  /// </summary>
  [TestClass]
  public class ParamsCollectionsTests {
    [TestMethod]
    public void TestParamsCollections() {
      var output = GameScript.Run("ParamsCollections", "Program");
      Assert.AreEqual("3 0 1\n6 0 11\n2 3 2\n3 0 1\n2 0\n", output);
    }
  }
}
