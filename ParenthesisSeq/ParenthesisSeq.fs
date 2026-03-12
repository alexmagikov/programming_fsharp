module ParenthesisSeq.ParenthesisSeq

let isBalanced s =
    let bracketMap =
        Map.ofList [
            (')', '(')
            (']', '[')
            ('}', '{')
        ]

    let openBrackets =
        bracketMap
        |> Map.values
        |> Set.ofSeq
    
    let rec loop chars stack =
        match chars with
        | [] -> List.isEmpty stack
        | c::tail ->
            if Set.contains c openBrackets then
                loop tail (c::stack)
            elif Map.containsKey c bracketMap then
                match stack with
                | top :: stackTail when top = bracketMap[c] ->
                    loop tail stackTail
                | _ -> false
            else
                loop tail stack
    loop (Seq.toList s) []