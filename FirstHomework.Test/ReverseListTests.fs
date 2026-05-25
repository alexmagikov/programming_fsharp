module FirstHomework.Test.ReverseListTests

open ReverseList
open FsUnit
open NUnit.Framework

[<Test>]
let ``Reverse empty list should return empty list`` () =
    reverseList [] |> should equal []
 
[<Test>]
let ``Reverse normal list should return reversed list`` () =
    reverseList [1;2;3] |> should equal [3;2;1]

[<Test>]
let ``Reverse list with 2 equal elements should return the same list`` () =
    reverseList [1;2;2;3] |> should equal [3;2;2;1]
    
[<Test>]
let ``Reverse list with solo elements should return the same list`` () =
    reverseList [3] |> should equal [3]
   
[<Test>]
let ``Reverse already reversed list should return original list`` () =
    reverseList (reverseList [1;2;3]) |> should equal [1;2;3]