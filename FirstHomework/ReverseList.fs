module ReverseList

let reverseList list =
   let rec reverse curList acc =
       match curList with
        | [] -> acc
        | head::tail ->
            reverse tail (head::acc)
   reverse list []
      
    
