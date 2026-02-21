module CalculateFibb

let calculateFibb num =
    if num < 0 then
        Error "Num must be positive"
    else
        let rec fibb current previous stepsLeft =
            if stepsLeft = 0 then
                current
            else
                fibb (current + previous) current (stepsLeft - 1)
        Ok (fibb 0 1 num)
       