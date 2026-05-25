module FirstHomework.Test.FactorialTests

open Factorial
open FsUnit
open NUnit.Framework

[<Test>]
let ``Factorial of negative num should return Error`` () =
    factorial -3 |> should equal (Error "Num must be positive" : Result<bigint, string>)

[<Test>]
let ``Factorial of 0 should return 1`` () =
    factorial 0 |> should equal (Ok 1I : Result<bigint, string>)
    
[<Test>]
let ``Factorial of 1 should return 1`` () =
    factorial 1 |> should equal (Ok 1I : Result<bigint, string>)

[<Test>]
let ``Factorial of normal num should return normal value`` () =
    factorial 4 |> should equal (Ok 24I : Result<bigint, string>)
    
[<Test>]
let ``Factorial of big number should not throw StackOverflow`` () =
    let result = factorial 100000
    match result with
     | Ok _ -> Assert.Pass()
     | Error e -> Assert.Fail($"Should be Ok, but was {e}")