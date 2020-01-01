module CliFoundation.InterfaceManager

open CliFoundation
open System
        
let rec manageCommandsInner failAction (cliCommands : CliCommand list) =
    let singleExecuter = CommandExecuter.executeCommandWithoutArgs failAction cliCommands

    let inputText = Console.ReadLine()
    let inputParts = inputText.Split [| ' ' |]
                     |> Array.toList
    let result = match inputParts with
                 | command :: args -> CommandExecuter.executeCommand failAction cliCommands command args
                 | _ ->  Recursion
    match result with
    | Kill -> printfn "Tschüss!"
    | Recursion -> manageCommandsInner failAction cliCommands
    | SimpleResponse msg ->
        Console.WriteLine msg
        manageCommandsInner failAction cliCommands

let manageCommands initAction failAction (cliCommands : CliCommand list) =
    initAction()
    manageCommandsInner failAction cliCommands