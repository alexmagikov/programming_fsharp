module LazyTests.SimpleLazyTests

open Lazy.SimpleLazy
open LazyTests.LazyTests
open NUnit.Framework


[<Test>]
let ``Simple: caching`` () = checkCaching singleThreadedLazy

[<Test>]
let ``Simple: side effect`` () = checkSideEffect singleThreadedLazy

[<Test>]
let ``Simple: independence`` () = checkIndependence singleThreadedLazy