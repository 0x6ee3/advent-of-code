#nowarn "25"
#r "nuget: MathNet.Numerics, 5.0.0"
#r "nuget: MathNet.Numerics.FSharp, 5.0.0"
open System
open System.IO
open MathNet.Numerics

type Path = (String * String * String)
type Network = { Steps: String; Paths: Path array }

let parseNetwork (lines: string array): Network =
  let parse (line: String): Path = (line[0..2], line[7..9], line[12..14])
  { Steps = lines[0]; Paths =  lines[2..] |> Array.map parse }

let walkToZ start network =
  let rec walk cur steps =
    let (_, left, right) = network.Paths |> Seq.find (fun (n, _, _) -> n = cur)
    let next = network.Steps[steps % network.Steps.Length]
    match next with
    | _ when cur.EndsWith 'Z' -> steps
    | 'L' -> walk left (steps+1)
    | 'R' -> walk right (steps+1)
  walk start 0

let ghostWalk network =
  network.Paths
  |> Seq.filter (fun (p, _, _) -> p[2] = 'A')
  |> Seq.map (fun (start, _, _) -> walkToZ start network)
  |> Seq.map int64
  |> Array.ofSeq
  |> Euclid.LeastCommonMultiple

File.ReadAllLines("example3.txt") |> parseNetwork |> ghostWalk |> printfn "steps: %A"
File.ReadAllLines("input.txt") |> parseNetwork |> ghostWalk |> printfn "steps: %A"
