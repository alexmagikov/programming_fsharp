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
    let expected = Guid.NewGuid()
    let lazyVal =
        create (fun () ->
            Interlocked.Increment(&counter) |> ignore
            expected)

    lazyVal.Get() |> should equal expected
    lazyVal.Get() |> should equal expected
    counter |> should equal 1

let checkReferenceCaching (create: (unit -> ReferenceValue) -> #ILazy<ReferenceValue>) =
    let mutable counter = 0
    let expected = { Id = Guid.NewGuid() }
    let lazyVal =
        create (fun () ->
            Interlocked.Increment(&counter) |> ignore
            expected)

    let first = lazyVal.Get()
    let second = lazyVal.Get()

    Assert.That(first, Is.SameAs(expected))
    Assert.That(second, Is.SameAs(expected))
    Assert.That(second, Is.SameAs(first))
    counter |> should equal 1

let checkMultiThreadedConsistency (create: (unit -> Guid) -> #ILazy<Guid>) =
    let expected = Guid.NewGuid()
    let lazyVal = create (fun () -> expected)
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
    results |> Array.iter (fun result -> result |> should equal expected)

let checkMultiThreadedCount (create: (unit -> int) -> #ILazy<int>) allowMultiple =
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

    if allowMultiple then
        Assert.That(callCount, Is.GreaterThanOrEqualTo(1))
        Assert.That(callCount, Is.LessThanOrEqualTo(nThreads))
    else
        callCount |> should equal 1
