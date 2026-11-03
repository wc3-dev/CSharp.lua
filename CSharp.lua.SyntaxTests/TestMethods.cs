using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpLua.SyntaxTests {
  /// <summary>A case's test methods, T1, T2, ..., which its Main runs one after another through H.R.</summary>
  internal static class TestMethods {
    // A test's own helpers are named after it (T3_Make), and go with it.
    private static readonly Regex Test = new(@"^T\d+(_\w*)?$");

    public static List<string> Names(string code) =>
      Regex.Matches(code, @"static (?:unsafe )?void (T\d+)\(").Select(match => match.Groups[1].Value).Distinct().ToList();

    /// <summary>The case with every test method but <paramref name="test"/> left out, and Main's calls of them.</summary>
    public static string Keep(string code, string test) {
      var root = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(LanguageVersion.CSharp13)).GetRoot();
      var drop = new List<SyntaxNode>();
      foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>()) {
        var name = method.Identifier.Text;
        if (Test.IsMatch(name) && name != test && !name.StartsWith(test + "_")) {
          drop.Add(method);
        } else if (name == "Main") {
          foreach (var statement in method.DescendantNodes().OfType<ExpressionStatementSyntax>()) {
            var called = Regex.Match(statement.ToString(), @"\bT\d+\b");
            if (called.Success && called.Value != test) {
              drop.Add(statement);
            }
          }
        }
      }
      return root.RemoveNodes(drop, SyntaxRemoveOptions.KeepNoTrivia).ToFullString();
    }
  }
}
