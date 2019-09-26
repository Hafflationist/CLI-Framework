module CliFoundation.CLIFoundation
open CliFoundation

[<EntryPoint>]
let main args =
    CliCommand.create
    |> CliCommand.setName "hugobert"
    |> CliCommand.addArgumentName "-nörgeln"
    |> CliCommand.addArgumentName "-rassismus"
    |> CliCommand.setExecuter (fun m -> "ignoring command!")
    |> ignore
    0