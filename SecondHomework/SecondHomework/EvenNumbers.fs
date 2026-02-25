module SecondHomework.EvenNumbers

let countEvenNumbersByMap list =
    List.map (fun x ->
        if x % 2 = 0 then 1
        else 0
        ) list
    |> List.sum
    
let countEvenNumbersByFilter list =
    List.filter (fun x -> x % 2 = 0) list
    |> List.length
    
let countEvenNumbersByFold list =
    List.fold (fun acc x ->
        if x % 2 = 0 then acc + 1
        else acc
        ) 0 list 