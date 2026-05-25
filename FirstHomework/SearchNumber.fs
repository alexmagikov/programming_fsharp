module SearchNumber

// Return index of searching num.
let searchNum list num =
    let rec search curList index =
        match curList with
         | [] -> None
         | head::_ when head = num -> Some index
         | _::tail -> search tail (index + 1)
    search list 0