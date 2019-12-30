module CliFoundation.CLIFoundation
open System

[<EntryPoint>]
let main args =
    let inputText = Console.ReadLine()
    let inputParts = inputText.Split [| ' ' |]
                     |> Array.toList
    match inputParts with
    | command :: args :: body -> printfn "command with args"
    | command :: args -> printfn "command without args"
    | [] -> printfn "nothing"


    
    0