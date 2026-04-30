module MyInterpreter

open System
open FParsec

type Term =
    | Var of string
    | Lambda of string list * Term
    | Apply of Term * Term
    
let rec termToString term =
    match term with
    | Var s -> s
    | Lambda (args, body) ->
        sprintf "\\%s. %s" (String.concat " " args) (termToString body)
    | Apply (t1, t2) ->
        let s1 =
            match t1 with
            | Lambda _ -> $"({termToString t1})"
            | _ -> termToString t1
        let s2 =
            match t2 with
            | Apply _ | Lambda _ -> $"({termToString t2})"
            | _ -> termToString t2
        $"{s1} {s2}"

module Parser =
    let ws = spaces

    let pSymbol str = pstring str .>> ws

    let pKeyword str =
        pstring str
        .>> notFollowedBy (satisfy (fun c -> Char.IsLetterOrDigit c || c = '_'))
        .>> ws

    let pIdentifier =
        let isFirst c = Char.IsLetter c || c = '_'
        let isNext c = Char.IsLetterOrDigit c || c = '_'
        pipe2 (satisfy isFirst) (manyChars (satisfy isNext))
            (fun c cs -> string c + cs)

    let pVariable : Parser<string, unit> =
        pIdentifier >>= fun id ->
            if id = "let" then fail "keyword 'let' cannot be a variable"
            else preturn id

    let pApplication, pApplicationRef = createParserForwardedToRef<Term, unit>()
    let pAtom, pAtomRef = createParserForwardedToRef<Term, unit>()

    do
        let pVarTerm = pVariable |>> Var

        let pLambda =
            pipe3
                (pSymbol "\\")
                (many1 (pVariable .>> ws))
                (pSymbol "." >>. pApplication)
                (fun _ args body -> Lambda(args, body))

        let pParen = between (pSymbol "(") (pSymbol ")") pApplication

        pAtomRef.Value <- pVarTerm <|> pLambda <|> pParen

    do
        let appOp = ws >>. preturn (fun t1 t2 -> Apply(t1, t2))
        pApplicationRef.Value <- chainl1 pAtom appOp

    let pDefinition =
        pipe3
            (pKeyword "let")
            pVariable
            (pSymbol "=" >>. pApplication)
            (fun _ name term -> (name, term))

    let pLineEnd = (newline >>% ()) <|> eof

    let pDefinitionLine =
        pDefinition .>> ws .>> pLineEnd

    let pProgram =
        pipe2
            (many pDefinitionLine)
            (ws >>. pApplication)
            (fun defs expr -> (defs, expr))
        .>> eof

    let run parser input =
        match run parser input with
        | Success(result, _, _) -> Ok result
        | Failure(msg, _, _) -> Error("ParsingError: " + msg)

module Interpreter =
    let private varCounter = ref 0

    let private freshVar () =
        varCounter.Value <- varCounter.Value + 1
        $"v{varCounter.Value}"

    let rec freeVars term =
        match term with
        | Var s -> Set.singleton s
        | Lambda (params, body) ->
            Set.difference (freeVars body) (Set.ofList params)
        | Apply (t1, t2) ->
            Set.union (freeVars t1) (freeVars t2)

    let rec subst term varToReplace replacement =
        match term with
        | Var v when v = varToReplace -> replacement
        | Var _ -> term
        | Apply (t1, t2) ->
            Apply(subst t1 varToReplace replacement,
                  subst t2 varToReplace replacement)
        | Lambda (params, body) ->
            if List.contains varToReplace params then
                term
            else
                let fv = freeVars replacement
                let conflicts =
                    params |> List.filter (fun p -> Set.contains p fv)

                if List.isEmpty conflicts then
                    Lambda(params, subst body varToReplace replacement)
                else
                    let newParams, newBody =
                        List.fold (fun (ps, b) p ->
                            if List.contains p conflicts then
                                let np = freshVar()
                                (np :: ps, subst b p (Var np))
                            else
                                (p :: ps, b)
                        ) ([], body) params

                    Lambda(List.rev newParams,
                           subst newBody varToReplace replacement)

    let rec reduce term =
        let t, changed = reduceStep term
        if changed then reduce t else t

    and private reduceStep term =
        match term with
        | Apply (t1, t2) ->
            let t1', c1 = reduceStep t1
            if c1 then (Apply(t1', t2), true)
            else
                match t1' with
                | Lambda (p::rest, body) ->
                    let newBody = subst body p t2
                    let res =
                        if List.isEmpty rest then newBody
                        else Lambda(rest, newBody)
                    (res, true)
                | _ ->
                    let t2', c2 = reduceStep t2
                    if c2 then (Apply(t1', t2'), true)
                    else (term, false)

        | Lambda (ps, body) ->
            let b', c = reduceStep body
            if c then (Lambda(ps, b'), true)
            else (term, false)

        | Var _ -> (term, false)

    let expandDefs defs expr =
        let defsMap = Map.ofList defs

        let rec expand map term =
            match term with
            | Var v when Map.containsKey v map ->
                expand map map.[v]
            | Var v -> Var v
            | Apply (t1, t2) ->
                Apply(expand map t1, expand map t2)
            | Lambda (ps, body) ->
                let map' =
                    ps |> List.fold (fun m p -> Map.remove p m) map
                Lambda(ps, expand map' body)

        expand defsMap expr

let evaluate (source: string) =
    match Parser.run Parser.pProgram source with
    | Error -> Error
    | Ok (defs, expr) ->
        try
            let expanded = Interpreter.expandDefs defs expr
            let result = Interpreter.reduce expanded
            Ok (termToString result)
        with ex ->
            Error($"ExecError: {ex.Message}")