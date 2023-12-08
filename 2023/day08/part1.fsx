#nowarn "25"
open System
open System.IO

type Path = (String * String * String)
type Network = { Steps: String; Paths: Path array }

let parseNetwork (lines: string array): Network =
  let parse (line: String): Path = (line[0..2], line[7..9], line[12..14])
  { Steps = lines[0]; Paths =  lines[2..] |> Array.map parse }

let walkTo network =
  let rec walk cur steps =
    let (_, left, right) = network.Paths |> Seq.find (fun (n, _, _) -> n = cur)
    let next = network.Steps[steps % network.Steps.Length]
    match next with
    | _ when cur = "ZZZ" -> steps
    | 'L' -> walk left (steps+1)
    | 'R' -> walk right (steps+1)
  walk "AAA" 0

File.ReadAllLines("example1.txt") |> parseNetwork |> walkTo |> printfn "steps: %A"
File.ReadAllLines("example2.txt") |> parseNetwork |> walkTo |> printfn "steps: %A"
File.ReadAllLines("input.txt") |> parseNetwork |> walkTo |> printfn "steps: %A"
