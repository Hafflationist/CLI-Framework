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
type CliCommandWithCountedArguments = {
    name : string;
    numberOfArguments : int;
    executer : CliExecuter
 }
type CliCommandWithNamedArguments = {
    name : string;
    arguments : string list;
    executer : CliExecuterWithNamedArguments
 }
type CliCommand =
    | CliCommandSimple of CliCommandSimple
    | CliCommandWithCountedArguments of CliCommandWithCountedArguments
    | CliCommandWithNamedArguments of CliCommandWithNamedArguments