using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  /// <summary>
  /// A struct that can change was copied only where its bare name stood in a declaration, an assignment, an argument or a
  /// return. Put into a collection by an initializer, a collection expression or an indexer (new List&lt;P&gt; { a }, [a],
  /// list[0] = a), given to a delegate (f(a), list.ForEach(p => p.X = 9)), read through its owner (Bump(h.Value), return
  /// h.Value), or standing in parentheses, ?:, ??, a switch expression or an expression body (P Get() => value), it was the
  /// same table, so a change to one was a change to the other. So was a struct a call or a property gave back as a type
  /// parameter (list.First(), queue.Peek(), n.Value, a generic class's Get()): it was the collection's or the object's
  /// own. It is now a copy in each. A change made in place (h.Value.X = 9, arr[0].Move(), a ref) still changes the
  /// original. The expected lines are what the sample prints on .NET: the value that must be kept, then the changed one.
  /// </summary>
  [TestClass]
  public class StructStoreCopiesTests {
    [TestMethod]
    public void TestAStructStoredOrGivenOnIsACopy() {
      var output = GameScript.Run("StructStoreCopies", "Program", "P", "Outer", "Holder", "Box_1");
      var expected = string.Join("\n", new[] {
        "list-initializer 1 9",
        "dict-index-initializer 1 9",
        "dict-add-initializer 1 9",
        "collection-expression-list 1 9",
        "collection-expression-array 1 9",
        "array-initializer 1 9",
        "implicit-array-initializer 1 9",
        "bare-array-initializer 1 9",
        "array-2d-initializer 1 9",
        "list-index-store 1 9",
        "dict-index-store 1 9",
        "tuple-item-store 1 9",
        "list-initializer-field 1 9",
        "list-initializer-own-field 2 9",
        "collection-expression-field 1 9",
        "add-field 1 9",
        "list-initializer-element 1 9",
        "list-index-store-element 1 9",
        "list-index-store-conditional 1 9",
        "lambda 1 9",
        "lambda-method 1 2",
        "anonymous-method 1 9",
        "lambda-two-parameters 1 9",
        "for-each-lambda 1 9",
        "for-each-lambda-method 1 2",
        "select-lambda 1 9",
        "select-identity 1 9",
        "method-group 1 9",
        "own-delegate 1 9",
        "lambda-capture 1 9",
        "select-capture 1 9",
        "lambda-invoke 1 9",
        "lambda-conditional-invoke 1 9",
        "sort-lambda 1 9",
        "argument-field 1 9",
        "argument-own-field 1 9",
        "argument-static-field 1 9",
        "argument-nested 1 9",
        "return-field 1 9",
        "return-this-field 1 9",
        "arrow-method 1 9",
        "arrow-property 1 9",
        "yield-field 1 9",
        "tuple-field 1 9",
        "lambda-result 1 9",
        "lambda-result-parameter 1 9",
        "conditional 1 9",
        "switch-expression 1 9",
        "parenthesized 1 9",
        "assign-parenthesized 1 9",
        "coalesce 1 9",
        "coalesce-right 1 9",
        "linq-first 1 9",
        "linq-first-or-default 1 9",
        "linq-single 1 9",
        "list-find 1 9",
        "linq-first-argument 1 9",
        "linq-first-array-initializer 1 9",
        "queue-peek 1 9",
        "stack-peek 1 9",
        "dict-get-value-or-default 1 9",
        "dict-values-first 1 9",
        "dict-foreach-value 1 9",
        "generic-class-method 1 9",
        "generic-class-arrow-property 1 9",
        "generic-class-block-property 1 9",
        "generic-class-auto-property 1 9",
        "generic-class-property-store 1 9",
        "generic-method-element 1 9",
        "nullable-value 1 9",
        "nullable-get-value-or-default 1 9",
        "enumerator-current 1 9",
        "delegate-of-core 1 9",
        "linq-first-read 1 9",
        "linq-first-number 1 9",
        "field-in-place 11 11",
        "field-by-ref 9 9",
        "nested-in-place 10 10",
        "element-in-place 10 10",
        "ref-local 9 9",
      }) + "\n";
      Assert.AreEqual(expected, output);
    }
  }
}
