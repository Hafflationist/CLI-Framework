module CliFoundation.InterfaceManager

open System

let executeCommandWithoutArgs failAction (cliCommands: CliCommand list) commandName =
    
    let compareOptWithString optStr str =
        optStr |> Option.map(fun optStrStr -> optStrStr = str)
               |> Option.defaultValue false
    
    cliCommands
    |> List.tryFind (fun command -> compareOptWithString command.name commandName)
    |> Option.map (fun command -> command.executer)
    |> Option.bind (Option.map (fun executer -> executer None))
    |> Option.defaultWith failAction
    
    
    
    
let rec manageCommands initAction failAction (cliCommands: CliCommand list) =
    let singleExecuter =  executeCommandWithoutArgs failAction cliCommands
    
    initAction()
    let inputText = Console.ReadLine()
    let inputParts = inputText.Split [|' '|]
                     |> Array.toList
    let result = match inputParts with
                 | command :: args :: body ->
                     printfn "command with args"
                     Recursion
                 | command :: args ->
                     singleExecuter command
                 | _ ->
                     printfn "nothing"
                     Recursion
    match result with
    | Kill -> printfn "Tschüss!"
    | Recursion -> manageCommands initAction failAction cliCommands
    | SimpleResponse msg ->
        Console.WriteLine msg
        manageCommands initAction failAction cliCommands