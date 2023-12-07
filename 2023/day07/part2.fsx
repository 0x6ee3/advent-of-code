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
  let jCount = cards |> Seq.filter (fun c -> c = 'J') |> Seq.length
  match (counts |> Seq.length, maxCount, jCount) with
  | 5, _, 0 -> HandType.HighCard    // 23456
  | 5, _, 1 -> HandType.OnePair     // 2345J
  
  | 4, _, 0 -> HandType.OnePair     // 22345
  | 4, _, 1 -> HandType.ThreeOfKind // 2234J
  | 4, _, 2 -> HandType.ThreeOfKind // 234JJ
  
  | 3, 3, 0 -> HandType.ThreeOfKind // 22234
  | 3, 3, 1 -> HandType.FourOfKind  // J2333
  | 3, 3, 3 -> HandType.FourOfKind  // 23JJJ

  | 3, 2, 0 -> HandType.TwoPair     // QQKKA
  | 3, 2, 1 -> HandType.FullHouse   // QQKKJ
  | 3, 2, 2 -> HandType.FourOfKind  // JJQQK
  
  | 2, 3, 0 -> HandType.FullHouse   // QQKKK
  | 2, 3, _ -> HandType.FiveOfKind  // JJKKK or QQJJJ
  | 2, 4, 0 -> HandType.FourOfKind  // QQQQK
  | 2, 4, _ -> HandType.FiveOfKind  // QQQQJ or JJJJQ

  | 1, _, _ -> HandType.FiveOfKind  // JJJJJ or KKKKK

let parse (line: string) =
  let [| cards; bid |] = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
  let t = handType cards
  { Cards = cards; Bid = int bid; Type = t }

let PlayingCards = "J23456789TQKA"
let matchCards (a: string) (b: string): int =
  let (a', b') = Seq.zip a b |> Seq.skipWhile (fun c -> fst c = snd c) |> Seq.head
  match (PlayingCards.IndexOf a', PlayingCards.IndexOf b') with
  | x, y when x > y -> 1
  | _ -> -1

let playCards (a: Hand) (b: Hand): int =
  match (a, b) with
  | _ when a.Type > b.Type -> 1
  | _ when a.Type < b.Type -> -1
  | _ when a.Cards = b.Cards -> 0
  | _ -> matchCards a.Cards b.Cards

let play (lines: string array) =
  lines
  |> Seq.map parse
  |> Seq.sortWith playCards
  |> Seq.mapi (fun i h -> (i+1)*h.Bid)
  |> Seq.sum

File.ReadAllLines("example.txt") |> play |> printfn "winnings: %A"
File.ReadAllLines("input.txt") |> play |> printfn "winnings: %A"
