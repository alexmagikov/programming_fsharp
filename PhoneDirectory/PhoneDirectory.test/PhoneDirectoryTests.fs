module PhoneDirectory.test

open NUnit.Framework
open FsUnit

[<Test>]
let ``New book should have empty maps`` () =
    let empty : PhoneBook = {
        ByName = Map.empty
        ByPhone = Map.empty
    }
    empty.ByName |> should be Empty
    empty.ByPhone |> should be Empty

[<Test>]
let ``Adding entry updates both maps`` () =
    let empty : PhoneBook = {
        ByName = Map.empty
        ByPhone = Map.empty
    }
    
    let book = empty |> addEntry "Name" "123"
    Map.containsKey "Name" book.ByName |> should be True
    book.ByName.["Name"] |> should equal "123"
    
    Map.containsKey "123" book.ByPhone |> should be True
    book.ByPhone.["123"] |> should equal "Name"
  
[<Test>]
let ``Fast search works both ways`` () =
    let empty : PhoneBook = {
        ByName = Map.empty
        ByPhone = Map.empty
    }
    
    let book = 
        empty 
        |> addEntry "Alice" "111" 
        |> addEntry "Bob" "222"

    searchPhoneByName "Alice" book |> should equal (Some "111")
    searchNameByPhone "222" book |> should equal (Some "Bob")
    
    searchPhoneByName "Charlie" book |> should equal None
    

[<Test>]
let ``Overwriting updates both maps correctly`` () =
    let empty : PhoneBook = {
        ByName = Map.empty
        ByPhone = Map.empty
    }
    
    let book = 
        empty 
        |> addEntry "OldName" "555"
        |> addEntry "NewName" "555"

    searchNameByPhone "555" book |> should equal (Some "NewName")