#nowarn "25"
open System
open System.IO

type MapEntry = { srcStart: int64; srcEnd: int64; offset: int64 }

type Almanac = {
    Seeds: int64 list
    Maps: MapEntry list list }

let intList (line: String): int64 list =
    line.Split(" ", StringSplitOptions.RemoveEmptyEntries) |> Array.map int64 |> List.ofArray

let parseMap (input: string): MapEntry list =
    let lines = input.Split("\n")[1..]
    lines
    |> Array.map intList
    |> Array.map (fun i -> { srcStart = i[1]; srcEnd = i[1] + i[2]; offset = i[0] - i[1] })
    |> List.ofArray

let parseAlmanac (input: String) =
    let seeds :: soilmap :: fertilizermap :: watermap :: lightmap :: tempmap :: humiditymap :: locationmap :: _ =
        input.Split("\n\n") |> List.ofArray
    { Seeds = intList seeds[6..]
      Maps = [
        parseMap soilmap
        parseMap fertilizermap
        parseMap watermap
        parseMap lightmap
        parseMap tempmap
        parseMap humiditymap
        parseMap locationmap ] }

let findSeedLocations (almanac: Almanac): int64 list =
    let applyMap (seed: int64) (map: MapEntry list): int64 =
        let m = List.tryFind (fun e -> e.srcStart <= seed && seed < e.srcEnd) map
        match m with
        | Some e -> seed + e.offset
        | None -> seed

    almanac.Seeds
    |> List.map (fun seed -> List.fold applyMap seed almanac.Maps)

let location = File.ReadAllText("input.txt") |> parseAlmanac |> findSeedLocations |> List.reduce min

printfn "location: %d" location
