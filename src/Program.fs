open System
open CardsSimulator
open CardsSimulator.Deck
open CardsSimulator.TexasHoldemPoker

[<EntryPoint>]
let main argv =
    let cards = 
        [| Suit.Hearts, Rank.Six
           Suit.Clubs, Rank.Eight
           Suit.Spades, Rank.Eight
           Suit.Hearts, Rank.Eight 
           Suit.Diamonds, Rank.Nine|]
               
    let actual = isSequence cards

    0 // return an integer exit code
