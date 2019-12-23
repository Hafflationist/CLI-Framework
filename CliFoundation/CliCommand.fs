namespace CliFoundation

type ResultOfCommand =
    | Kill
    | Recursion
    | SimpleResponse of string

type CliExecuter = string list -> ResultOfCommand
type CliExecuterWithNamedArguments = Map<string, string> -> ResultOfCommand

type CliCommandSimple = {
    name : string;
    executer : CliExecuter
 }
type CliCommandWithNamedArguments = {
    name : string;
    arguments : string list;
    executer : CliExecuterWithNamedArguments
 }
type CliCommandWithCountedArguments = {
    name : string;
    numberOfArguments : int;
    executer : CliExecuter
 }
type CliCommand =
    | CliCommandSimple of CliCommandSimple
    | CliCommandWithCountedArguments of CliCommandWithCountedArguments
    | CliCommandWithNamedArguments of CliCommandWithNamedArguments


//module CliCommand =

//    let create() = {
//        name = None;
//        arguments = None;
//        numberOfArguments = None;
//        executer = None
//    }

//    let setName name old = { old with name = Some name }

//    let addArgumentName argument old = {
//         old with arguments = old.arguments
//                              |> Option.map (fun oldArguemnts -> argument :: oldArguemnts)
//                              |> Option.defaultValue [ argument ]
//                              |> Some
//    }

//    let setExecuter executer old = { old with executer = Some executer }
