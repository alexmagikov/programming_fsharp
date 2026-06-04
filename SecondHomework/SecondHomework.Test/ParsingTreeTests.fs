module SecondHomework.Test.ParsingTreeTests

open NUnit.Framework
open FsUnit
open SecondHomework.ParsingTree

[<Test>]
let ``eval of parsing tree of number should return number`` () =
    eval (Num 5) id
    |> should equal 5
 
[<Test>]
let ``eval of parsing tree should return correct num`` () =
    let tree = Mul (
            Add (Num 5, Num 5),
            Num 5
        )
    eval tree id
    |> should equal 50
    
[<Test>]
let ``eval of parsing tree with 2 equal branch should return correct num`` () =
    let tree = Mul (
            Add (Num 5, Num 5),
            Sub (
                Div (Num 6, Num 2),
                Num 3
            )
        )
    eval tree id
    |> should equal 0
    
[<Test>]
let ``eval of parsing tree with div to zero should return exception`` () =
    let tree = Div (Num 5, Num 0)
    (fun () -> eval tree id |> ignore)
    |> should throw typeof<System.DivideByZeroException>