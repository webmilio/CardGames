using Common;

namespace Games;

public interface IScoreStrategy
{
    public int Score(ScoringContext context);

    public int Score(Card card, int current);

    public int ScoreLimit { get; }
}

public record ScoringContext(IList<Card> Deck, FaceState Facing = FaceState.Up);

public sealed class ScoreStrategy
{
    public class Default : IScoreStrategy
    {
        public virtual int ScoreLimit { get; set; }

        public virtual int Score(ScoringContext context)
        {
            int total = 0;

            for (int i = 0; i < context.Deck.Count; i++)
            {
                var card = context.Deck[i];

                if (card.Facing.HasFlag(context.Facing))
                {
                    total += Score(context.Deck[i], total);
                }
            }

            return total;
        }

        public virtual int Score(Card card, int current)
        {
            switch (card.Rank)
            {
                case Ranks.Ace: return 1;
                case Ranks.Two: return 2;
                case Ranks.Three: return 3;
                case Ranks.Four: return 4;
                case Ranks.Five: return 5;
                case Ranks.Six: return 6;
                case Ranks.Seven: return 7;
                case Ranks.Eight: return 8;
                case Ranks.Nine: return 9;
                case Ranks.Ten:

                case Ranks.Jack:
                case Ranks.Queen:
                case Ranks.King: return 10;

                default: return 0;
            }
        }
    }
}