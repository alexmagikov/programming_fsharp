module Program

open PhoneDirectory
open System
open System.IO

let printResult result prefix =
    match result with
    | Some value -> printfn $"%s{prefix}: %s{value}"
    | None -> printfn "Entry is not found"

let rec mainLoop (book: PhoneBook) =
    printfn "\n--- PhoneDirectory ---"
    printfn "1. add empty"
    printfn "2. find phone by name"
    printfn "3. find name by phone"
    printfn "4. show all empties"
    printfn "5. save"
    printfn "6. load"
    printfn "0. exit"
    printf "input: "

    match Console.ReadLine() with
    | "1" ->
        printf "Name: "
        let name = Console.ReadLine()
        printf "Phone: "
        let phone = Console.ReadLine()
        let newBook = addEntry name phone book
        printfn "ОК."
        mainLoop newBook

    | "2" ->
        printf "Name: "
        let name = Console.ReadLine()
        let res = searchPhoneByName name book
        printResult res "Phone"
        mainLoop book

    | "3" ->
        printf "Phone: "
        let phone = Console.ReadLine()
        let res = searchNameByPhone phone book
        printResult res "Name"
        mainLoop book

    | "4" ->
        let entries = getAllEntries book
        if List.isEmpty entries then printfn "Clean."
        else
            entries |> List.iter (fun (n, p) -> printfn $"%s{n} -> %s{p}")
        mainLoop book

    | "5" ->
        printf "Path to the file: "
        let path = Console.ReadLine()
        try
            saveData book path
            printfn "Saved."
        with ex -> printfn $"Error: %s{ex.Message}"
        mainLoop book

    | "6" ->
        printf "Path to the file: "
        let path = Console.ReadLine()
        try
            let loadedBook = readData path
            printfn "Saved."
            mainLoop loadedBook
        with ex -> printfn $"Error: %s{ex.Message}"
        mainLoop book

    | "0" -> ()
    | _ -> mainLoop book

[<EntryPoint>]
let main argv =
    mainLoop empty
    0