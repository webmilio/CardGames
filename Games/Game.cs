using Common;

namespace Games;

public abstract class Game
{
    public record Action(Card[]? Play = null, int Draw = 0);
    public record ActionResult(bool Playing);

    public abstract IScoreStrategy Scoring { get; set; }

    public abstract ActionResult Act(Deck deck, Action action);

    public virtual int Score(ScoringContext context)
    {
        return Scoring.Score(context);
    }

}
