
namespace Common;

public class Card
{
    public Ranks Rank { get; set; }

    public Suits Suit { get; set; }

    public FaceState Facing { get; set; }

    public Card()
    {
        
    }

    public Card(Ranks rank, Suits suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Card card &&
               Suit == card.Suit &&
               Rank == card.Rank;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }
}
