module LazyTests.MultiThreadedTests

open Lazy.MultiThreadedLazy
open LazyTests.LazyTests
open NUnit.Framework

[<Test>]
let ``ThreadSafe: caching`` () = checkCaching MultiThreadedLazy

[<Test>]
let ``ThreadSafe: reference caching`` () = checkReferenceCaching MultiThreadedLazy

[<Test>]
let ``ThreadSafe: multi-thread consistency`` () = checkMultiThreadedConsistency MultiThreadedLazy

[<Test>]
let ``ThreadSafe: multi-thread count`` () = checkMultiThreadedCountFor1Thread MultiThreadedLazy
