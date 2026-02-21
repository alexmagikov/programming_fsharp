module DegreeRow

// Calculate the list with format: [2^n; 2^(n+1); ...; 2^(n+m)].
let degreeRow (n : uint32) (m : uint32)=
    let rec loop current_list current_element index =
        if index > m then current_list
        else loop (current_element::current_list) (current_element >>> 1) (index + 1u)
    loop [] (1u <<< int (n + m)) 0u