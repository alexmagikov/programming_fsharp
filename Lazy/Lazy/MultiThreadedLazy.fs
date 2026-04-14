module Lazy.MultiThreadedLazy

type MultiThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutalbe value: 'a option = None
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