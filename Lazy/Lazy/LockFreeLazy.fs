module Lazy.LockFreeLazy

open System.Threading
open Lazy.ILazy

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable value: 'a option = None
    
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                let result = supplier()
                let newValue = Some result
                let original = Interlocked.CompareExchange(&value, newValue, None)
                match original with
                | Some v -> v
                | None -> result
                    
 let lockFreeLazy (supplier: unit -> 'a) : ILazy<'a> =
    LockFreeLazy<'a>(supplier) :> ILazy<'a>