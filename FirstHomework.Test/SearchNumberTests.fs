module FirstHomework.Test.SearchNumberTests

open SearchNumber
open FsUnit
open NUnit.Framework

[<TestFixture>]
type SearchNumberTests () =
    let mutable testList = []
    
    [<SetUp>]
    member _.Setup() =
        testList <- [20; 30; 4; 4; 5; 1; 2]

    [<Test>]
    member _.``unknown -> None`` () =
        searchNum testList 10 |> should equal None
    
    [<Test>]
    member _.``existing -> index`` () =
        searchNum testList 5 |> should equal (Some 4)
        
    [<Test>]
    member _.``duplicates -> first index`` () =
        searchNum testList 4 |> should equal (Some 2)
        
    [<Test>]
    member _.``empty list -> None`` () =
        let emptyList = []
        searchNum emptyList 10 |> should equal None
    
    [<Test>]
    member _.``oneElementList -> 0`` () =
        let oneElementList = [10]
        searchNum oneElementList 10 |> should equal (Some 0)