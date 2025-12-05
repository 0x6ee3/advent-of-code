#r "nuget: Unquote, 7.0.1"
open Swensen.Unquote
open System.IO

let findValidIDs (firstID, lastID) : int64 list =
    [for i in firstID .. lastID do
        let is = string i
        if is.Length % 2 = 0 then
            let mid = is.Length / 2
            let left, right = is.[..mid-1], is.[mid..]
            if left = right then yield i]        

test <@ findValidIDs (11, 22) = [ 11; 22 ] @>
test <@ findValidIDs (95, 115) = [ 99 ] @>
test <@ findValidIDs (998, 1012) = [ 1010 ] @>
test <@ findValidIDs (1188511880, 1188511890) = [ 1188511885 ] @>
test <@ findValidIDs (222220, 222224) = [ 222222 ] @>
test <@ findValidIDs (1698522, 1698528) = [] @>
test <@ findValidIDs (446443, 446449) = [ 446446 ] @>
test <@ findValidIDs (38593856, 38593862) = [ 38593859 ] @>

let parse (line : string) : int64 * int64 = 
    let nums = line.Split "-"
    int64 nums[0], int64 nums[1]

test <@ parse "12345-67890" = (12345, 67890) @>

let part1 (line : string) =
    line.Split ","
    |> Array.map (parse >> findValidIDs)
    |> Array.sumBy List.sum

File.ReadAllLines "example.txt" |> Array.exactlyOne |> part1 |> printfn "p1 ex %d"
File.ReadAllLines "input.txt" |> Array.exactlyOne |> part1 |> printfn "p1 in %d"
