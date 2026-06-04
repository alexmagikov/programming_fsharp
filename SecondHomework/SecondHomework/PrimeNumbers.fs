module PrimeNumbers

let isPrime n =
    match n with
    | x when x < 2 -> false
    | 2 -> true
    | x when x % 2 = 0 -> false
    | _ ->
        let limit = int (sqrt (float n))
        
        seq { 3 .. 2 .. limit }
        |> Seq.exists (fun d -> n % d = 0)
        |> not

let generatePrimeNumbers =
    Seq.initInfinite id
    |> Seq.map ((+) 2)
    |> Seq.filter isPrime