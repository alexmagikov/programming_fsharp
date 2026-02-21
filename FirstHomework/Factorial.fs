module Factorial

let factorial num =
    if num < 0 then
        Error "Num must be positive"
    else
        let rec mul n acc =
            if n = 0I then acc
            else mul (n - 1I) (acc * n)
        Ok (mul (bigint num) 1I)