module DegreeRow

// Calculate the list with format: [2^n; 2^(n+1); ...; 2^(n+m)].
let degreeRow (n : int) (m : int)=
    match m with
    | num when num < 0 -> Error "m must be non-negative"
    | _ ->
        let rec loop rem currentElement currentList =
            match rem with
            | 0 -> Ok (List.rev currentList)
            | _ -> loop (rem - 1) (currentElement * 2.0) (currentElement :: currentList)    
        loop (m + 1) (2.0 ** float n) []