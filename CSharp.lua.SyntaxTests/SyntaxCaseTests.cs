using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// About 770 checks of C# in 253 small programs (Cases/*.cs), from a coverage report of the compiler (2026-10-25); a case's
  /// ids, such as t02 T3 or a01, are the report's. Each case must print in Lua what its .lua.txt recorded, the forms that are
  /// still wrong included, so that any change shows. Its .net.txt is what it prints on .NET.
  /// When a change is meant to alter what a case prints, record the cases again (CSHARPLUA_RECORD=1 dotnet test) and read
  /// the diff of the .lua.txt files: each changed test should now print what .NET prints.
  /// </summary>
  [TestClass]
  public class SyntaxCaseTests {
    private static readonly string Cases = Path.Combine(LuaRun.Root, "CSharp.lua.SyntaxTests", "Cases");
    private static readonly bool Recording = Environment.GetEnvironmentVariable("CSHARPLUA_RECORD") == "1";

    // Tests whose lines change from run to run, which are run but not compared: Enum.GetValues and Enum.GetNames go by the
    // order of a Lua table, which is new each run. "" is a case's lines before its first test.
    private static readonly Dictionary<string, string[]> Unsteady = new() {
      ["d07_enum_getvalues_order"] = new[] { "" },
      ["t06_enum"] = new[] { "T8", "T9" },
    };

    [TestMethod]
    public void TestEachCasePrintsWhatWasRecorded() {
      var changes = new ConcurrentBag<string>();
      var names = Directory.GetFiles(Cases, "*.cs").Select(Path.GetFileNameWithoutExtension).OrderBy(name => name, StringComparer.Ordinal);
      Parallel.ForEach(names, name => {
        var code = File.ReadAllText(Path.Combine(Cases, name + ".cs"));
        var lua = LuaRun.Run(code);
        var recordedPath = Path.Combine(Cases, name + ".lua.txt");
        var netPath = Path.Combine(Cases, name + ".net.txt");
        if (Recording) {
          File.WriteAllText(recordedPath, lua);
          File.WriteAllText(netPath, DotNetRun.Run(code));
          return;
        }
        if (!File.Exists(recordedPath)) {
          changes.Add(name + ": not recorded");
          return;
        }
        var recorded = File.ReadAllText(recordedPath).Replace("\r\n", "\n");
        if (lua != recorded) {
          var change = Describe(name, recorded, lua, File.Exists(netPath) ? File.ReadAllText(netPath).Replace("\r\n", "\n") : "");
          if (change.Length > 0) {
            changes.Add(change);
          }
        }
      });
      if (!changes.IsEmpty) {
        Assert.Fail(changes.Count + " cases print something else than was recorded:\n\n" + string.Join("\n", changes.OrderBy(change => change, StringComparer.Ordinal)));
      }
    }

    private static string Describe(string name, string recorded, string now, string net) {
      var before = Tests(recorded);
      var after = Tests(now);
      var dotNet = Tests(net);
      var text = new StringBuilder();
      var unsteady = Unsteady.TryGetValue(name, out var tests) ? tests : Array.Empty<string>();
      foreach (var test in before.Keys.Union(after.Keys).Except(unsteady)) {
        before.TryGetValue(test, out var was);
        after.TryGetValue(test, out var became);
        if (was != became) {
          dotNet.TryGetValue(test, out var expected);
          text.Append(name).Append(' ').Append(test).Append('\n')
            .Append("  recorded: ").Append(OneLine(was)).Append('\n')
            .Append("  now:      ").Append(OneLine(became)).Append('\n')
            .Append("  .NET:     ").Append(OneLine(expected)).Append('\n');
        }
      }
      return text.ToString();
    }

    // The lines of each test, by the "# T1" each starts with; what comes before the first is under "".
    private static Dictionary<string, string> Tests(string output) {
      var tests = new Dictionary<string, string>();
      var test = "";
      var lines = new List<string>();
      foreach (var line in output.Split('\n')) {
        if (line.StartsWith("# ")) {
          tests[test] = string.Join("\n", lines);
          test = line.Substring(2);
          lines.Clear();
        } else if (line.Length > 0) {
          lines.Add(line);
        }
      }
      tests[test] = string.Join("\n", lines);
      return tests;
    }

    private static string OneLine(string lines) => lines == null ? "(none)" : lines.Replace("\n", " | ");
  }
}
