module CombinationTests

open System
open Xunit
open CardsSimulator
open CardsSimulator.Deck
open TexasHoldemPoker
open System.Linq

[<Fact>]
let ``A set of of 5 cards should be 1 combination of 5 cards `` () =
    let cards = 
        [| Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King 
           Suit.Clubs, Rank.Ace |]

    let actual = getFiveCardsCombinations cards

    Assert.Equal(1, actual.Length)
    for list in actual do
        Assert.True(list.All(fun card -> cards.Contains(card)))

[<Fact>]
let ``A set of of 6 cards should be 6 different combinations of 5 cards `` () =
    let cards = 
        [| Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King 
           Suit.Diamonds, Rank.King 
           Suit.Clubs, Rank.Ace |]

    let actual = getFiveCardsCombinations cards

    Assert.Equal(6, actual.Length)
    Assert.Equal(6, actual.Distinct().Count())
    for list in actual do
        Assert.True(list.All(fun card -> cards.Contains(card)))

[<Fact>]
let ``A set of of 7 cards should be 21 different combinations of 5 cards `` () =
    let cards = 
        [| Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King 
           Suit.Diamonds, Rank.King 
           Suit.Diamonds, Rank.Jack 
           Suit.Clubs, Rank.Ace |]

    let actual = getFiveCardsCombinations cards

    Assert.Equal(21, actual.Length)
    Assert.Equal(21, actual.Distinct().Count())
    for list in actual do
        Assert.True(list.All(fun card -> cards.Contains(card)))
