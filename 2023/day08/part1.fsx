#nowarn "25"
open System
open System.IO

type Path = (String * String * String)
type Network = { Steps: String; Paths: Path array }

let parseNetwork (lines: string array): Network =
  let parse (line: String): Path = (line[0..2], line[7..9], line[12..15])
  { Steps = lines[0]; Paths =  lines[2..] |> Array.map parse }

let rec walk nextStep goal paths cur way =
  let node = paths |> Seq.find (fun (n, _, _) -> n = cur)
  let next = nextStep |> Seq.head
  match goal, cur, next with
  | _ when goal = cur -> way
  | _, _, "L" -> cur :: way
  | _, _, "R" -> cur :: way

let walkNextStep network =
  seq { yield "R" }

let walkTo goal (network: Network) =
  let (start, _, _) = network.Paths |> Seq.head
  
  let steps = walk nextStep goal network.Paths start []
  steps |> Seq.length

File.ReadAllLines("example1.txt") |> parseNetwork |> walkTo "ZZZ" |> printfn "steps: %A"
File.ReadAllLines("example2.txt") |> parseNetwork |> walkTo "ZZZ" |> printfn "steps: %A"
