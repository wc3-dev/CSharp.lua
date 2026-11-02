using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using CSharpLua.CoreSystem;

using Microsoft.CodeAnalysis;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// What a case prints in Lua: compiled as a map's script is (Moonwell's settings, CoreSystem in the same file), and run on
  /// the Lua 5.3 of test/__bin with no debug library. A case that stops before its first test method (T1, T2, ...) runs, in
  /// the compiler or in Lua, is run again one test method at a time, so the others still count.
  /// </summary>
  internal static class LuaRun {
    public static readonly string Root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
    private static readonly string LuaExe = Path.Combine(Root, "test", "__bin", "lua5.3", "lua.exe");
    private static readonly string CoreSystemFolder = Path.Combine(Root, "CSharp.lua", "CoreSystem.Lua");
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

    private const string Runner = @"
local path = ...
local file = assert(io.open(path, 'rb'))
local code = file:read('a')
file:close()
local chunk, failure = load(code .. '\nreturn InitCSharp\n', '=case')
if not chunk then
  print('NO LOAD ' .. failure)
  return
end
debug = nil
local loaded, init = pcall(chunk)
if not loaded then
  print('LOAD ERROR ' .. tostring(init))
  return
end
local ok, err = xpcall(init, function (e)
  if type(e) == 'table' then
    local named, name = pcall(function () return e:GetType().Name end)
    return 'EXCEPTION ' .. tostring(named and name or e)
  end
  return 'LUA ERROR ' .. tostring(e)
end)
if not ok then
  print(err)
end
";

    public static string Run(string code) {
      var whole = RunOnce(code);
      var tests = TestMethods.Names(code);
      if (tests.Count == 0 || tests.Any(test => whole.Contains("# " + test + "\n"))) {
        return whole;
      }
      var text = new StringBuilder("whole file: " + whole);
      foreach (var test in tests) {
        var one = RunOnce(TestMethods.Keep(code, test));
        text.Append(one.StartsWith("# " + test + "\n") ? one : "# " + test + "\n" + one);
      }
      return text.ToString();
    }

    private static string RunOnce(string code) {
      string lua;
      try {
        lua = Compile(code);
      } catch (Exception e) {
        var inner = e;
        while (inner.InnerException != null) {
          inner = inner.InnerException;
        }
        if (inner is CompilationErrorException refused) {
          var error = refused.EmitResult?.Diagnostics.FirstOrDefault(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
          return "REFUSED " + Unplaced(FirstLine(error?.ToString() ?? refused.Message)) + "\n";
        }
        return "CRASH " + inner.GetType().Name + ": " + Unplaced(FirstLine(inner.Message)) + "\n";
      }
      return Execute(lua);
    }

    private static string Compile(string code) {
      var folder = Path.Combine(Path.GetTempPath(), "syntax-case-" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(folder);
      try {
        File.WriteAllText(Path.Combine(folder, "Case.cs"), code);
        var compiler = new Compiler(folder, folder, string.Empty, null, null, "*", string.Empty, false, null, string.Empty) {
          IsExportMetadata = false,
          IsModule = false,
          IsInlineSimpleProperty = false,
          IsPreventDebugObject = true,
        };
        var core = CoreSystemProvider.GetCoreSystemFiles().Select(file => Path.Combine(CoreSystemFolder, file)).ToList();
        using var stream = new MemoryStream();
        compiler.CompileSingleFile(stream, core);
        return Encoding.UTF8.GetString(stream.ToArray());
      } finally {
        Directory.Delete(folder, true);
      }
    }

    private static string Execute(string lua) {
      var folder = Path.Combine(Path.GetTempPath(), "syntax-lua-" + Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(folder);
      try {
        var script = Path.Combine(folder, "case.lua");
        var runner = Path.Combine(folder, "run.lua");
        File.WriteAllText(script, lua);
        File.WriteAllText(runner, Runner);
        var start = new ProcessStartInfo(LuaExe, "\"" + runner + "\" \"" + script + "\"") {
          WorkingDirectory = folder,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
          StandardOutputEncoding = Encoding.UTF8,
        };
        using var process = Process.Start(start);
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();
        if (!process.WaitForExit(Timeout)) {
          process.Kill(true);
          process.WaitForExit();
          return Clean(output.Result) + "TIMEOUT\n";
        }
        var text = Clean(output.Result);
        return process.ExitCode == 0 ? text : text + "LUA STOPPED " + Regex.Replace(FirstLine(Clean(error.Result)), @"^.*?lua\.exe: ", "") + "\n";
      } finally {
        Directory.Delete(folder, true);
      }
    }

    // A Lua error names the chunk and the line, and the line moves with every change to CoreSystem.
    private static string Clean(string text) => Regex.Replace(text.Replace("\r\n", "\n"), @"case:\d+: ", "");

    // The compiler names the case's file, which is in a folder of its own each time.
    private static string Unplaced(string text) => Regex.Replace(text, @"[^\s(""]*Case\.cs", "Case.cs");

    private static string FirstLine(string text) => text.Split('\n')[0].TrimEnd('\r');
  }
}
