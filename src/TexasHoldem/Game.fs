namespace CardsSimulator
open System;
open CardsSimulator.Deck
open System.Linq

type Player = {
    Name : string
    Card1 : Card
    Card2 : Card
}

type Game = {
    Flop1 : Card
    Flop2 : Card
    Flop3 : Card
    Turn : Card
    River : Card
    Players : Player[]
}

type Hand = {
    Cards : Card[]
}

type HandValue = 
    | HighCard
    | Pair
    | TwoPairs
    | ThreeOfAKind
    | Straight
    | Flush
    | FullHouse
    | FourOfAkind
    | StraightFlush
    | RoyalFlush

module TexasHoldemPoker =
    let player1 = {
        Name = "John"
        Card1 = GetCard()
        Card2 = GetCard()
    }

    let player2 = {
        Name = "Smith"
        Card1 = GetCard()
        Card2 = GetCard()
    }

    let game = {
        Flop1 =  GetCard()
        Flop2 =  GetCard()
        Flop3 =  GetCard()
        Turn = GetCard()
        River = GetCard()
        Players = [| player1; player2 |]
    }

    let isSequence (cards : Card[]) =
        let ordernedCards = Array.sortBy(fun (suit, rank) -> rank) cards
        let _, rankFst = ordernedCards.[0]
        let _, rankLst = ordernedCards |> Array.last

        let cardIsSequenceFromLastOne i =
            match i with
            | 0 -> true
            | _ ->
                let _, prevRank = ordernedCards.[i - 1]
                let _, currRank = ordernedCards.[i]
                let expectedRank = enum<Rank>((int prevRank) + 1)
                expectedRank = currRank
        
        let isInSequence cards =
            cards 
            |> Array.mapi (fun i card -> cardIsSequenceFromLastOne i)
            |> Array.reduce (fun acc isSeq -> isSeq)

        match rankFst with
        | Rank.Ace | Rank.King | Rank.Queen | Rank.Jack -> false
        | Rank.Two ->
            match rankLst with
            | Rank.Ace -> isInSequence (ordernedCards |> Array.take 4)
            | _ -> isInSequence ordernedCards
        | _ -> isInSequence ordernedCards

    let getHandValue hand = 
        let ordernedCards = 
            hand.Cards
            |> Array.sortBy(fun (suit, rank) -> rank) 
        
        let groupsOfRanks = 
            ordernedCards
            |> Array.groupBy(fun (suit, rank) -> rank) 
            |> Array.sortByDescending(fun (key, values) -> values.Length)
            |> Array.map(fun (key, values) -> values.Length)

        let hasOnlyOneSuit cards = 
            cards 
            |> Array.distinctBy(fun (suit, rank) -> suit)
            |> Array.length = 1

        let startsWithTen (cards : Card[]) =
            let _, rankFst = cards.[0]
            rankFst = Rank.Ten

        let isGroupedAs (spec: List<int>) = 
            spec
            |> List.mapi(fun i num -> groupsOfRanks.Length > i && groupsOfRanks.[i] = num)
            |> List.reduce (fun acc cur -> acc && cur = acc)

        let isStraight (cards : Card[]) = 
            cards.Length = 5 && isSequence cards

        let isFlush (cards : Card[]) = 
            cards.Length = 5 && hasOnlyOneSuit cards
        
        let hand = 
            match ordernedCards with
            | h when isFlush h && isStraight h && startsWithTen h -> HandValue.RoyalFlush
            | h when isFlush h && isStraight h -> HandValue.StraightFlush
            | h when isGroupedAs [4] -> HandValue.FourOfAkind
            | h when isGroupedAs [3;2] -> HandValue.FullHouse
            | h when isFlush h -> HandValue.Flush
            | h when isStraight h -> HandValue.Straight
            | h when isGroupedAs [3] -> HandValue.ThreeOfAKind
            | h when isGroupedAs [2;2] -> HandValue.TwoPairs
            | h when isGroupedAs [2] -> HandValue.Pair
            | _ -> HandValue.HighCard
        hand    

    let getFiveCardsCombinations (cards : Card[]) =
        let rec comb n l = 
            match n, l with
            | 0, _ -> [[]]
            | _, [] -> []
            | k, (x::xs) -> List.map ((@) [x]) (comb (k-1) xs) @ comb k xs
        
        comb 5 [1..cards.Length]
        |> List.map(fun list -> list |> List.map(fun i -> cards.[i - 1]))