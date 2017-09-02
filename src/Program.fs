open System
open CardsSimulator
open CardsSimulator.Deck
open CardsSimulator.TexasHoldemPoker

[<EntryPoint>]
let main argv =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.Nine
               Suit.Clubs, Rank.Nine
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand
    
    Console.WriteLine(actual)

    0 // return an integer exit code
