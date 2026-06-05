module LazyTests.LockFreeTests

open Lazy
open Lazy.ILazy
open Lazy.LockFreeLazy
open LazyTests.LazyTests
open NUnit.Framework

[<Test>]
let ``LockFree: caching`` () = checkCaching LockFreeLazy

[<Test>]
let ``LockFree: reference caching`` () = checkReferenceCaching LockFreeLazy

[<Test>]
let ``LockFree: multi-thread consistency`` () = checkMultiThreadedConsistency LockFreeLazy

[<Test>]
let ``LockFree: multi-thread count (allowed multiple)`` () = checkMultiThreadedCountForMultiThreads LockFreeLazy
