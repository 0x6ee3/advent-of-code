open System.IO

let example = """123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  """

let solve (lines : string) =
    0

File.ReadAllText "example.txt" |> solve |> printfn "example %d"
File.ReadAllText "input.txt" |> solve |> printfn "input %d"
