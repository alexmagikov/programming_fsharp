module ControlWork.test.SquareTests

open ControlWork02_04.Square
open NUnit.Framework
open FsUnit

[<Test>]
let ``build 4lines square should return correct list`` () =
    let expected = ["****"; "*  *"; "*  *"; "****"]
    buildSquare 4 |> should equal expected
   
[<Test>]
let ``build 1lines square should return correct list`` () =
    buildSquare 1 |> should equal ["*"]
    
[<Test>]
let ``build 2lines square should return correct list`` () =
    buildSquare 2 |> should equal ["**"; "**"]

