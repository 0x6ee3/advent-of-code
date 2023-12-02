open System
open System.IO

let rec findDigit1 line =
    match line with
    | "" -> ""
    | _ when Char.IsNumber(line[0]) -> line[..0]
    | _ -> findDigit1 line[1..]

let rec findDigit2 line =
    match line with
    | "" -> ""
    | _ when line.StartsWith("one") || line.StartsWith("eno") -> "1"
    | _ when line.StartsWith("two") || line.StartsWith("owt") -> "2"
    | _ when line.StartsWith("three") || line.StartsWith("eerht") -> "3"
    | _ when line.StartsWith("four") || line.StartsWith("ruof") -> "4"
    | _ when line.StartsWith("five") || line.StartsWith("evif") -> "5"
    | _ when line.StartsWith("six") || line.StartsWith("xis") -> "6"
    | _ when line.StartsWith("seven") || line.StartsWith("neves") -> "7"
    | _ when line.StartsWith("eight") || line.StartsWith("thgie") -> "8"
    | _ when line.StartsWith("nine") || line.StartsWith("enin") -> "9"
    | _ when Char.IsNumber(line[0]) -> line[..0]
    | _ -> findDigit2 line[1..]

let calibrationNumber (findDigit: string -> string) line =
    findDigit line + findDigit (line |> Seq.rev |> String.Concat)

let part1 lines =
    lines |> List.map (calibrationNumber findDigit1) |> List.map int |> List.sum

let part2 lines =
    lines |> List.map (calibrationNumber findDigit2) |> List.map int |> List.sum

File.ReadAllLines("example1.txt") |> List.ofArray |> part1 |> printfn "example1: {%A}"
File.ReadAllLines("input.txt") |> List.ofArray |> part1 |> printfn "part1: {%A}"
File.ReadAllLines("example2.txt") |> List.ofArray |> part2 |> printfn "example2: {%A}"
File.ReadAllLines("input.txt") |> List.ofArray |> part2 |> printfn "part2: {%A}"
