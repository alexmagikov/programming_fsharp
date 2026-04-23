module CalculateFibb

let calculateFibb num =
    match num with
    | n when n < 0 -> Error "Num must be positive"
    | _ -> 
        let rec fibb = function
            | 0 -> Ok 0
            | 1 -> Ok 1
            | n ->
                match fibb (n - 1), fibb ( n - 2) with
                | Ok a, Ok b -> Ok (a + b)
                | Error e, _ -> Error e
                | _, Error e -> Error e
        fibb num
       