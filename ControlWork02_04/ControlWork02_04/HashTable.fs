module ControlWork02_04.HashTable

// HashTable
type HashTable<'T when 'T : equality>(hashFunc: 'T -> int, ?size) =
    let mutable capacity = defaultArg size 16
    let mutable buckets = Array.init capacity (fun _ -> [])
    let mutable count = 0
    
    let resize (self: HashTable<'T>) newCapacity =
        let oldBackets = buckets
        buckets <- Array.init newCapacity (fun _ -> [])
        capacity <- newCapacity
        count <- 0
        oldBackets
        |> Array.iter (fun bucket ->
            bucket |> List.iter self.Add)
    
    let getIndex item = abs (hashFunc item) % capacity
    
    // Add element into HashTable
    member this.Add(item) =
        let idx = getIndex item
        let bucket = buckets[idx]
        if not (List.contains item bucket) then
            buckets[idx] <- item :: bucket
            count <- count + 1
            if float count / float capacity > 0.75 then
                resize this (capacity * 2)
        
    // Return containing of element
    member this.Contains(item) =
        let idx = getIndex item
        List.contains item buckets[idx]
        
    // Remove element from hashtable
    member this.Remove(item) =
        let idx = getIndex item
        let oldBucket = buckets[idx]
        let newBucket = List.filter (fun x -> x <> item) oldBucket
        if List.length oldBucket <> List.length newBucket then
            buckets[idx] <- newBucket
            count <- count - 1