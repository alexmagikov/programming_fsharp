module LazyTests.LockFreeTests

open Lazy
open Lazy.ILazy
open Lazy.LockFreeLazy
open LazyTests.LazyTests
open NUnit.Framework

[<Test>]
let ``LockFree: caching`` () = checkCaching lockFreeLazy

[<Test>]
let ``LockFree: side effect`` () = checkSideEffect lockFreeLazy

[<Test>]
let ``LockFree: independence`` () = checkIndependence lockFreeLazy

[<Test>]
let ``LockFree: multi-thread consistency`` () = checkMultiThreadedConsistency lockFreeLazy

[<Test>]
let ``LockFree: multi-thread count (allowed multiple)`` () = checkMultiThreadedCount lockFreeLazy true