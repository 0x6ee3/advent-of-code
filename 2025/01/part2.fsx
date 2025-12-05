#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO

let safeSize = 100

// let rec turnRight dail turn zeros : int * int = 
//     match turn with
//     | _ when dail+turn >= safeSize -> turnRight dail (turn-safeSize) (zeros+1)
//     | _ -> dail+turn, zeros

let turnRight dail turn =
    let div = turn / safeSize
    let rem = turn % safeSize

    let dail' = dail + rem
    if dail' >= safeSize then dail' - safeSize, div + 1
    else                      dail', div

test <@ turnRight 50 5 = (55, 0) @>
test <@ turnRight 50 50 = (0, 1) @>
test <@ turnRight 50 75 = (25, 1) @>
test <@ turnRight 50 250 = (0, 3) @>
test <@ turnRight 0 50 = (50, 0) @>
test <@ turnRight 0 100 = (0, 1) @>
test <@ turnRight 0 200 = (0, 2) @>
test <@ turnRight 0 299 = (99, 2) @>
test <@ turnRight 1 299 = (0, 3) @>

// let rec turnLeft dail turn zeros : int * int =
//     match turn with
//     | _ when turn < safeSize && dail = 0 -> safeSize-turn, zeros
//     | _ when turn < safeSize && dail > turn -> dail-turn, zeros
//     | _ when turn < safeSize && dail < turn -> (dail-turn+safeSize)%safeSize, zeros+1
//     | _ when turn = dail -> 0, zeros+1
//     | _ when turn % safeSize = 0 -> dail, zeros+turn/safeSize
//     | _ -> turnLeft dail (turn-safeSize) (zeros+1)

let turnLeft dail turn : int * int =
    let div = turn / safeSize
    let rem = turn % safeSize

    let dail' =
        if rem = 0 then dail
        else (dail - rem + safeSize) % safeSize

    let turns =
        if dail > 0 && rem >= dail then 1
        else 0

    dail', div + turns

test <@ turnLeft 50 5 = (45, 0) @>
test <@ turnLeft 50 50 = (0, 1) @>
test <@ turnLeft 50 55 = (95, 1) @>
test <@ turnLeft 50 150 = (0, 2) @>
test <@ turnLeft 50 155 = (95, 2) @>
test <@ turnLeft 50 250 = (0, 3) @>
test <@ turnLeft 50 255 = (95, 3) @>
test <@ turnLeft 0 1 = (99, 0) @>
test <@ turnLeft 0 99 = (1, 0) @>
test <@ turnLeft 0 100 = (0, 1) @>
test <@ turnLeft 0 101 = (99, 1) @>
test <@ turnLeft 0 199 = (1, 1) @>
test <@ turnLeft 0 200 = (0, 2) @>
test <@ turnLeft 0 201 = (99, 2) @>
test <@ turnLeft 0 299 = (1, 2) @>

let turn dail (line: string) : int * int =
    match line.[0], line.[1..] with
    | 'R', len -> turnRight dail (int len)
    | 'L', len -> turnLeft dail (int len)
    | _ -> 0, 0

test <@ turn 50 "L68" = (82, 1) @>
test <@ turn 82 "L30" = (52, 0) @>
test <@ turn 52 "R48" = (0, 1) @>
test <@ turn 0 "L5" = (95, 0) @>
test <@ turn 95 "R60" = (55, 1) @>
test <@ turn 55 "L55" = (0, 1) @>
test <@ turn 0 "L1" = (99, 0) @>
test <@ turn 99 "L99" = (0, 1) @>
test <@ turn 0 "R14" = (14, 0) @>
test <@ turn 14 "L82" = (32, 1) @>
test <@ turn 50 "R1000" = (50, 10) @>

let part2 lines =
    lines
    |> List.scan (fun (state : int * int) line -> 
        turn (state |> fst) line
    ) (50, 0)
    |> List.map snd
    |> List.sum

File.ReadAllLines "example.txt" |> List.ofArray |> part2 |> printfn "p2 ex %d"
File.ReadAllLines "input.txt" |> List.ofArray |> part2 |> printfn "p2 in %d"
