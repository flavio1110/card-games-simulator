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
        
        let hasOnlyOneSuit cards = 
            cards 
            |> Array.distinctBy(fun (suit, rank) -> suit)
            |> Array.length = 1

        let ranksGroup = 
            ordernedCards
            |> Array.groupBy(fun (suit, rank) -> rank) 
            |> Array.sortByDescending(fun (key, values) -> values.Length)

        let _, rankFst = ordernedCards.[0]

        let key, sameRank = ranksGroup.[0]
        let haveRanks number = ranksGroup.Length = number        
        let startsWithTen = rankFst = Rank.Ten                
        let topCardWithSameRank = sameRank.Length
        let groupedAs (differentRanks, occurrences) = 
            haveRanks differentRanks && topCardWithSameRank = occurrences

        let isStraight = isSequence ordernedCards 
        let isFlush = hasOnlyOneSuit ordernedCards      

        let hand = 
            match ordernedCards with
            | h when isFlush && isStraight && startsWithTen -> HandValue.RoyalFlush
            | h when isFlush && isStraight -> HandValue.StraightFlush
            | h when groupedAs (2, 4) -> HandValue.FourOfAkind
            | h when groupedAs (2, 3) -> HandValue.FullHouse
            | h when isFlush -> HandValue.Flush
            | h when isStraight  -> HandValue.Straight
            | h when groupedAs (3, 3) -> HandValue.ThreeOfAKind
            | h when groupedAs (3, 2) -> HandValue.TwoPairs
            | h when groupedAs (4, 2) -> HandValue.Pair
            | _ -> HandValue.HighCard
        
        hand    