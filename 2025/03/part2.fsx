#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO

let parse (line : string) : int list =
    line |> Seq.map (string >> int) |> List.ofSeq

test <@ parse "987654321111111" = [9; 8; 7; 6; 5; 4; 3; 2; 1; 1; 1; 1; 1; 1; 1] @>
test <@ parse "811111111111119" = [8; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 9] @>
test <@ parse "234234234234278" = [2; 3; 4; 2; 3; 4; 2; 3; 4; 2; 3; 4; 2; 7; 8] @>
test <@ parse "818181911112111" = [8; 1; 8; 1; 8; 1; 9; 1; 1; 1; 1; 2; 1; 1; 1] @>

let rec jolts (acc : int list) (batteries : int) (bank : int list) : int list =
    if batteries = 0 then             acc
    elif batteries = bank.Length then acc @ bank
    else 
        let i, x = bank
                |> List.take (bank.Length-batteries+1)
                |> List.indexed
                |> List.maxBy snd
        jolts (acc @ [x]) (batteries-1) bank[i+1..]

test <@ jolts [] 12 [8; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 9] = [8; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 9] @>
test <@ jolts [] 12 [9; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 8; 9] = [9; 1; 1; 1; 1; 1; 1; 1; 1; 1; 8; 9] @>
test <@ jolts [] 12 [8; 1; 8; 1; 8; 1; 9; 1; 1; 1; 1; 2; 1; 1; 1] = [8; 8; 8; 9; 1; 1; 1; 1; 2; 1; 1; 1] @>
test <@ jolts [] 2  [9; 8; 7; 6; 5; 4; 3; 2; 1; 1; 1; 1; 1; 1; 1] = [9;8] @>

let joltage (bank : int list) : int64 =
    jolts [] 12 bank
    |> List.map string
    |> String.concat ""
    |> int64

test <@ joltage [9; 8; 7; 6; 5; 4; 3; 2; 1; 1; 1; 1; 1; 1; 1] = 987654321111L @>
test <@ joltage [8; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 1; 9] = 811111111119L @>
test <@ joltage [2; 3; 4; 2; 3; 4; 2; 3; 4; 2; 3; 4; 2; 7; 8] = 434234234278L @>
test <@ joltage [8; 1; 8; 1; 8; 1; 9; 1; 1; 1; 1; 2; 1; 1; 1] = 888911112111L @>

let solve lines =
    lines
    |> List.map (parse >> joltage)
    |> List.sum

File.ReadAllLines "example.txt" |> List.ofArray |> solve |> printfn "example %d"
File.ReadAllLines "input.txt" |> List.ofArray |> solve |> printfn "input %d"
