open System.IO

let calculate line =
    match line |> List.ofSeq |> List.rev with 
    | "+" :: nums -> nums |> List.map int64 |> List.sum
    | "*" :: nums -> nums |> List.map int64 |> List.fold (fun a b -> a * b) 1
    | _ -> 0

let solve (lines : string array) =
    lines
    |> Seq.map (fun line -> line.Split " " |> Seq.filter (fun s -> s <> ""))
    |> Seq.transpose
    |> Seq.map calculate
    |> Seq.sum

File.ReadAllLines "example.txt" |> solve |> printfn "example %d"
File.ReadAllLines "input.txt" |> solve |> printfn "input %d"
