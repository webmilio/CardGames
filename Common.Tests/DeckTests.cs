using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests;

[TestClass]
public class DeckTests
{
    private readonly Card firstCard = new Card(Ranks.Four, Suits.Hearts);
    private Deck deck;

    [TestInitialize]
    public void Initialize()
    {
        deck = new Deck(firstCard, new(Ranks.Five, Suits.Clubs), new(Ranks.Jack, Suits.Diamond));
    }

    [TestMethod]
    public void Take()
    {
        var card = deck.Take();

        Assert.AreEqual(2, deck.Count);
        Assert.AreEqual(firstCard, card);
    }

    [TestMethod]
    public void Deal()
    {
        var hand = new Deck();
        deck.Deal(hand);

        Assert.AreEqual(2, deck.Count);
        Assert.AreEqual(1, hand.Count);

        Assert.AreEqual(firstCard, hand[0]);
    }

    [TestMethod]
    public void DealMany()
    {
        var pile = new Deck.Standard();
        
        var x = new Deck();
        var y = new Deck();
        var z = new Deck();

        pile.Deal(pile.Count, x, y, z);

        Assert.AreEqual(18, x.Count);
        Assert.AreEqual(17, y.Count);
        Assert.AreEqual(17, z.Count);
    }
}
