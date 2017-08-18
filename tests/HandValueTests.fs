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

[<Fact>]
let ``A sequence with the same suit should be StraightFlush`` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Nine
               Suit.Clubs, Rank.Ten
               Suit.Clubs, Rank.Jack
               Suit.Clubs, Rank.Queen 
               Suit.Clubs, Rank.King|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.StraightFlush, actual)

[<Fact>]
let ``When have four card with same Rank should be FourOfAKInd`` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Nine
               Suit.Diamonds, Rank.Nine
               Suit.Hearts, Rank.Nine
               Suit.Spades, Rank.Nine 
               Suit.Clubs, Rank.King|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.FourOfAkind, actual)

[<Fact>]
let ``When have five cards with same Suit should be Flush`` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Two
               Suit.Clubs, Rank.Nine
               Suit.Clubs, Rank.Six
               Suit.Clubs, Rank.Six 
               Suit.Clubs, Rank.King|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.Flush, actual)