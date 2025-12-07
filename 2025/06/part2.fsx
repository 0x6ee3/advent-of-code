open System.IO

let chunkBy predicate input =
    let rec chunk acc cur input =
        match input with
        | x :: xs when predicate x -> chunk (cur :: acc) [] xs
        | x :: xs                  -> chunk acc (x :: cur) xs
        | [] -> cur :: acc
    chunk [] [] input |> List.rev |> List.map List.rev

let parse (problem : string seq list) =
    let len = problem[0] |> Seq.length
    let numbers = problem |> List.map (Seq.take (len-1) >> String.concat "" >> int64)
    let op = problem |> List.map Seq.last |> List.filter (fun c -> c <> " ") |> String.concat ""
    op, numbers

let calculate (op : string, numbers : int64 list) : int64 =
    match op with
    | "*" -> numbers |> List.fold (fun a b -> a * b) 1L
    | "+" -> numbers |> List.sum
    | _ -> 0
    
let solve (lines : string array) =
    lines
    |> Seq.map (Seq.map string)
    |> Seq.transpose
    |> List.ofSeq
    |> chunkBy (Seq.forall ((=) " "))
    |> List.map (parse >> calculate)
    |> List.sum

File.ReadAllLines "example.txt" |> solve |> printfn "\nexample %d"
File.ReadAllLines "input.txt" |> solve |> printfn "\ninput %d"
