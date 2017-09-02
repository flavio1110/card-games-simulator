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

[<Fact>]
let ``When have five cards in sequence with different Suits should be Straight`` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Five
               Suit.Diamonds, Rank.Six
               Suit.Clubs, Rank.Seven
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Nine|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.Straight, actual)

[<Fact>]
let ``When have three cards of the same rank and other two cards of the other rank should be FullHouse `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.King
               Suit.Clubs, Rank.Eight
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    
    let actual = getHandValue hand

    Assert.Equal(HandValue.FullHouse, actual)

[<Fact>]
let ``When have a set of cards with three cards of the same rank and other two cards of the other rank should be FullHouse `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Clubs, Rank.Eight
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.ThreeOfAKind, actual)

[<Fact>]
let ``When have three cards of the same rank should be ThreeOfAKind `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.Nine
               Suit.Clubs, Rank.Eight
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.ThreeOfAKind, actual)

[<Fact>]
let ``When have set of four cards with three cards of the same rank should be ThreeOfAKind `` () =
    let hand = {
        Cards = 
            [| Suit.Diamonds, Rank.Nine
               Suit.Clubs, Rank.Eight
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.ThreeOfAKind, actual)

[<Fact>]
let ``When have set of three cards with all cards of the same rank should be ThreeOfAKind `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Eight
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.ThreeOfAKind, actual)

[<Fact>]
let ``When have two pairs should be TwoPairs `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.Nine
               Suit.Clubs, Rank.Nine
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.TwoPairs, actual)

[<Fact>]
let ``When have one pair should be Pair `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.Six
               Suit.Clubs, Rank.Nine
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.Pair, actual)


[<Fact>]
let ``When have a set of three cards with one pair should be Pair `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.Nine
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Eight|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.Pair, actual)

[<Fact>]
let ``When don't have any other combination should be HighCard `` () =
    let hand = {
        Cards = 
            [| Suit.Clubs, Rank.King
               Suit.Diamonds, Rank.Six
               Suit.Clubs, Rank.Nine
               Suit.Spades, Rank.Eight 
               Suit.Hearts, Rank.Five|]
    }
    let actual = getHandValue hand

    Assert.Equal(HandValue.HighCard, actual)