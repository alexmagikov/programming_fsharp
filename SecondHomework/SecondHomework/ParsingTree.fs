module SecondHomework.ParsingTree

type ParsingTree =
    | Num of int
    | Add of ParsingTree * ParsingTree
    | Sub of ParsingTree * ParsingTree
    | Mul of ParsingTree * ParsingTree
    | Div of ParsingTree * ParsingTree
    
let rec eval (p: ParsingTree) cont=
    match p with
     | Num value -> cont value
     | Add (left, right) -> eval left (fun left ->
         eval right (fun right ->
             cont (left + right)))
     | Sub (left, right) -> eval left (fun left ->
         eval right (fun right ->
             cont (left - right)))
     | Mul (left, right) -> eval left (fun left ->
         eval right (fun right ->
             cont (left * right)))
     | Div (left, right) -> eval left (fun left ->
         eval right (fun right ->
             cont (left / right)))
   