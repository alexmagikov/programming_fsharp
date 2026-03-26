module LocalNetwok.OS

open LocalNetwok.IOS

type Windows() =
    interface IOS with
        member this.Name = "Windows"
        member this.VirusProbab = 0.6
       
type Linux() =
    interface IOS with
        member this.Name = "Linux"
        member this.VirusProbab = 0.3

type MacOS() =
    interface IOS with
        member this.Name = "MacOS"
        member this.VirusProbab = 0.4