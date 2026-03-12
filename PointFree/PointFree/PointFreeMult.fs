module PointFree.PointFreeMult

open FsCheck

let func x l = List.map (fun y -> x * y) l

let func'1 x = List.map (fun y -> x * y)
    
let func'2 x = List.map (fun y -> (*) x y)
 
let func'3 x = List.map ((*) x)

let func'4 = List.map << (*)

let properties x (l: int list) =
    func x l = func'4 x l

Check.Quick properties