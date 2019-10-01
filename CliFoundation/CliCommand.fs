namespace CliFoundation

type ResultOfCommand =
    | Kill
    | Recursion
    | SimpleResponse of string

type CliExecuter = Map<string, string> option -> ResultOfCommand

type CliCommand = {
    name : string option;
    arguments: string list option;
    executer: CliExecuter option
}


module CliCommand =
    
    let create = {
        name = None;
        arguments = None;
        executer = None
    }
    
    let setName name old = { old with name = Some name }
    
    let addArgumentName argument old = {
         old with arguments = old.arguments
                              |> Option.map (fun oldArguemnts -> argument :: oldArguemnts)
                              |> Option.defaultValue [argument]
                              |> Some
    }
    
    let setExecuter executer old = { old with executer = Some executer }