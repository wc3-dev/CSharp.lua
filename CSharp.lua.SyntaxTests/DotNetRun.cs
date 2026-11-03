using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// What a case prints on .NET, written beside its Lua when the cases are recorded. It runs in the culture whose number
  /// formats CoreSystem writes, on any machine: the invariant culture's, with 50% for a percent.
  /// </summary>
  internal static class DotNetRun {
    // Console.Out is the whole process's.
    private static readonly object Running = new();

    private static readonly CultureInfo Culture = GameCulture();

    private static CultureInfo GameCulture() {
      var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
      culture.NumberFormat.PercentPositivePattern = 1;
      culture.NumberFormat.PercentNegativePattern = 1;
      return culture;
    }

    public static string Run(string code) {
      var platform = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);
      var references = platform
        .Where(path => Path.GetFileName(path).StartsWith("System.") || Path.GetFileName(path) is "mscorlib.dll" or "netstandard.dll")
        .Select(path => MetadataReference.CreateFromFile(path));
      var tree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(LanguageVersion.CSharp13));
      var compilation = CSharpCompilation.Create("Case" + Guid.NewGuid().ToString("N"), new[] { tree }, references,
        new CSharpCompilationOptions(OutputKind.ConsoleApplication, allowUnsafe: true));
      using var assembly = new MemoryStream();
      var emitted = compilation.Emit(assembly);
      if (!emitted.Success) {
        return "DOES NOT COMPILE " + emitted.Diagnostics.First(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error) + "\n";
      }
      assembly.Position = 0;
      lock (Running) {
        var context = new AssemblyLoadContext(null, true);
        var output = new StringWriter();
        var console = Console.Out;
        var culture = CultureInfo.CurrentCulture;
        Console.SetOut(output);
        CultureInfo.CurrentCulture = Culture;
        try {
          var main = context.LoadFromStream(assembly).EntryPoint;
          main.Invoke(null, main.GetParameters().Length == 0 ? null : new object[] { Array.Empty<string>() });
        } catch (TargetInvocationException e) {
          output.WriteLine("EXCEPTION " + e.InnerException.GetType().Name);
        } finally {
          CultureInfo.CurrentCulture = culture;
          Console.SetOut(console);
          context.Unload();
        }
        return output.ToString().Replace("\r\n", "\n");
      }
    }
  }
}
