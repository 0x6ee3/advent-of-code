open System.IO

let example = """
5973 7497 845 23 5484 92   8
6499 9645 659 91 7266 28  54
4319   39 122 88 8952 54  82
8513    7 965 65  956 21 319
+    +    *   *  +    +  *  """

let chunkBy predicate input =
    let rec chunk acc cur input =
        match input with
        | x :: xs when predicate x -> chunk (cur :: acc) [] xs
        | x :: xs                  -> chunk acc (x :: cur) xs
        | [] -> cur :: acc
    chunk [] [] input |> List.rev |> List.map List.rev

let solve (lines : string array) =
    lines
    |> Seq.map (Seq.map string)
    |> Seq.transpose
    |> List.ofSeq
    |> chunkBy (Seq.forall ((=) " "))
    // |> List.map parse
    |> printf "----\n%A\n----"
    0

File.ReadAllLines "example.txt" |> solve |> printfn "\nexample %d"
// File.ReadAllLines "input.txt" |> solve |> printfn "\ninput %d"
