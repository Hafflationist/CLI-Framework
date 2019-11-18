open System
    
    
[<EntryPoint>]
let main argv =   
    Some 88
    |> Option.filter (fun value -> value = 0 ) 
    |> Option.defaultValue 44
    |> Console.WriteLine
    
    
    Console.ForegroundColor <- ConsoleColor.DarkYellow
    Console.WriteLine "Hugobert in dunkelgelb"
    printfn "haha test"
    Console.ForegroundColor <- ConsoleColor.Yellow
    Console.WriteLine "Hugobert in dunkelgelb"
    
    0 // return an integer exit code

