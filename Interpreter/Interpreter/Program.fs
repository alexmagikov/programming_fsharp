module Interpreter.Program

open System.IO
open MyInterpreter

[<EntryPoint>]
let main argv =
    let source =
        if argv.Length > 0 && File.Exists(argv[0]) then
            File.ReadAllText(argv[0])
        else
            printfn "Example..."
            """
            let S = \x y z.x z (y z)
            let K = \x y.x

            S K K
            """

    printfn $"\n--- Source code ---\n%s{source}\n---------------------\n"

    match Parser.run Parser.pProgram source with
    | Ok (_, result) ->
        printfn $"Result: %s{termToString result}"
        0
    | Error msg ->
        eprintfn $"error: %s{msg}"
        1
