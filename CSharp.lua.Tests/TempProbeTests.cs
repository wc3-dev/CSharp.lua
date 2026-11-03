using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CSharpLua.Tests {
  [TestClass]
  public class TempProbeTests {
    [TestMethod]
    public void Probe() { System.Console.WriteLine(Compiler.CompileSingleCodeForGame(System.IO.File.ReadAllText(@"C:\Users\MDLSVE~1\AppData\Local\Temp\claude\C--Users-mdlsvensson-Repo-wc3-cs\83d7135c-6cae-4d8a-b94f-9d030fd6597e\scratchpad\probe-collections.cs"))); }
  }
}
