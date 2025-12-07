open System.IO

let parse (line : string) : int64 * int64 =
    let ids = line.Split "-"
    int64 ids[0], int64 ids[1]

let solve (lines : string) =
    let lines' = lines.Split "\r\n\r\n"
    let ranges = lines'[0].Split "\n" |> Seq.map parse
    let ingredients = lines'.[1].Split "\n" |> Seq.map int64

    [for id in ingredients do
        for start, end' in ranges do
            if start <= id && id <= end' then
                yield id] |> Seq.distinct |> Seq.length

File.ReadAllText "example.txt" |> solve |> printfn "example %d"
File.ReadAllText "input.txt" |> solve |> printfn "input %d"
