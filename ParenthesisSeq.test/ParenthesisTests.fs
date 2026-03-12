module ParenthesisSeq.test.ParenthisisTest

open NUnit.Framework
open ParenthesisSeq.ParenthesisSeq

[<TestCase("", true)>]
[<TestCase("()", true)>]
[<TestCase("[]", true)>]
[<TestCase("{}", true)>]
[<TestCase("()[]{}", true)>]
[<TestCase("{[]}", true)>]
[<TestCase("([{}])", true)>]
[<TestCase("(", false)>]
[<TestCase(")", false)>]
[<TestCase("(]", false)>]
[<TestCase("([)]", false)>]
[<TestCase(")(", false)>]
[<TestCase("(()", false)>]
[<TestCase("())", false)>]
[<TestCase("erewfwfew", true)>]
[<TestCase("gesgesg(serer[gre]{dwef}fwe)f", true)>]
let ``isBalanced tests`` (input: string) (expected: bool) =
    Assert.That(isBalanced input, Is.EqualTo(expected))
 
 
 
 
  