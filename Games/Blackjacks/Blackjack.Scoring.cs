using Common;

namespace Games.Blackjacks;

public partial class Blackjack
{
    public class BlackjackScoring : ScoreStrategy.Default
    {
        public override int ScoreLimit { get; set; } = 21;

        public override int Score(ScoringContext context)
        {
            var sorted = context.Deck.OrderByDescending(c => c.Rank).ToArray();
            context = new ScoringContext(sorted, Facing: context.Facing);

            return base.Score(context);
        }

        public override int Score(Card card, int current)
        {
            switch (card.Rank)
            {
                case Ranks.Ace:     
                    if (current + 11 > ScoreLimit)
                    {
                        if (current + 1 > ScoreLimit)
                        {
                            return -11 + 1 + 1;
                        }

                        return 1;
                    }
                    
                    return 11;
                case Ranks.Two:     return 2;
                case Ranks.Three:   return 3;
                case Ranks.Four:    return 4;
                case Ranks.Five:    return 5;
                case Ranks.Six:     return 6;
                case Ranks.Seven:   return 7;
                case Ranks.Eight:   return 8;
                case Ranks.Nine:    return 9;
                case Ranks.Ten:

                case Ranks.Jack:
                case Ranks.Queen:
                case Ranks.King:    return 10;

                default:            return 0;
            }
        }
    }
}
