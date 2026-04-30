module DS.demo2

#r "nuget: Deedle"

#r "nuget: System.Drawing.Common"
#r "nuget: FSharp.Charting"
open Deedle

let df = Frame.ReadCsv("/home/markus/source/DS/DS/train.csv")
df.Print()

df?HasCabin <- df.GetColumn<string>("Cabin")
  |> Series.mapAll (fun _ v -> Some(v.IsSome))

let newDb = df.Columns.[ ["Name"; "Pclass"; "HasCabin"] ] |> Frame.take 5
newDb.Print()