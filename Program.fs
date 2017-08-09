open System
open CardsSimulator
open CardsSimulator.Deck
open CardsSimulator.Poker

[<EntryPoint>]
let main argv =
    Console.WriteLine("Flop: {0} {1} {2}", 
        DescribeCard(game.Flop1),
        DescribeCard(game.Flop2),
        DescribeCard(game.Flop3))

    Console.WriteLine("============")
    Console.WriteLine("Turn: {0}", DescribeCard(game.Turn))
    Console.WriteLine("River: {0}", DescribeCard(game.River))
    Console.WriteLine("============")

    let printPlayer player =
        Console.WriteLine("{0} cards", player.Name)
        Console.WriteLine("Card1: {0}", DescribeCard(player.Card1))
        Console.WriteLine("Card2: {0}", DescribeCard(player.Card2))
        Console.WriteLine("============")

    game.Players |> Array.iter printPlayer

    0 // return an integer exit code
