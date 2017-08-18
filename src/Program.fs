open System
open CardsSimulator
open CardsSimulator.Deck
open CardsSimulator.TexasHoldemPoker

[<EntryPoint>]
let main argv =
    let cards = 
        [| Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King 
           Suit.Clubs, Rank.Ace |]
               
    let actual = isSequence cards

    Console.WriteLine actual

    0 // return an integer exit code
