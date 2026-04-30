module deedle

#r "nuget: Deedle"
#r "nuget: System.Drawing.Common"
#r "nuget: FSharp.Charting"
open Deedle

let df = Frame.ReadCsv("/home/markus/source/DS/DS/train.csv")
df.Print()

df.RowCount

df?Age |> Stats.mean

// keep rows matching to condition
let survived = df |> Frame.filterRowValues (fun row ->
  row.GetAs<bool>("Survived"))

survived.RowCount

let newDf = df |> Frame.aggregateRowsBy ["Pclass"] ["Age"] Stats.mean
newDf.Print()