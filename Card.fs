namespace CardsSimulator

open System

type Suit = 
    | Clubs 
    | Diamonds 
    | Hearts 
    | Spades

type Face = 
    | Jack 
    | Queen 
    | King 
    | Ace

type Card =   
  | ValueCard of Suit * int
  | FaceCard of Suit * Face
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
            yield Card.ValueCard(suit, 2);
            yield Card.ValueCard(suit, 3);
            yield Card.ValueCard(suit, 4);
            yield Card.ValueCard(suit, 5);
            yield Card.ValueCard(suit, 6);
            yield Card.ValueCard(suit, 7);
            yield Card.ValueCard(suit, 8);
            yield Card.ValueCard(suit, 9);
            yield Card.ValueCard(suit, 10);
            yield Card.FaceCard(suit, Face.Jack);
            yield Card.FaceCard(suit, Face.Queen);
            yield Card.FaceCard(suit, Face.King);
            yield Card.FaceCard(suit, Face.Ace);
        ]

    let mutable ShuffledDeck = FullDeck |> List.toArray |> Shuffle
    
    let GetCard () =
        let nextCad = ShuffledDeck.[0]
        ShuffledDeck <- ShuffledDeck.[1..]
        nextCad

    let DescribeCard card =
        match card with        
        | ValueCard (suit, value) -> value.ToString() + suit.ToString()
        | FaceCard (suit, face) -> face.ToString() + suit.ToString()
        | Joker -> "Joker"