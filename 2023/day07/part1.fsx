#nowarn "25"
open System
open System.IO

type HandType =
  | FiveOfKind = 6
  | FourOfKind = 5
  | FullHouse = 4
  | ThreeOfKind = 3
  | TwoPair = 2
  | OnePair = 1
  | HighCard = 0

type Hand = { Cards: string; Bid: int; Type: HandType; }

let handType cards =
  let counts = cards |> Seq.countBy id
  let maxCount = counts |> Seq.maxBy snd |> snd
  match (counts |> Seq.length, maxCount) with
  | 1, _ -> HandType.FiveOfKind
  | 2, 4 -> HandType.FourOfKind
  | 2, 3 -> HandType.FullHouse
  | 3, 3 -> HandType.ThreeOfKind
  | 3, 2 -> HandType.TwoPair
  | 4, _ -> HandType.OnePair
  | 5, _ -> HandType.HighCard

let parse (line: string) =
  let [| cards; bid |] = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
  { Cards = cards; Bid = int bid; Type = handType cards }

let PlayingCards = "23456789TJQKA"
let matchCards (a: string) (b: string): int =
  let (a', b') = Seq.zip a b
              |> Seq.skipWhile (fun c -> fst c = snd c)
              |> Seq.head
  match (PlayingCards.IndexOf a', PlayingCards.IndexOf b') with
  | x, y when x > y -> 1
  | _ -> -1

let playCards (a: Hand) (b: Hand): int =
  match (a, b) with
  | _ when a.Type > b.Type -> 1
  | _ when a.Type < b.Type -> -1
  | _ when a.Cards = b.Cards -> 0
  | _ -> matchCards a.Cards b.Cards

let part1 (lines: string array) =
  lines
  |> Seq.map parse
  |> Seq.sortWith playCards
  |> Seq.mapi (fun i h -> (i+1)*h.Bid)
  |> Seq.sum

File.ReadAllLines("example.txt") |> part1 |> printfn "winnings: %A"
File.ReadAllLines("input.txt") |> part1 |> printfn "winnings: %A"
