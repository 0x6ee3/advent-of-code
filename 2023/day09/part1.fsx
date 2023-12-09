#r "nuget: Unquote, 6.1.0"
open Swensen.Unquote
open System
open System.IO

let diffs (input: int list): int list =
  input |> List.pairwise |> List.map (fun (a, b) -> b - a)

test <@ diffs [10; 13; 16; 21; 30; 45] = [3; 3; 5; 9; 15] @>
test <@ diffs [3; 3; 5; 9; 15] = [0; 2; 4; 6] @>
test <@ diffs [0; 2; 4; 6] = [2; 2; 2] @>
test <@ diffs [2; 2; 2] = [0; 0] @>

let zeros (input: int list): bool =
  input |> List.exists (fun x -> x <> 0) |> not

test <@ zeros [0; 0; 0; 0] = true @>
test <@ zeros [0; 0; 2] = false @>

let rec diffLists (acc: int list list) (input: int list): int list list =
  match (diffs input, zeros input) with
  | d, false -> diffLists (input :: acc) d 
  | _, true -> input :: acc |> List.rev

test <@ diffLists [] [3; 3; 3; 3; 3] = [[3; 3; 3; 3; 3]; [0; 0; 0; 0]] @>
test <@ diffLists [] [0; 3; 6; 9; 12; 15] = [[0; 3; 6; 9; 12; 15]; [3; 3; 3; 3; 3]; [0; 0; 0; 0]] @>
test <@ diffLists [] [1; 3; 6; 10; 15; 21] = [[1; 3; 6; 10; 15; 21]; [2; 3; 4; 5; 6]; [1; 1; 1; 1]; [0; 0; 0]] @>

let lasts (inputs: int list list): int list =
  inputs |> List.map (fun l -> l |> List.rev |> List.head) |> List.rev

test <@ lasts (diffLists [] [1; 3; 6; 10; 15; 21]) = [0; 1; 6; 21] @>

let parse (lines: string array) =
  lines
  |> Array.map (fun line -> line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int |> Array.toList)
  |> Array.toList

let solve (inputs: int list list) =
  inputs
  |> List.map (fun input -> diffLists [] input |> lasts |> List.reduce (+))
  |> List.sum

File.ReadAllLines("example.txt") |> parse |> solve |> printfn "%A"
File.ReadAllLines("input.txt") |> parse |> solve |> printfn "%A"
