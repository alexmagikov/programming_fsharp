module Lazy.SimpleLazy

open Lazy.ILazy

type SingleThreadedLazy<'a>(supplier: unit ->'a) =
    let mutable value = None
    
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                let result = supplier()
                value <- Some result
                result