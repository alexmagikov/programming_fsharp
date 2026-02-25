module SecondHomework.Test.EvenNumbersTests

open FsCheck
open FsUnit
open NUnit.Framework
open SecondHomework.EvenNumbers

let allFunAreEqual (list:list<int>) =
    let resMap = countEvenNumbersByMap list
    let resFilter = countEvenNumbersByFilter list
    let resFold = countEvenNumbersByFold list
    
    resMap = resFilter && resFilter = resFold
    
[<Test>]
let ``All func should be equal`` () =
    Check.QuickThrowOnFailure allFunAreEqual
   
[<Test>]
let ``EvenNumberCounter on list with 1 element should return 1 or 0`` () =
    countEvenNumbersByFilter [1]
    |> should equal 0
    
[<Test>]
let ``EvenNumberCounter on normal list should return normal value`` () =
    countEvenNumbersByFilter [1; 2; 3; 4; 5; 6]
    |> should equal 3
    
[<Test>]
let ``EvenNumberCounter on list with 2 equal num should return normal value`` () =
    countEvenNumbersByFilter [1; 2; 3; 4; 6; 6]
    |> should equal 4

[<Test>]
let ``EvenNumberCounter on empty list should return 0`` () =
    countEvenNumbersByFilter []
    |> should equal 0
   
[<Test>]
let ``EvenNumberCounter on list with negative num should return normal value`` () =
    countEvenNumbersByFilter [1; 2; 3; -2]
    |> should equal 2