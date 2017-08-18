module TestsSequence

open System
open Xunit
open CardsSimulator
open CardsSimulator.Deck
open TexasHoldemPoker

[<Fact>]
let ``Card from 10 to Ace should be a sequence`` () =
    let cards = 
        [| Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King 
           Suit.Clubs, Rank.Ace |]

    let actual = isSequence cards

    Assert.True actual

[<Fact>]
let ``Cards from Nine to King`` () =
    let cards = 
        [| Suit.Clubs, Rank.Nine
           Suit.Clubs, Rank.Ten
           Suit.Clubs, Rank.Jack
           Suit.Clubs, Rank.Queen 
           Suit.Clubs, Rank.King|]
    
    let actual = isSequence cards

    Assert.True actual

[<Fact>]
let ``Five cards without sequence should not be sequence`` () =
    let cards = 
        [| Suit.Clubs, Rank.Two
           Suit.Clubs, Rank.Nine
           Suit.Diamonds, Rank.Six
           Suit.Clubs, Rank.Six 
           Suit.Clubs, Rank.King|]
               
    let actual = isSequence cards

    Assert.False actual

[<Fact>]
let ``Five cards starting from Ace to five should be sequence`` () =
    let cards = 
        [| Suit.Clubs, Rank.Ace
           Suit.Clubs, Rank.Two
           Suit.Diamonds, Rank.Three
           Suit.Clubs, Rank.Four 
           Suit.Clubs, Rank.Five|]
               
    let actual = isSequence cards

    Assert.True actual