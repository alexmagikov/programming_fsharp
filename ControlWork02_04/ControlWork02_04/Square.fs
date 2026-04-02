module ControlWork02_04.Square

// Build square of * with width n.
let buildSquare n =
    if n <= 0 then []
    else
        let rec build acc l =
            match acc with
            | acc when acc = n -> l |> List.rev
            | acc when acc = 0 || acc = n - 1 ->
                build (acc + 1) (String.replicate n "*" :: l)
            | _ ->
                build (acc + 1) ("*" + String.replicate (n - 2) " " + "*" :: l)
        build 0 []

// Print square of * with width n.
let printSquare n =
    buildSquare n |> List.iter (printfn "%s")