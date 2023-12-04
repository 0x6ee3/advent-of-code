#nowarn "25"
open System
open System.IO

type Card = { id: int; picks: int Set; wins: int Set }

let parseNumbers (line: string) =
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int |> Set.ofArray

let parseRound (line: string): Card =
    let cardNo :: pickNo :: winsNo :: _ = line[5..].Split [| ':'; '|' |] |> Array.map _.Trim() |> List.ofArray
    { id = cardNo |> int
      picks = pickNo |> parseNumbers
      wins = winsNo |> parseNumbers}

let pointValue round =
    let matching = Set.intersectMany [ round.picks; round.wins ] |> Seq.length
    pown 2 (matching-1)

let part1 (lines: string list) =
    lines
    |> List.map parseRound
    |> List.map pointValue
    |> List.sum

File.ReadAllLines("example.txt") |> List.ofArray |> part1 |> printfn "example: %A"
File.ReadAllLines("input.txt") |> List.ofArray |> part1 |> printfn "input: %A"

let toWinsList id wins: int list =
    match wins with
    | 0 -> []
    | 1 -> [id+1]
    | w -> [id+1 .. id+w]

let rec cardWins (rounds: Card list) (wins: int list) (acc: int) =
    match wins with
    | id :: rest ->
        let round = rounds |> List.find (fun c -> c.id = id)
        let wins' = Set.intersectMany [ round.picks; round.wins ] |> Seq.length
        let wins'' = toWinsList id wins'
        cardWins rounds (wins'' @ rest) (acc + wins''.Length)
    | [] -> acc

let part2 (lines: string list) =
    let rounds = lines |> List.map parseRound
    cardWins rounds (rounds |> List.map _.id) rounds.Length

File.ReadAllLines("example.txt") |> List.ofArray |> part2 |> printfn "example: %A"
File.ReadAllLines("input.txt") |> List.ofArray |> part2 |> printfn "input: %A"
