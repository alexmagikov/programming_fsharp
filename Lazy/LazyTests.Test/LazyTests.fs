module LazyTests.LazyTests

open System
open System.Threading
open FsUnit
open Lazy.ILazy
open Lazy.LockFreeLazy
open Lazy.MultiThreadedLazy
open Lazy.SimpleLazy
open NUnit.Framework

type ReferenceValue = { Id: Guid }

let checkCaching (create: (unit -> Guid) -> #ILazy<Guid>) =
    let mutable counter = 0
    let lazyVal =
        create (fun () ->
            Interlocked.Increment(&counter) |> ignore
            Guid.NewGuid())

    let firstRes = lazyVal.Get()
    lazyVal.Get() |> should equal firstRes
    counter |> should equal 1

let checkReferenceCaching (create: (unit -> ReferenceValue) -> #ILazy<ReferenceValue>) =
    let mutable counter = 0
    let lazyVal =
        create (fun () ->
            Interlocked.Increment(&counter) |> ignore
            { Id = Guid.NewGuid() })

    let first = lazyVal.Get()
    let second = lazyVal.Get()

    Assert.That(second, Is.SameAs(first))
    counter |> should equal 1
 
type MutableStruct =
    struct
        val public Value: int
        new(v) = { Value = v }
    end
 
let checkStructCaching (create: (unit -> MutableStruct) -> #ILazy<MutableStruct>) =
    let mutable counter = 0
    let lazyVal =
        create (fun () ->
            Interlocked.Increment(&counter) |> ignore
            MutableStruct(counter))

    let first = lazyVal.Get()
    let second = lazyVal.Get()

    second.Value |> should equal 1
    counter |> should equal 1  


let checkMultiThreadedConsistency (create: (unit -> Guid) -> #ILazy<Guid>) =
    let lazyVal = create (fun () -> Guid.NewGuid())
    let nThreads = 16
    let startGate = new ManualResetEventSlim(false)
    let results = Array.zeroCreate nThreads

    let threads =
        Array.init nThreads (fun i ->
            Thread(fun () ->
                startGate.Wait()
                results[i] <- lazyVal.Get()))

    threads |> Array.iter (fun t -> t.Start())
    startGate.Set()
    threads |> Array.iter (fun t -> t.Join())
    let firstResult = results[0]
    results |> Array.iter (fun result -> result |> should equal firstResult)

let checkMultiThreadedCountForMultiThreads (create: (unit -> int) -> #ILazy<int>) =
    let mutable callCount = 0
    let lazyVal =
        create (fun () ->
            Thread.Sleep(10)
            Interlocked.Increment(&callCount))

    let nThreads = 32
    let startGate = new ManualResetEventSlim(false)

    let threads =
        Array.init nThreads (fun _ ->
            Thread(fun () ->
                startGate.Wait()
                lazyVal.Get() |> ignore))

    threads |> Array.iter (fun t -> t.Start())
    startGate.Set()
    threads |> Array.iter (fun t -> t.Join())

    Assert.That(callCount, Is.GreaterThanOrEqualTo(1))
    Assert.That(callCount, Is.LessThanOrEqualTo(nThreads))

let checkMultiThreadedCountFor1Thread (create: (unit -> int) -> #ILazy<int>) =
    let mutable callCount = 0
    let lazyVal =
        create (fun () ->
            Thread.Sleep(10)
            Interlocked.Increment(&callCount))

    let nThreads = 16
    let startGate = new ManualResetEventSlim(false)

    let threads =
        Array.init nThreads (fun _ ->
            Thread(fun () ->
                startGate.Wait()
                lazyVal.Get() |> ignore))

    threads |> Array.iter (fun t -> t.Start())
    startGate.Set()
    threads |> Array.iter (fun t -> t.Join())

    callCount |> should equal 1