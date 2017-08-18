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

        let cardIsSequenceFromLastOne i =
            let isSeq = 
                if i = 0 then
                    true
                else
                    let _, prevRank = ordernedCards.[i - 1]
                    let _, currRank = ordernedCards.[i]
                    let expectedRank = enum<Rank>((int prevRank) + 1)
                    expectedRank = currRank
            isSeq                

        match rankFst with
        | Rank.Ace -> false
        | Rank.King -> false
        | Rank.Queen -> false
        | Rank.Jack -> false
        | _ -> 
            ordernedCards 
            |> Array.mapi (fun i card -> cardIsSequenceFromLastOne i)
            |> Array.reduce (fun acc isSeq -> isSeq)

    let getHandValue hand = 
        let ordernedCards = Array.sortBy(fun (suit, rank) -> rank) hand.Cards
        let suits = Array.groupBy(fun (suit, rank) -> suit) hand.Cards
        let ranks = Array.groupBy(fun (suit, rank) -> rank) hand.Cards

        let suitFst, rankFst = ordernedCards.[0]
        let suitLst, rankLst = ordernedCards.[4]

        let haveSuits number = suits.Length = number
        let haveRanks number = ranks.Length = number        
        let sequenceStartsWithTen = rankFst = Rank.Ten
        let isStraight = isSequence ordernedCards

        let isFlush = haveSuits 1
        let isFourOfAKind = haveRanks 2
        let isRoyalFlush = isFlush && isStraight && sequenceStartsWithTen
        let isStraightFlush = isFlush && isStraight
        
        let hand =
            if isRoyalFlush then
                HandValue.RoyalFlush
            elif isStraightFlush then
                HandValue.StraightFlush
            elif isFourOfAKind then
                HandValue.FourOfAkind
            elif isFlush then
                HandValue.Flush
            else
                HandValue.HighCard

        hand
    