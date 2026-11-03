using System.IO;
using System.Linq;

using CSharpLua.CoreSystem;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// Moonwell leaves CoreSystem's Numerics and Threading files out of a map's script when nothing else in it names their
  /// namespace, so a core file outside those folders, which every map has, must not name it: one that did put every
  /// Numerics file in every map.
  /// </summary>
  [TestClass]
  public class TrimmedGroupNamesTests {
    [TestMethod]
    public void TestOnlyAGroupsOwnFilesNameItsNamespace() {
      var root = Path.Combine(Path.GetFullPath(@"..\..\..\.."), "CSharp.lua", "CoreSystem.Lua");
      var naming = CoreSystemProvider.GetCoreSystemFiles()
        .Select(file => file.Replace(Path.DirectorySeparatorChar, '/'))
        .Where(file => !file.Contains("/Numerics/") && !file.Contains("/Threading/"))
        .Where(file => {
          var text = File.ReadAllText(Path.Combine(root, file));
          return text.Contains("System.Numerics") || text.Contains("System.Threading");
        })
        .ToList();
      Assert.AreEqual("", string.Join(", ", naming));
    }
  }
}
