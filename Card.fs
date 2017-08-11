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

type Card =
  | Regular of Suit * Rank
  | Joker

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
            yield Card.Regular(suit, Rank.Two);
            yield Card.Regular(suit, Rank.Three);
            yield Card.Regular(suit, Rank.Four);
            yield Card.Regular(suit, Rank.Five);
            yield Card.Regular(suit, Rank.Six);
            yield Card.Regular(suit, Rank.Seven);
            yield Card.Regular(suit, Rank.Eight);
            yield Card.Regular(suit, Rank.Nine);
            yield Card.Regular(suit, Rank.Ten);
            yield Card.Regular(suit, Rank.Jack);
            yield Card.Regular(suit, Rank.Queen);
            yield Card.Regular(suit, Rank.King);
            yield Card.Regular(suit, Rank.Ace);
        ]

    let mutable ShuffledDeck = FullDeck |> List.toArray |> Shuffle
    
    let GetCard () =
        let nextCad = ShuffledDeck.[0]
        ShuffledDeck <- ShuffledDeck.[1..]
        nextCad

    let DescribeCard card =
        match card with        
        | Regular (suit, rank) -> rank.ToString() + suit.ToString()
        | Joker -> "Joker"