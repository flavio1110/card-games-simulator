open System
open CardsSimulator
open CardsSimulator.Deck
open CardsSimulator.TexasHoldemPoker

[<EntryPoint>]
let main argv =
    let cards = 
        [| Suit.Clubs, Rank.Six
           Suit.Clubs, Rank.Two
           Suit.Diamonds, Rank.Three
           Suit.Clubs, Rank.Four 
           Suit.Clubs, Rank.Five|]
               
    let actual = isSequence cards

    Console.WriteLine actual

    0 // return an integer exit code
