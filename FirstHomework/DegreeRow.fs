module DegreeRow

// Calculate the list with format: [2^n; 2^(n+1); ...; 2^(n+m)].
let degreeRow (n : int) (m : int)=
    let rec loop current_list current_element index =
        match index with
        | i when i > m -> current_list
        | _ -> loop (current_element::current_list) (current_element >>> 1) (index + 1)
    
    match m with
    | num when num < 0 -> []
    | _ -> loop [] (1 <<< (n + m)) 0