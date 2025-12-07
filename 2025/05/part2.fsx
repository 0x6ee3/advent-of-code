#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO

let parse (line : string) : int64 * int64 =
    let ids = line.Split "-"
    int64 ids[0], int64 ids[1]

let merge (a : int64 * int64) (b : int64 * int64) : (int64 * int64) list =
    if fst a <= fst b && fst b <= snd a && snd a <= snd b then
        [(fst a, snd b)]
    elif fst a <= fst b && snd b <= snd a then
        [a]
    else
        [a; b]

test <@ merge (3, 5) (10, 14) = [3, 5; 10, 14] @>
test <@ merge (10, 14) (12, 18) = [10, 18] @>
test <@ merge (10, 18) (16, 20) = [10, 20] @>

// trim requires the lists to be sorted by the first element
// if there are at least 2 elements left in ranges try to merge them
// if they are mergeable prepend to ranges and repeat
// if they are not mergeable continue with the rest of the list
let rec trim (ranges : (int64 * int64) list) : (int64 * int64) list =
    match ranges with
    | a :: b :: ranges' ->
        match merge a b with
        | [a'; b'] -> [a'] @ trim ([b'] @ ranges')
        | [a'] -> trim ([a'] @ ranges')
        | _ -> []
    | a :: _ -> [a]
    | [] -> []

test <@ trim [3, 5; 10, 14; 12, 18; 16, 20] = [3, 5; 10, 20] @>

let solve (lines : string) =
    let lines' = lines.Split "\r\n\r\n"
    let ranges = lines'[0].Split "\n" |> Seq.map parse |> Seq.sortBy fst |> List.ofSeq
    let ranges' = trim ranges
    ranges' |> List.map (fun (a, b) -> b-a+1L) |> List.sum

File.ReadAllText "example.txt" |> solve |> printfn "example %d"
File.ReadAllText "input.txt" |> solve |> printfn "input %d"
