module ControlWork02_04.MinElement

// Find min in list.
let findMinEl l =
    match l with
    | [] -> None
    | _ -> Some (List.reduce (fun x y -> if x < y then x else y) l)
