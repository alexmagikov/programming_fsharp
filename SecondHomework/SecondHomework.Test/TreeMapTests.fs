module SecondHomework.Test.TreeMapTests

open NUnit.Framework
open FsUnit
open SecondHomework.TreeMap

[<Test>]
let ``treeMap of tree of number with degree should return number`` () =
    treeMap (fun x -> x * x) (Node(5, Empty, Empty)) id
    |> should equal (Node(25, Empty, Empty))
    
[<Test>]
let ``treeMap of empty tree with any fun should return empty tree`` () =
    treeMap (fun x -> x * x) Empty id
    |> should equal (Empty : int binTree)
    
[<Test>]
let ``treeMap of normal tree with any fun should return map normal tree`` () =
    treeMap (fun x -> x * x) (Node(5, Node(5, Empty, Empty), Node(5, Empty, Empty))) id
    |> should equal (Node(25, Node(25, Empty, Empty), Node(25, Empty, Empty)))