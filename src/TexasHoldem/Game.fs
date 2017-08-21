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
        let _, rankLst = ordernedCards.[4]

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
        
        let isInSequence cards =
            cards 
            |> Array.mapi (fun i card -> cardIsSequenceFromLastOne i)
            |> Array.reduce (fun acc isSeq -> isSeq)

        match rankFst with
        | Rank.Ace -> false
        | Rank.King -> false
        | Rank.Queen -> false
        | Rank.Jack -> false
        | Rank.Two ->
            if rankLst = Rank.Ace then
                isInSequence (ordernedCards |> Array.take 4)
            else
                isInSequence ordernedCards
        | _ -> 
            isInSequence ordernedCards

    let getHandValue hand = 
        let ordernedCards = 
            hand.Cards
            |> Array.sortBy(fun (suit, rank) -> rank) 
        
        let suitsGroups = 
            ordernedCards 
            |> Array.groupBy(fun (suit, rank) -> suit)
            |> Array.sortByDescending(fun (key, values) -> values.Length)

        let ranksGroup = 
            ordernedCards
            |> Array.groupBy(fun (suit, rank) -> rank) 
            |> Array.sortByDescending(fun (key, values) -> values.Length)

        let _, rankFst = ordernedCards.[0]

        let (key, sameRank) = ranksGroup.[0]
        let haveSuits number = suitsGroups.Length = number
        let haveRanks number = ranksGroup.Length = number        
        let sequenceStartsWithTen = rankFst = Rank.Ten
        let isStraight = isSequence ordernedCards
         
        let topCardWithSameRank = sameRank.Length
        let isFlush = haveSuits 1
        let isFourOfAKind = haveRanks 2 && topCardWithSameRank = 4
        let isRoyalFlush = isFlush && isStraight && sequenceStartsWithTen
        let isStraightFlush = isFlush && isStraight
        let isFullHouse = haveRanks 2 && topCardWithSameRank = 3
        let isThreeOfAKind = haveRanks 3 && topCardWithSameRank = 3
        let isTwoPairs = haveRanks 3 && topCardWithSameRank = 2
        let isPair = haveRanks 4 && topCardWithSameRank = 2

        let handValue =
            if isRoyalFlush then
                HandValue.RoyalFlush
            elif isStraightFlush then
                HandValue.StraightFlush
            elif isFourOfAKind then
                HandValue.FourOfAkind
            elif isFullHouse then
               HandValue.FullHouse
            elif isFlush then
                HandValue.Flush
            elif isStraight then
                HandValue.Straight
            elif isThreeOfAKind then
                HandValue.ThreeOfAKind
            elif isTwoPairs then
                HandValue.TwoPairs
            elif isPair then
                HandValue.Pair
            else
                HandValue.HighCard

        handValue
    