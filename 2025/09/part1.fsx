#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO


let size (pair : (int*int)*(int*int)) =
    let x1, y1 = fst (fst pair), snd (fst pair)
    let x2, y2 = fst (snd pair), snd (snd pair)
    let width  = abs(x2 - x1) + 1
    let height = abs(y2 - y1) + 1
    width * height

test <@ size ((7,1), (11,7)) = 35 @>
test <@ size ((2,5), (9,7)) = 24 @>
test <@ size ((7,3), (2,3)) = 6 @>
test <@ size ((2,5), (11,1)) = 50 @>

let parse (line:string) =
    let numbers = line.Split ","
    int numbers[0], int numbers[1]

test <@ parse "1,2" = (1, 2) @>
test <@ parse "4,3" = (4, 3) @>

let solve input =
    let seats = input
                |> Seq.map parse
                |> Seq.sortBy fst
                |> List.ofSeq
    [for i in [0..seats.Length-2] do
        for j in [i..seats.Length-1] do
            yield seats[i], seats[j]]
    |> List.map size
    |> Seq.max

File.ReadAllLines "example.txt" |> solve |> printfn "example %d"
File.ReadAllLines "input.txt" |> solve |> printfn "input %d"
