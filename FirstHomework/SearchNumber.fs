module SearchNumber

// Return index of searching num.
let searchNum list num =
    let rec search curList index =
        match curList with
         | [] -> None
         | head::tail ->
             if head = num then Some index
             else search tail (index + 1)
    search list 0