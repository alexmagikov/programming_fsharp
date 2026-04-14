module Lazy.MultiThreadedLazy

open Lazy.ILazy

type MultiThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable value: 'a option = None
    let lockObj = obj()
    
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                lock lockObj (fun () ->
                    match value with
                    | Some v -> v
                    | None ->
                        let result = supplier()
                        value <- Some result
                        result)
                
let multiThreadedLazy (supplier: unit -> 'a) : ILazy<'a> =
    MultiThreadedLazy<'a>(supplier) :> ILazy<'a>