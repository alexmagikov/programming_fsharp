module SecondHomework.Test.PrimeNumbers

open NUnit.Framework
open FsUnit
open PrimeNumbers


[<Test>]
let ``Sequence should contain prime numbers`` () =
    generatePrimeNumbers
     |> Seq.take 5
     |> Seq.toList
     |> should equal [2; 3; 5; 7; 11]