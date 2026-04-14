module Lazy.LockFreeLazy

type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let value = ref None
    
    interface ILazy<'a> with
        member this.Get() =
            match System.Threading.Volatile.Read(ref !value) with
            | Some v -> v
            | None ->
                let result = supplier()
                let newValue = Some result
                System.Threading.Interlocked.CompareExchange(value, newValue, None)
                |> function
                    | Some v -> v
                    | None -> result
                    
 