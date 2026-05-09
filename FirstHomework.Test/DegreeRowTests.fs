module FirstHomework.Test.DegreeRowTests

open DegreeRow
open FsUnit
open NUnit.Framework

[<Test>]
let ``DegreeRow for normal value should return normal value`` () =
    degreeRow 2 3 |> should equal [4; 8; 16; 32]
    
[<Test>]
let ``DegreeRow for n=0 and m = 1 should return [1; 2]`` () =
    degreeRow 0 1 |> should equal [1; 2]
    
[<Test>]
let ``DegreeRow for n=1 and m = 0 should return [2]`` () =
    degreeRow 1 0 |> should equal [2]
    
[<Test>]
let ``DegreeRow for n=0 and m = 0 should return [1]`` () =
    degreeRow 0 0 |> should equal [1] 