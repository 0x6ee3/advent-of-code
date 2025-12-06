open System.IO

let parse (row : int) (line : string) : (int * int) seq =
    line 
    |> Seq.indexed
    |> Seq.filter (fun (i, c) -> c = '@')
    |> Seq.map (fun (i, _) -> row, i)

let closeBy = [-1, -1; -1, 0; -1, 1;
                0, -1;         0, 1;
                1, -1;  1, 0;  1, 1]

let inline (+.) (x1, y1) (x2, y2) = x1 + x2, y1 + y2

let forkliftable (floor : (int * int) Set) (paper : int * int) =
    closeBy
    |> Seq.map ((+.) paper)
    |> Seq.filter floor.Contains
    |> Seq.length < 4

let rec clean (floor : (int * int) Set) : int =
    let papers, floor = floor |> Set.partition (forkliftable floor)
    if papers.Count = 0 then 0
    else                     papers.Count + clean floor

let solve (lines : string array) =
    let floor = lines |> Seq.mapi parse |> Seq.collect id |> Set.ofSeq
    clean floor

File.ReadAllLines "example.txt" |> solve |> printfn "example %d"
File.ReadAllLines "input.txt" |> solve |> printfn "input %d"
