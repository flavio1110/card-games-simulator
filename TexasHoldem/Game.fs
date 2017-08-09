namespace CardsSimulator

open CardsSimulator.Deck

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

module Poker =
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
        Players = [| player1; player2|]
    }