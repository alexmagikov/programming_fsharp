module FirstHomework.Test.DegreeRowTests

open DegreeRow
open FsUnit
open NUnit.Framework

[<Test>]
let ``DegreeRow for normal value should return normal value`` () =
    degreeRow 2 3 |> should equal (Ok [4.0; 8.0; 16.0; 32.0] : Result<float list, string>)
    
[<Test>]
let ``DegreeRow for n=0 and m = 1 should return [1; 2]`` () =
    degreeRow 0 1 |> should equal (Ok [1.0; 2.0] : Result<float list, string>)
    
[<Test>]
let ``DegreeRow for n=1 and m = 0 should return [2]`` () =
    degreeRow 1 0 |> should equal (Ok [2.0] : Result<float list, string>)
    
[<Test>]
let ``DegreeRow for n=0 and m = 0 should return [1]`` () =
    degreeRow 0 0 |> should equal (Ok [1.0] : Result<float list, string>)
    
[<Test>]
let ``DegreeRow for negative n should return fractional values`` () =
    degreeRow -2 3 |> should equal (Ok [0.25; 0.5; 1.0; 2.0] : Result<float list, string>)
    
[<Test>]
let ``DegreeRow for negative m should return Error`` () =
    degreeRow 2 -1
    |> should equal (Error "m must be non-negative" : Result<float list, string>)