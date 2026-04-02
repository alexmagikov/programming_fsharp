module ControlWork.test.MinEl

open ControlWork02_04.MinElement
open NUnit.Framework
open FsUnit

[<Test>]
let ``FindMinEl for usual list should return expected value`` () =
    findMinEl [1; 2; 3; 4] |> should equal (Some 1)
    
[<Test>]
let ``FindMinEl for empty list should return None`` () =
    findMinEl [] |> should equal None
    
[<Test>]
let ``FindMinEl with negative nums should return expected value`` () =
    findMinEl [1; 2; 3; -1] |> should equal (Some(-1))