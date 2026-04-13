module LocalNetwok.Network

open LocalNetwok.Computer
open LocalNetwok.IRandomGenerator

type Network(adjacencyMatrix : bool[,], computers : Computer[], random : IRandomGenerator) =
    member v.Computers = computers
    member v.AdjacencyMatrix = adjacencyMatrix
    
    member v.PrintState(step : int) =
        printfn $"Step %d{step}"
        computers |> Array.iter (fun c -> printfn $"{c}")
        printfn ""
        
    member v.CanChange() =
        let n = computers.Length
        seq {
            for i in 0 .. n - 1 do
                if computers[i].IsInfected then
                    for j in 0 .. n - 1 do
                        if adjacencyMatrix[i, j] &&
                           not computers[j].IsInfected &&
                           computers[j].OS.VirusProbab > 0.0 then
                            yield ()
        }
        |> Seq.isEmpty
        |> not
        
    member v.Step() =
         let n = computers.Length
         
         let infectedNow =
             computers
             |> Array.mapi (fun i c -> i, c.IsInfected)
             |> Array.choose (fun (i, inf) -> if inf then Some i else None)
             
         let toInfect = ResizeArray<int>()
         
         for infectedIndex in infectedNow do
             for j in 0 .. n - 1 do
                 if adjacencyMatrix[infectedIndex, j] && not computers[j].IsInfected then
                     let probab = computers[j].OS.VirusProbab
                     let value = random.NextDouble()
                     if value < probab then
                         toInfect.Add(j)
                      
         let uniqueToInfect = toInfect
                              |> Seq.distinct
                              |> Seq.toArray
         
         for i in uniqueToInfect do
             computers[i].IsInfected <- true
             
         uniqueToInfect.Length > 0
         
         
    member v.Run() =
         let mutable step = 0
         v.PrintState(step)
         
         while v.CanChange() do
             step <- step + 1
             let _ = v.Step()
             v.PrintState(step)
         
         
                   
    
    