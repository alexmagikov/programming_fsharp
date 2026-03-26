module LocalNetwork.Test

open LocalNetwok
open LocalNetwok.IRandomGenerator
open NUnit.Framework

type ConstantGenerator(value : float) =
    interface IRandomGenerator with
        member this.NextDouble() = value
        
type SequenceRandomGenerator(values : float list) =
    let mutable index = 0
    interface IRandomGenerator with
        member this.NextDouble() =
            let v = values[index]
            index <- (index + 1) % values.Length
            v

[<Test>]
let ``BFS virus with probability 1`` () =
    let adjacency = array2D [
        [false; true;  false; false];
        [true;  false; true;  false];
        [false; true;  false; true ];
        [false; false; true;  false]
    ]
    
     let computers = [|
        { Id = 0; OS = Windows; IsInfected = true }
        { Id = 1; OS = Windows; IsInfected = false }
        { Id = 2; OS = Windows; IsInfected = false }
        { Id = 3; OS = Windows; IsInfected = false }
    |]
     
    let network = new Network(adjacency, computers)
    let infectionProb = Map.ofList [(Windows, 1.0); (Linux, 1.0); (MacOS, 1.0)]