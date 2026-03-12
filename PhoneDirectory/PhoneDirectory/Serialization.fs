module Serialization

open System.IO
open System.Runtime.Serialization.Formatters.Binary

let writeValue outputStream (x: 'a) =
    let formatter = new BinaryFormatter()
    formatter.Serialize(outputStream, box x)
    
let readValue inputStream=
    let formatter = new BinaryFormatter()
    let res = formatter.Deserialize(inputStream)
    unbox res
    
let saveToFile path data =
    use fsOut = new FileStream(path, FileMode.Create)
    writeValue fsOut data
    
let readFromFile path =
    use fsIn = new FileStream(path, FileMode.Open)
    readValue fsIn
    