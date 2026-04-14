module LazyTests.MultiThreadedTests

open Lazy.MultiThreadedLazy
open LazyTests.LazyTests
open NUnit.Framework

[<Test>]
let ``ThreadSafe: caching`` () = checkCaching multiThreadedLazy

[<Test>]
let ``ThreadSafe: side effect`` () = checkSideEffect multiThreadedLazy

[<Test>]
let ``ThreadSafe: independence`` () = checkIndependence multiThreadedLazy

[<Test>]
let ``ThreadSafe: multi-thread consistency`` () = checkMultiThreadedConsistency multiThreadedLazy

[<Test>]
let ``ThreadSafe: multi-thread count`` () = checkMultiThreadedCount multiThreadedLazy false