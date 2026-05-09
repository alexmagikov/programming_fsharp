module CalculateFibb

let calculateFibb num =
    match num with
    | n when n < 0 -> Error "Num must be positive"
    | _ -> 
        let rec loop index prev current =
            match index with
            | i when i = num -> Ok prev
            | _ -> loop (index + 1) current (prev + current)
        loop 0 0 1