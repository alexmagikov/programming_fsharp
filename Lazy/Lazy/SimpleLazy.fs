module Lazy.SimpleLazy

open Lazy.ILazy

type SingleThreadedLazy<'a>(supplier: unit ->'a) =
    let mutable value: 'a option = None
    
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                let result = supplier()
                value <- Some result
                result
                
let singleThreadedLazy (supplier: unit -> 'a) : ILazy<'a> =
    SingleThreadedLazy<'a>(supplier) :> ILazy<'a>