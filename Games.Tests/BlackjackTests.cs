using Common;
using Games.Blackjacks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Tests;

[TestClass]
public class BlackjackTests
{
    [DataTestMethod]
    [DataRow(new object[] { 
        new Ranks[] { Ranks.Queen, Ranks.King },
        new Ranks[] { Ranks.Nine, Ranks.Two },
        false
    }, DisplayName = "Dealer Wins")]
    [DataRow(new object[] {
        new Ranks[] { Ranks.Queen, Ranks.Four },
        new Ranks[] { Ranks.Nine, Ranks.Ten },
        true
    }, DisplayName = "Player Wins")]
    [DataRow(new object?[] {
        new Ranks[] { Ranks.Queen, Ranks.Four },
        new Ranks[] { Ranks.Queen, Ranks.Four },
        null
    }, DisplayName = "Tie")]
    [DataRow(new object?[] {
        new Ranks[] { Ranks.Queen, Ranks.Four },
        new Ranks[] { Ranks.Queen, Ranks.Four, Ranks.Queen },
        false
    }, DisplayName = "Bust, Dealer Wins")]
    [DataRow(new object?[] {
        new Ranks[] { Ranks.Queen, Ranks.Four, Ranks.Queen },
        new Ranks[] { Ranks.Queen, Ranks.Four },
        true
    }, DisplayName = "Bust, Player Wins")]
    public void Winning(Ranks[] dealer, Ranks[] player, bool? result)
    {
        var game = new Blackjack();

        var dDealer = Make(dealer);
        var dPlayer = Make(player);

        var mResult = game.DetermineWinner(dPlayer, dDealer);

        Assert.AreEqual(result, mResult);
    }

    [DataTestMethod]
    [DataRow(new object[] { new Ranks[] { Ranks.Queen, Ranks.Ace }, 21 })]
    [DataRow(new object[] { new Ranks[] { Ranks.Queen, Ranks.Ace, Ranks.Ace }, 12 })]
    [DataRow(new object[] { new Ranks[] { Ranks.Queen, Ranks.Ace, Ranks.Two }, 13 })]
    public void Scoring(Ranks[] ranks, int expected)
    {
        var scoring = new Blackjack.BlackjackScoring();

        var deck = Make(ranks);
        var score = scoring.Score(new(deck));
        
        Assert.AreEqual(expected, score);
    }

    private static Deck Make(Ranks[] ranks)
    {
        var cards = new Card[ranks.Length];

        for (int i = 0; i < ranks.Length; i++)
        {
            cards[i] = new Card(ranks[i], Suits.Spades)
            {
                Facing = FaceState.Up
            };
        }

        return new Deck(cards);
    }
}
