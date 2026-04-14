module LazyTests.LazyTests

open System.Threading
open FsUnit
open Lazy.ILazy
open NUnit.Framework

// Check cashing
let checkCaching (create: (unit -> int) -> ILazy<int>) =
    let mutable counter = 0
    let lazyVal = create (fun () -> counter <- counter + 1; 42)
    lazyVal.Get() |> should equal 42
    lazyVal.Get() |> should equal 42
    counter |> should equal 1

// Check side effects for create
let checkSideEffect (create: (unit -> string) -> ILazy<string>) =
    let mutable side = 0
    let lazyVal = create (fun () -> side <- side + 1; "hello")
    lazyVal.Get() |> should equal "hello"
    lazyVal.Get() |> should equal "hello"
    side |> should equal 1

// Check independence for int
let checkIndependence (create: (unit -> int) -> ILazy<int>) =
    let lazy1 = create (fun () -> 1)
    let lazy2 = create (fun () -> 2)
    lazy1.Get() |> should equal 1
    lazy2.Get() |> should equal 2

//  MultiThreaded checking
let checkMultiThreadedConsistency (create: (unit -> 'a) -> ILazy<'a>) =
    let rnd = System.Random()
    let lazyVal = create (fun () -> rnd.Next())
    let nThreads = 10
    let results = Array.zeroCreate nThreads
    let threads = Array.init nThreads (fun i -> Thread(fun () -> results[i] <- lazyVal.Get()))
    threads |> Array.iter (fun t -> t.Start())
    threads |> Array.iter (fun t -> t.Join())
    let first = results[0]
    results |> Array.iter (fun r -> r |> should equal first)

let checkMultiThreadedCount (create: (unit -> int) -> ILazy<int>) allowMultiple =
    let mutable callCount = 0
    let lazyVal = create (fun () -> Interlocked.Increment(&callCount) |> ignore; 100)
    let nThreads = 10
    let threads = Array.init nThreads (fun _ -> Thread(fun () -> lazyVal.Get() |> ignore))
    threads |> Array.iter (fun t -> t.Start())
    threads |> Array.iter (fun t -> t.Join())
    if allowMultiple then
        Assert.That(callCount, Is.GreaterThanOrEqualTo(1))
    else
        callCount |> should equal 1