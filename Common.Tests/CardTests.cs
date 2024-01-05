namespace Common.Tests;

[TestClass]
public class CardTests
{
    [TestMethod]
    public void Equality()
    {
        var x = new Card
        {
            Suit = Suits.Clubs,
            Rank = Ranks.Three
        };

        var y = new Card
        {
            Suit = Suits.Clubs,
            Rank = Ranks.Three
        };

        Assert.IsFalse(ReferenceEquals(x, y));
        Assert.AreEqual(x, y);
    }
}