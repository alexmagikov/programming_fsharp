module PhoneDirectory

type Entry = {
    Name: string
    Phone: string
}

type PhoneBook = {
    ByName: Map<string, string>
    ByPhone: Map<string, string>
}

let empty : PhoneBook = {
        ByName = Map.empty
        ByPhone = Map.empty
}

let addEntry name phone book = {
        ByName = book.ByName.Add(name, phone)
        ByPhone = book.ByPhone.Add(phone, name)
}
    
let searchPhoneByName name (book: PhoneBook)= 
    Map.tryFind name book.ByPhone
    
let searchNameByPhone phone (book: PhoneBook)=
    Map.tryFind phone book.ByName
    
let getAllEntries book=
    book.ByName |> Map.toList
    
let saveData book path=
    let dataToList = Map.toList book.ByName
    Serialization.saveToFile path dataToList
    
let readData path=
    let listData : (string * string) list = Serialization.readFromFile path
    
    let byName = Map.ofList listData
    let byPhone = listData |> List.map (fun (n, p) -> (p, n)) |> Map.ofList
    
    { ByName = byName; ByPhone = byPhone }