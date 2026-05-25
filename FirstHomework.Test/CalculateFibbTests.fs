module FirstHomework.Test.CalculateFibbTests

open CalculateFibb
open FsUnit
open NUnit.Framework

[<Test>]
let ``CalcFibb of negative number should return Error`` () =
     calculateFibb -3 |> should equal (Error "Num must be positive" : Result<int, string>)

[<Test>]
let ``CalcFibb of 0 should return 0`` () =
     calculateFibb 0 |> should equal (Ok 0 : Result<int, string>)

[<Test>]
let ``CalcFibb of 1 should return 1`` () =
     calculateFibb 1 |> should equal (Ok 1 : Result<int, string>)
     
[<Test>]
let ``CalcFibb of num > 1 should return fib num`` () =
     calculateFibb 6 |> should equal (Ok 8 : Result<int, string>)