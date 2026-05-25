module Factorial

let factorial num =
    match num with
    | n when n < 0 -> Error "Num must be positive"
    | _ -> 
        let rec factorialHelper n acc =
            if n = 0I then acc
            else factorialHelper (n - 1I) (acc * n)
        Ok (factorialHelper (bigint num) 1I)