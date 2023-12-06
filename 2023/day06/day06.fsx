#nowarn "25"
open System
open System.IO

type Race = (int64 * int64)

let numbers (line: String) =
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int64 |> List.ofArray

let parseRaces (lines: string array) =
    let [| times ; distances |] = lines
    let times' = numbers times[9..]
    let distances' = numbers distances[9..]
    List.zip times' distances'

// let parseRacesTranspose (lines: string array) =
//     let numbers (line: string) =
//         line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
//         |> Array.tail
//         |> Array.map int64
//     lines
//     |> Array.map numbers
//     |> Array.transpose

let findWinners (races: Race list) =
    let raceResult race speed =
        ((fst race - speed) * speed) > snd race

    let runRaces (race: Race) =
        [1L..fst race] |> List.filter (raceResult race) |> List.length

    races |> List.map runRaces |> List.reduce (*)

File.ReadAllLines("example.txt") |> parseRaces |> findWinners |> printfn "races: %A"
File.ReadAllLines("input.txt") |> parseRaces |> findWinners |> printfn "races: %A"

let number (line: string): int64 =
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
    |> Array.reduce (+)
    |> int64

let parseRaces2 (lines: string array) =
    let [| times ; distances |] = lines
    let times' = number times[9..]
    let distances' = number distances[9..]
    [(times', distances')]

File.ReadAllLines("example.txt") |> parseRaces2 |> findWinners |> printfn "races: %A"
File.ReadAllLines("input.txt") |> parseRaces2 |> findWinners |> printfn "races: %A"
