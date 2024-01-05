using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests;

[TestClass]
public class StandardDeckTests
{
    private readonly Deck _deck = new Deck.Standard();

    [TestMethod]
    public void IsComplete()
    {
        Assert.AreEqual(52, _deck.Count);
    }

    [TestMethod]
    public void Shuffle()
    {
        var x = new Deck.Standard();
        var y = new Deck.Standard();

        CollectionAssert.AreEqual(x.ToArray(), y.ToArray());

        y.Shuffle();

        CollectionAssert.AreNotEqual(x.ToArray(), y.ToArray());
    }

    [TestMethod]
    public void Move()
    {
        var x = new Deck.Standard();

        var a = x[0];
        var b = x[1];

        x.Move(0, 1);

        Assert.AreEqual(x[0], b);
        Assert.AreEqual(x[1], a);
    }

    [TestMethod]
    public void Equality()
    {
        var x = new Deck.Standard();
        var y = new Deck.Standard();

        x.Move(4, 44);
        y.Move(4, 44);

        Assert.AreEqual(x, y);
    }
}
