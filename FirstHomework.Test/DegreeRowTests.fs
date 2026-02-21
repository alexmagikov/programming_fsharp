module FirstHomework.Test.DegreeRowTests

open DegreeRow
open FsUnit
open NUnit.Framework

[<Test>]
let ``DegreeRow for normal value should return normal value`` () =
    degreeRow 2u 3u |> should equal [4u; 8u; 16u; 32u]
    
[<Test>]
let ``DegreeRow for n=0 and m = 1 should return [1; 2]`` () =
    degreeRow 0u 1u |> should equal [1u; 2u]
    
[<Test>]
let ``DegreeRow for n=1 and m = 0 should return [2]`` () =
    degreeRow 1u 0u |> should equal [2u]
    
[<Test>]
let ``DegreeRow for n=0 and m = 0 should return [1]`` () =
    degreeRow 0u 0u |> should equal [1u] 