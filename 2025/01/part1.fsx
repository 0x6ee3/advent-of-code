#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO

let turn dail (rotation: string) =
    match rotation.[0], rotation.[1..] with
    | 'R', len -> (dail + int len) % 100
    | 'L', len -> (dail - int len + 100) % 100
    | _ -> 0

test <@ turn 50 "L68" = 82 @>
test <@ turn 82 "L30" = 52 @>
test <@ turn 52 "R48" = 0 @>
test <@ turn 0 "L5" = 95 @>
test <@ turn 95 "R60" = 55 @>
test <@ turn 55 "L55" = 0 @>
test <@ turn 0 "L1" = 99 @>
test <@ turn 99 "L99" = 0 @>
test <@ turn 0 "R14" = 14 @>
test <@ turn 14 "L82" = 32 @>

let solve dail turns =
    turns |> List.scan turn dail

test <@ solve 50 [ "L68"; "L30"; "R48"; "L5"; "R60"; "L55"; "L1"; "L99"; "R14"; "L82" ] = [ 50; 82; 52; 0; 95; 55; 0; 99; 0; 14; 32 ] @>

let part1 lines =
    lines
    |> solve 50
    |> List.filter ((=) 0)
    |> List.length

File.ReadAllLines "example.txt" |> List.ofArray |> part1 |> printfn "example %d"
File.ReadAllLines "input.txt" |> List.ofArray |> part1 |> printfn "input %d"
