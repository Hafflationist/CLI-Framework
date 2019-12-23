module CliFoundation.InterfaceManager

open System

let executeCommand failAction (cliCommands: CliCommand list) commandName args =
    
    let compareOptWithString optStr str =
        optStr |> Option.map(fun optStrStr -> optStrStr = str)
               |> Option.defaultValue false
               
    let checkCliCommand = function
        | CliCommandSimple simpleCommand -> simpleCommand.name = commandName
        | CliCommandWithCountedArguments countedCommand ->
            List.length args = countedCommand.numberOfArguments
            && countedCommand.name = commandName
        | CliCommandWithNamedArguments namedCommand -> false        // TODO Implement check
                       
    
    let extractExecuter = function
        | CliCommandSimple simpleCommand -> simpleCommand.executer
        | CliCommandWithCountedArguments countedCommand -> countedCommand.executer
        | CliCommandWithNamedArguments namedCommand -> (fun (a: string list) -> SimpleResponse "to be implemented!")        // TODO Implement check
               
    cliCommands
    |> List.tryFind checkCliCommand
    |> Option.map extractExecuter
    |> Option.map (fun (executer: CliExecuter) -> executer args)
    |> Option.defaultWith failAction
    
let executeCommandWithoutArgs failAction (cliCommands: CliCommand list) commandName =
    executeCommand failAction cliCommands commandName List.empty
    
let executeCommandWithArgs failAction (cliCommands: CliCommand list) commandName args =
    executeCommand failAction cliCommands commandName args
    
    
    
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
                 | command :: args -> singleExecuter command
                 | _ ->
                     printfn ""
                     Recursion
    match result with
    | Kill -> printfn "Tschüss!"
    | Recursion -> manageCommands initAction failAction cliCommands
    | SimpleResponse msg ->
        Console.WriteLine msg
        manageCommands initAction failAction cliCommands