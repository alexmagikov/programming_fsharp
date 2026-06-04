module LazyTests.SimpleLazyTests

open Lazy.SimpleLazy
open LazyTests.LazyTests
open NUnit.Framework


[<Test>]
let ``Simple: caching`` () = checkCaching SingleThreadedLazy

[<Test>]
let ``Simple: reference caching`` () = checkReferenceCaching SingleThreadedLazy
