module LocalNetwok.Computer

open LocalNetwok.IOS

type Computer(id: int, os: IOS, initiallyVirus: bool) =
    let mutable infected = initiallyVirus
    
    member v.Id = id
    member v.OS = os
    member v.IsInfected
        with get() = infected
        and set value = infected <- value
        
    override v.ToString() =
        let state = if infected then "Infected" else "Healthy"
        $"Computer %d{id} | OS = %s{os.Name} | %s{state}"
