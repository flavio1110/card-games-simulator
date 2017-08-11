module Tests

open System
open Xunit
open CardsSimulator
open CardsSimulator.Deck
open TexasHoldemPoker

[<Fact>]
let ``A sequence from 10 to Ace of same suit should be RoyalFlush`` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Ten
               Suit.Clubs, Rank.Jack
               Suit.Clubs, Rank.Queen 
               Suit.Clubs, Rank.King 
               Suit.Clubs, Rank.Ace |]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.RoyalFlush, actual)
