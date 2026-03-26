module LocalNetwok.SystemRandom

open LocalNetwok.IRandomGenerator

type SystemRandom() =
    let rng = System.Random()
    interface IRandomGenerator with
        member this.NextDouble() = rng.NextDouble()