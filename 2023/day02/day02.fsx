#nowarn "25"

open System
open System.IO

type Hand = { Red: int; Green: int; Blue: int }
type Game = { Number: int; Hands: array<Hand> }

let rec parseCubes (cubes: Hand) (input: list<string>) : Hand =
    match input with
    | x :: c :: xs when c = "red" -> parseCubes { cubes with Red = x |> int } xs
    | x :: c :: xs when c = "green" -> parseCubes { cubes with Green = x |> int } xs
    | x :: c :: xs when c = "blue" -> parseCubes { cubes with Blue = x |> int } xs
    | x :: xs when x = "" -> parseCubes cubes xs
    | _ -> cubes

let parseHand (line: string) : Hand =
    let cubes = { Red = 0; Green = 0; Blue = 0 }
    parseCubes cubes (line.Split [| ' '; ',' |] |> List.ofArray)

let toGame (line: string) =
    let [| game; hands |] = line.Split ":"

    { Number = game[5..] |> int // "Game " = 5
      Hands = hands.Split ';' |> Array.map parseHand }

let impossibleMove hand =
    match hand with
    | _ when hand.Red > 12 -> true
    | _ when hand.Green > 13 -> true
    | _ when hand.Blue > 14 -> true
    | _ -> false

let fitsInBag (game: Game) : bool =
    game.Hands |> Array.exists impossibleMove |> not

let part1 lines =
    lines
    |> Array.map toGame
    |> Array.filter fitsInBag
    |> Array.map _.Number
    |> Array.sum

File.ReadAllLines("example1.txt") |> part1 |> printfn "example1: {%A}"
File.ReadAllLines("input.txt") |> part1 |> printfn "input: {%A}"

type MinimalGame = { Number: int; Hand: Hand }

let maxCube a b =
    { a with
        Red = List.max [ a.Red; b.Red ]
        Blue = List.max [ a.Blue; b.Blue ]
        Green = List.max [ a.Green; b.Green ] }

let rec requiredCubes (hands: list<Hand>) (cubes: Hand) =
    match hands with
    | x :: xs -> requiredCubes xs (maxCube x cubes)
    | [] -> cubes

let minimalGame (game: Game) : MinimalGame =
    { Number = game.Number
      Hand = requiredCubes (game.Hands[1..] |> List.ofArray) game.Hands[0] }

let part2 lines =
    lines
    |> Array.map toGame
    |> Array.map minimalGame
    |> Array.map (fun g -> g.Hand.Blue * g.Hand.Red * g.Hand.Green)
    |> Array.sum

File.ReadAllLines("example1.txt") |> part2 |> printfn "example1: {%A}"
File.ReadAllLines("input.txt") |> part2 |> printfn "input: {%A}"
