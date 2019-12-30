module CliFoundation.InterfaceManager

open System

let private getElementsWithIndexEven = List.indexed
                                       >> List.filter (fst >> ((%) 2) >> (=) 0)    //|> List.filter (fun pair -> (fst pair) % 2 = 0)
                                       >> List.map snd
let private getElementsWithIndexOdd = List.indexed
                                      >> List.filter (fst >> ((%) 2) >> (=) 1)    //|> List.filter (fun pair -> (fst pair) % 2 = 1)
                                      >> List.map snd

let private executeCommand failAction (cliCommands : CliCommand list) commandName args =

    let compareOptWithString optStr str =
        optStr |> Option.map (fun optStrStr -> optStrStr = str)
               |> Option.defaultValue false

    let checkCliCommand = function
        | CliCommandSimple simpleCommand -> simpleCommand.name = commandName
        | CliCommandWithCountedArguments countedCommand ->
            List.length args = countedCommand.numberOfArguments
            && countedCommand.name = commandName
        | CliCommandWithNamedArguments namedCommand ->
            let names = args |> getElementsWithIndexEven
            namedCommand.arguments |> List.forall (fun argumentName -> List.contains argumentName names)
            && (List.length args) % 2 = 0

    let extractExecuter = function
        | CliCommandSimple simpleCommand -> simpleCommand.executer
        | CliCommandWithCountedArguments countedCommand -> countedCommand.executer
        | CliCommandWithNamedArguments namedCommand ->
            (fun (namesAndArguments : string list) ->
                let names = getElementsWithIndexEven namesAndArguments
                let arguments = getElementsWithIndexOdd namesAndArguments
                List.map2 (fun a b -> (a, b)) names arguments
                |> Map.ofList
                |> namedCommand.executer)

    cliCommands
    |> List.tryFind checkCliCommand
    |> Option.map extractExecuter
    |> Option.map (fun (executer : CliExecuter) -> executer args)
    |> Option.defaultWith failAction

let private executeCommandWithoutArgs failAction (cliCommands : CliCommand list) commandName =
    executeCommand failAction cliCommands commandName List.empty

let private executeCommandWithArgs failAction (cliCommands : CliCommand list) commandName args =
    executeCommand failAction cliCommands commandName args



let rec manageCommands initAction failAction (cliCommands : CliCommand list) =

    let singleExecuter = executeCommandWithoutArgs failAction cliCommands

    initAction()
    let inputText = Console.ReadLine()
    let inputParts = inputText.Split [| ' ' |]
                     |> Array.toList
    let result = match inputParts with
                 | command :: args -> executeCommand failAction cliCommands command args
                 | _ ->
                     printfn ""
                     Recursion
    match result with
    | Kill -> printfn "Tschüss!"
    | Recursion -> manageCommands initAction failAction cliCommands
    | SimpleResponse msg ->
        Console.WriteLine msg
        manageCommands initAction failAction cliCommands