module Interpreter.test

open MyInterpreter
open NUnit.Framework
open FsUnit

let eval input =
    match evaluate input with
    | Ok v -> v
    | Error e -> failwith e

let evalResult input = evaluate input

[<Test>]
let ``identity function`` () =
    eval "\\x.x" |> should equal "\\x. x"

[<Test>]
let ``simple application`` () =
    eval "(\\x.x) y" |> should equal "y"

[<Test>]
let ``constant function K`` () =
    eval "(\\x y.x) a b" |> should equal "a"

[<Test>]
let ``nested lambda`` () =
    eval "(\\x.\\y.x) a b" |> should equal "a"

[<Test>]
let ``S K K`` () =
    let input = """
let S = \x y z.x z (y z)
let K = \x y.x
S K K
"""
    eval input |> should equal "\\x. x"

[<Test>]
let ``simple let`` () =
    let input = """
let I = \x.x
I a
"""
    eval input |> should equal "a"

[<Test>]
let ``multiple lets`` () =
    let input = """
let I = \x.x
let K = \x y.x
K (I a) b
"""
    eval input |> should equal "a"

[<Test>]
let ``avoid variable capture`` () =
    let result = eval "(\\x.\\y.x) y"
    result |> should not' (equal "\\y. y")

[<Test>]
let ``parse variable`` () =
    eval "x" |> should equal "x"

[<Test>]
let ``parse parentheses`` () =
    eval "(x)" |> should equal "x"

[<Test>]
let ``parse application chain`` () =
    eval "a b c" |> should equal "a b c"

[<Test>]
let ``invalid syntax`` () =
    match evalResult "\\x x" with
    | Ok _ -> failwith "Expected parsing error"
    | Error _ -> true |> should equal true

[<Test>]
let ``let as variable запрещён`` () =
    match evalResult "\\let. let" with
    | Ok _ -> failwith "Expected parsing error"
    | Error _ -> true |> should equal true

[<Test>]
let ``boolean true`` () =
    eval "(\\t f.t) a b" |> should equal "a"

[<Test>]
let ``boolean false`` () =
    eval "(\\t f.f) a b" |> should equal "b"

[<Test>]
let ``function composition`` () =
    let input = """
let comp = \f g x.f (g x)
let id = \x.x
comp id id a
"""
    eval input |> should equal "a"