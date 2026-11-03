using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.SyntaxTests {
  /// <summary>
  /// Members of StringBuilder, char and string the Lua core lacked or answered wrongly (Moonwell's MW0030 rejected some,
  /// and missed others): each prints in Lua what it prints on .NET. A line break is "\n" in the game, where .NET on
  /// Windows writes "\r\n", so those tests say what Lua prints.
  /// </summary>
  [TestClass]
  public class LibraryTextTests {
    [TestMethod]
    public void TestStringBuilderReplace() {
      SameAsNet.Prints(@"
var sb = new StringBuilder(""banana"");
Console.WriteLine(sb.Replace(""an"", ""AN"").ToString());
Console.WriteLine(sb.Replace('a', 'o').Replace('N', 'n', 0, 3).ToString());
Console.WriteLine(sb.Replace(""o"", """", 2, 4).Replace(""x"", ""y"").ToString() + "" "" + sb.Length);
Console.WriteLine(new StringBuilder(""a%b"").Replace(""%"", ""%%"").Replace(""."", ""-"").ToString());");
    }

    [TestMethod]
    public void TestStringBuilderInsert() {
      SameAsNet.Prints(@"
var sb = new StringBuilder(""ace"");
sb.Insert(1, ""b"").Insert(3, 'd').Append(""!"");
Console.WriteLine(sb.ToString() + "" "" + sb.Length);
sb.Insert(0, 5).Insert(0, true).Insert(0, ""ab"", 2).Insert(sb.Length, new[] { 'x', 'y' });
Console.WriteLine(sb.ToString() + "" "" + sb.Length);");
    }

    [TestMethod]
    public void TestStringBuilderRemove() {
      SameAsNet.Prints(@"
var sb = new StringBuilder(""abcdef"");
Console.WriteLine(sb.Remove(1, 2).ToString() + "" "" + sb.Length);
Console.WriteLine(sb.Remove(0, 0).Remove(2, 2).Append('z').ToString() + "" "" + sb.Length);");
    }

    [TestMethod]
    public void TestStringBuilderEquals() {
      SameAsNet.Prints(@"
var a = new StringBuilder(""ab"");
var b = new StringBuilder(""a"").Append('b');
Console.WriteLine(a.Equals(b) + "" "" + a.Equals(new StringBuilder(""abc"")) + "" "" + a.Equals((StringBuilder)null));");
    }

    [TestMethod]
    public void TestStringBuilderAppendJoin() {
      SameAsNet.Prints(@"
var sb = new StringBuilder();
sb.AppendJoin("","", ""a"", ""b"").Append(' ');
sb.AppendJoin("", "", new[] { 1, 2 }).Append(' ');
sb.AppendJoin('-', new[] { ""c"", ""d"" }).Append(' ');
sb.AppendJoin(""+"", new List<string> { ""e"" });
Console.WriteLine(sb.ToString());");
    }

    [TestMethod]
    public void TestStringBuilderToStringOfARange() {
      SameAsNet.Prints(@"
var sb = new StringBuilder(""abc"").Append(""def"");
Console.WriteLine(sb.ToString(1, 3) + "" "" + sb.ToString(0, 0).Length + "" "" + sb.ToString(5, 1));");
    }

    [TestMethod]
    public void TestStringBuilderAppendOfChars() {
      SameAsNet.Prints(@"
var sb = new StringBuilder();
sb.Append(new[] { 'h', 'i' }).Append(new[] { 'a', 'b', 'c' }, 1, 2);
Console.WriteLine(sb.ToString() + "" "" + sb.Length);");
    }

    [TestMethod]
    public void TestStringBuilderCopyTo() {
      SameAsNet.Prints(@"
var sb = new StringBuilder(""abcd"");
var chars = new char[4];
sb.CopyTo(1, chars, 1, 2);
chars[0] = '_';
chars[3] = '_';
Console.WriteLine(new string(chars));");
    }

    [TestMethod]
    public void TestCharAsciiHelpers() {
      SameAsNet.Prints(@"
foreach (var c in new[] { 'a', 'Z', '5', 'f', 'F', 'g', ' ' }) {
  Console.WriteLine(char.IsAscii(c) + "" "" + char.IsAsciiDigit(c) + "" "" + char.IsAsciiLetterLower(c) + "" "" + char.IsAsciiLetterUpper(c) + "" ""
    + char.IsAsciiLetterOrDigit(c) + "" "" + char.IsAsciiHexDigit(c) + "" "" + char.IsAsciiHexDigitLower(c) + "" "" + char.IsAsciiHexDigitUpper(c) + "" ""
    + char.IsBetween(c, 'a', 'z') + "" "" + char.ToUpperInvariant(c) + char.ToLowerInvariant(c));
}
var e = (char)233;
Console.WriteLine(char.IsAscii(e) + "" "" + char.IsAsciiLetterOrDigit(e) + "" "" + char.IsBetween(e, 'a', 'z') + "" "" + char.IsBetween('m', 'z', 'a'));");
    }

    [TestMethod]
    public void TestCharIsSymbolAndGetNumericValue() {
      SameAsNet.Prints(@"
Console.WriteLine(char.IsSymbol('+') + "" "" + char.IsSymbol('$') + "" "" + char.IsSymbol('a') + "" "" + char.IsSymbol(""a+"", 1));
Console.WriteLine(char.GetNumericValue('7') + "" "" + char.GetNumericValue('x') + "" "" + char.GetNumericValue(""a9"", 1));");
    }

    [TestMethod]
    public void TestNewLineIsTheGamesLineBreak() {
      var output = LuaRun.Run(@"
using System;
using System.Text;

class Program {
  public static void Main() {
    Console.WriteLine(Environment.NewLine.Length + "" "" + (Environment.NewLine == ""\n"") + "" "" + (new StringBuilder().AppendLine().ToString() == Environment.NewLine));
    Console.WriteLine(""a\r\nb\rc"".ReplaceLineEndings() == ""a\nb\nc"");
  }
}
");
      Assert.AreEqual("1 True True\nTrue\n", output);
    }

    [TestMethod]
    public void TestReplaceLineEndings() {
      SameAsNet.Prints(@"
Console.WriteLine(""a\r\nb\nc\rd\fe\u0085f\x2028g\x2029h"".ReplaceLineEndings(""|""));
Console.WriteLine(""no breaks"".ReplaceLineEndings(""%""));
Console.WriteLine(""1\n2"".ReplaceLineEndings(""%1""));");
    }

    [TestMethod]
    public void TestStringReplaceKeepsPercent() {
      SameAsNet.Prints(@"
Console.WriteLine(""a-b"".Replace(""-"", ""%"") + "" "" + ""a-b"".Replace(""-"", ""%1"") + "" "" + ""50"".Replace(""0"", ""0%%""));");
    }

    [TestMethod]
    public void TestIntern() {
      SameAsNet.Prints(@"
var text = string.Intern(""ab"");
Console.WriteLine(text + "" "" + (string.IsInterned(""ab"") ?? ""null"") + "" "" + ReferenceEquals(text, string.Intern(""a"" + ""b"")));");
    }

    [TestMethod]
    public void TestEnumerateRunes() {
      SameAsNet.Prints(@"
foreach (var rune in ""hé€\U0001F600"".EnumerateRunes()) {
  Console.WriteLine(rune.Value + "" "" + rune + "" "" + rune.Utf8SequenceLength + "" "" + rune.IsAscii);
}
var count = 0;
foreach (var rune in """".EnumerateRunes()) {
  count++;
}
Console.WriteLine(count);");
    }
  }
}
