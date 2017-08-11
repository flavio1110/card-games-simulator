namespace CardsSimulator

open System

type Suit = 
    | Clubs 
    | Diamonds 
    | Hearts 
    | Spades

type Rank = 
    | Ace = 14 
    | King  = 13
    | Queen = 12 
    | Jack = 11 
    | Ten = 10
    | Nine = 9
    | Eight = 8
    | Seven =7
    | Six = 6
    | Five = 5
    | Four = 4
    | Three = 3
    | Two = 2

type Card = Suit * Rank

module Deck =
    let rand = Random()

    let Shuffle (org:_[]) = 
        let arr = Array.copy org
        let max = (arr.Length - 1)
        let randomSwap (arr:_[]) i =
            let pos = rand.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr
       
        [|0..max|] |> Array.fold randomSwap arr
    let FullDeck = 
        [for suit in [Suit.Clubs; Suit.Diamonds; Suit.Hearts; Suit.Spades] do 
            yield (suit, Rank.Two);
            yield (suit, Rank.Three);
            yield (suit, Rank.Four);
            yield (suit, Rank.Five);
            yield (suit, Rank.Six);
            yield (suit, Rank.Seven);
            yield (suit, Rank.Eight);
            yield (suit, Rank.Nine);
            yield (suit, Rank.Ten);
            yield (suit, Rank.Jack);
            yield (suit, Rank.Queen);
            yield (suit, Rank.King);
            yield (suit, Rank.Ace);
        ]

    let mutable ShuffledDeck = FullDeck |> List.toArray |> Shuffle
    
    let GetCard () =
        let nextCad = ShuffledDeck.[0]
        ShuffledDeck <- ShuffledDeck.[1..]
        nextCad

    let DescribeCard suit rank =
        rank.ToString() + suit.ToString()