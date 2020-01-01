open System
open CliFoundation


[<EntryPoint>]
let main argv =
    Console.ForegroundColor <- ConsoleColor.DarkYellow
    Console.WriteLine "Hugobert in dunkelgelb"
    printfn "haha test"
    Console.ForegroundColor <- ConsoleColor.Yellow
    Console.WriteLine "Hugobert in dunkelgelb"

    let initAction() = Console.WriteLine("init-action!")
    let failAction _ _ = SimpleResponse "error-handling!"
    let executer listOfArgs = Console.WriteLine("Receiving: " + listOfArgs.ToString())
                              Recursion
    let cliCommands = [
        CliCommandSimple {
            name = "kill";
            executer = (fun listOfArgs -> Kill)
        }
        CliCommandSimple {
            name = "do";
            executer = executer
        }
        CliCommandWithCountedArguments {
            name = "do2";
            numberOfArguments = 3;
            executer = executer
        }
        CliCommandWithNamedArguments {
            name = "do3";
            arguments = [ "-first"; "-second" ];
            executer = executer
        }

    ]
    InterfaceManager.manageCommands initAction failAction cliCommands;



    0 // return an integer exit code

