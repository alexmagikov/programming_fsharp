module ControlWork.test.HashTableTests

open ControlWork02_04.HashTable
open NUnit.Framework
open FsUnit

let intHash (x: int) = x
let stringHash (x: string) = x.Length

[<Test>]
let ``Add and contains should work for ints`` () =
    let table = HashTable<int>(intHash, 4)
    table.Add(10)
    table.Add(20)
    table.Contains(10) |> should be True
    table.Contains(20) |> should be True
    table.Contains(30) |> should be False
    
[<Test>]
let ``Remove should delete existing elements`` () =
    let table = HashTable<int>(intHash, 4)
    table.Add(10)
    table.Contains(10) |> should be True
    table.Remove(10)
    table.Contains(10) |> should be False
    
[<Test>]
let ``Add duplicate element should not add twice`` () =
    let table = HashTable<int>(intHash, 4)
    table.Add(5)
    table.Add(5)
    table.Remove(5)
    table.Contains(5) |> should be False