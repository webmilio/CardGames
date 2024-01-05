using Common;
using Games;
using Games.Blackjacks;

namespace Player;

internal class Program
{
    private static void Main(string[] args)
    {
        var blackjack = new Blackjack();
        var decks = new Deck[1];

        for (int i = 0; i < decks.Length; i++)
        {
            decks[i] = [];
        }

        blackjack.Setup(decks);
        PlayTurns(blackjack, decks);

        Console.WriteLine("### DEALER ###");
        PrintDeck(blackjack, blackjack.dealer, checkWin: false);

        for (int i = 0; i < decks.Length; i++)
        {
            Console.WriteLine("### PLAYER {0} ###", i);
            PrintDeck(blackjack, decks[i]);
        }
    }

    private static void PlayTurns(Blackjack game, Deck[] decks)
    {
        Console.WriteLine("Dealer's cards:");
        PrintDeck(game, game.dealer, checkWin: false);

        for (int i = 0; i < decks.Length; i++)
        {
            PlayPlayer(game, decks[i]);
        }

        PlayDealer(game);
    }

    private static void PlayPlayer(Blackjack game, Deck deck)
    {
        var score = game.Score(new(deck));
        Game.ActionResult result;

        if (score == game.Scoring.ScoreLimit)
        {
            return;
        }

        do
        {
            PrintDeck(deck);

            Console.WriteLine();
            Console.WriteLine("Score: {0}", score);

            Console.Write("Action (s, h) ? ");
            var key = Console.ReadKey();

            Console.WriteLine();

            bool hit = key.Key == ConsoleKey.H;
            result = game.Act(deck, new(Draw: hit ? 1 : 0));
        }
        while ((score = game.Score(new(deck))) < game.Scoring.ScoreLimit && result.Playing);
    }

    private static void PlayDealer(Blackjack game)
    {
        var deck = game.dealer;

        deck[1].Facing = FaceState.Up;
        var score = game.Score(new(deck));

        while (score < game.DealerStandScore)
        {
            PrintDeck(game, deck, checkWin: false);

            game.Act(deck, new(Draw: 1));
            score = game.Score(new(deck));
        }
    }

    private static void PrintDeck(Deck deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].Facing == FaceState.Up)
            {
                Console.Write("{0}", deck[i]);

                if (i + 1 < deck.Count)
                {
                    Console.Write(", ");
                }
            }
        }
    }

    private static void PrintDeck(Blackjack game, Deck deck, bool checkWin = true)
    {
        PrintDeck(deck);

        Console.WriteLine();
        Console.WriteLine("Total: {0}", game.Score(new(deck)));

        if (checkWin)
        {
            var result = game.DetermineWinner(deck, game.dealer);
            Console.WriteLine(result == null ? "Tie!" : (result.Value ? "Won!" : "Lost!"));
        }
    }
}
