namespace CardsSimulator

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

    let getHandValue hand = 
        let sameSuit suit prevSuit = suit = prevSuit 
        let suit, rank = hand.Cards.[0]
        // TODO: Figure it out how it works :) Fold is the solution?
        //let sumList = Array.fold sameSuit hand.Cards
        HandValue.RoyalFlush
    