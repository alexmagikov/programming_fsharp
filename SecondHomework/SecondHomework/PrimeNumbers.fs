module PrimeNumbers

let isPrime n =
    if n < 2 then false
    elif n = 2 then true
    elif n % 2 = 0 then false
    else
        let limit = int (sqrt (float n))
        let rec check d =
            if d > limit then true
            elif n % d = 0 then false
            else check (d + 2)
        check 3

let generatePrimeNumbers =
    let rec loop n = seq {
        if isPrime n then
            yield n
        yield! loop (n + 1)
    }
    
    loop 2