module Lazy.LockFreeLazy

open System.Threading
open Lazy.ILazy

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    
    interface ILazy<'a> with
        member this.Get() =
            match Volatile.Read(&value) with
            | Some v -> v
            | None ->
                let result = supplier()
                let newValue = Some result
                let original = Interlocked.CompareExchange(&value, newValue, None)
                match original with
                | Some v -> v
                | None -> result