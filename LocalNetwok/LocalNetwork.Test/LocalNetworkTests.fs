module LocalNetwork.Test

open LocalNetwok.Computer
open LocalNetwok.IOS
open LocalNetwok.IRandomGenerator
open LocalNetwok.Network
open NUnit.Framework

type TestOS(prob : float) =
    interface IOS with
        member v.Name = "TestOS"
        member v.VirusProbab = prob

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
    
    let os1 = TestOS(1.0) :> IOS
     
    let computers = [|
       Computer(0, os1, true)
       Computer(1, os1, false)
       Computer(2, os1, false)
       Computer(3, os1, false)
    |]
     
    let rng = ConstantGenerator(0.0) :> IRandomGenerator
    let network = Network(adjacency, computers, rng)
    
    let getInfectedIds (n : Network) =
        n.Computers
        |> Array.filter (fun c -> c.IsInfected)
        |> Array.map (fun c -> c.Id)
        |> Set.ofArray
        
    let expectedStates = [
        set [0]
        set [0; 1]
        set [0; 1; 2]
        set [0; 1; 2; 3]
    ]
    
    Assert.AreEqual(expectedStates[0], getInfectedIds network, "Init state incorrect")
    
    let mutable stepCount = 0
    while network.Step() do
        stepCount <- stepCount + 1
        Assert.AreEqual(expectedStates[stepCount], getInfectedIds network,
            sprintf "State is not for BFS" stepCount)

    Assert.AreEqual(3, stepCount, "Num steps didnt match with expected")
    
[<Test>]
let ``There are no new infect with probability 0`` () =
    let adjacency = array2D [
        [false; true; false];
        [true;  false; true];
        [false; true; false]
    ]
    
    let os0 = TestOS(0.0) :> IOS
    let computers = [|
        Computer(0, os0, true)
        Computer(1, os0, false)
        Computer(2, os0, false)
    |]
    
    let rng = ConstantGenerator(1.0) :> IRandomGenerator
    let network = Network(adjacency, computers, rng)

    let initialInfected = network.Computers
                          |> Array.filter (fun c -> c.IsInfected)
                          |> Array.map (fun c -> c.Id)
                          |> Set.ofArray
    let changed = network.Step()
    let afterInfected = network.Computers
                        |> Array.filter (fun c -> c.IsInfected)
                        |> Array.map (fun c -> c.Id)
                        |> Set.ofArray
                        
    Assert.AreEqual(initialInfected, afterInfected, "Set of inf shouldnt be changed")

[<Test>]
let ``Normal variant with probs`` () =
    let adjacency = array2D [
        [false; true];
        [true;  false]
    ]

    let os05 = TestOS(0.5) :> IOS
    let computers = [|
        Computer(0, os05, true)
        Computer(1, os05, false)
    |]

    let rng1 = ConstantGenerator(0.3) :> IRandomGenerator
    let network1 = Network(adjacency, computers, rng1)
    let changed1 = network1.Step()
    Assert.IsTrue(changed1, "Inf should be")
    let infected1 = network1.Computers |>
                    Array.filter (fun c -> c.IsInfected)
                    |> Array.map (fun c -> c.Id)
                    |> Set.ofArray
    Assert.AreEqual(set [0; 1], infected1, "Both comp should be inf")

    let rng2 = ConstantGenerator(0.7) :> IRandomGenerator
    let network2 = Network(adjacency, computers, rng2)
    let changed2 = network2.Step()
    Assert.IsFalse(changed2, "There are no inf with prob 0.7")
    let infected2 = network2.Computers
                    |> Array.filter (fun c -> c.IsInfected)
                    |> Array.map (fun c -> c.Id)
                    |> Set.ofArray
    Assert.AreEqual(set [0], infected2, "Inf should be first comp")