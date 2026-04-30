module DS.demo3

#r "nuget: Deedle"
#r "nuget: System.Drawing.Common"
#r "nuget: FSharp.Charting"
open FSharp.Charting
open Deedle

let df = Frame.ReadCsv("/home/markus/source/DS/DS/train.csv")

let byClassAndSex = 
  df
  |> Frame.groupRowsByInt "Pclass"
  |> Frame.groupRowsByString "Sex"
  |> Frame.mapRowKeys Pair.flatten3

let finalStats = byClassAndSex.GetColumn<bool>("Survived")
               |> Series.applyLevel Pair.get1And2Of3 (fun s ->
                series (Seq.countBy id s.Values))
               |> Frame.ofRows

finalStats.Print()

let rows = finalStats.Rows
rows.Print()

let survivedData =
    finalStats.Rows
    |> Series.map (fun (pclass, sex) row ->
        let survived =
            if row.TryGetAs<int>(true).HasValue then
                row.GetAs<int>(true)
            else 0

        sprintf "Class %A - %A" pclass sex, survived
    )
    |> Series.values
    |> Seq.toList

printf "%A" survivedData

let diedData =
    finalStats.Rows
    |> Series.map (fun (pclass, sex) row ->

        let died =
            if row.TryGetAs<int>(false).HasValue then
                row.GetAs<int>(false)
            else 0

        sprintf "Class %A - %A" pclass sex, died
    )
    |> Series.values
    |> Seq.toList

[
    Chart.Column(survivedData, Name = "Выжили")
    Chart.Column(diedData, Name = "Не выжили")
]
|> Chart.Combine
|> Chart.WithTitle "Выживаемость по классу и полу"
|> Chart.WithXAxis(Title = "Pclass + Sex", Angle = -45)
|> Chart.WithYAxis(Title = "Количество пассажиров")
|> Chart.Show