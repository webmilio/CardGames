using Common;

namespace Games.Blackjacks;

public partial class Blackjack : Game
{
    /*
     * Evryp = Every player
     * c = card
     * fu = face up
     * fd = face down
     * 
     * 1. Evryp gets a c fu, visible to all
     * 2. Dealer gets a c fu
     * 3. Evryp gets a c fu
     * 4. Dealer gets a c fd
     * 5. Evryp (in order) gets the opportunity to either take another card (hitting) or to keep the current value (standing)
     *      A player can keep hitting in the same turn as long as they haven't busted.
     *      A player MUST end his turn by standing.
     *      The player's turn ends if they stand or go over 21 (busting). If they bust, they lose their wager.
     * Once all players have finished their turns:
     * 6. The dealer will reveal the fd c that they have (fd -> fu) and will have the opportunity to hit or stand, and may bust.
     *      However, the dealer MUST stand if their cards total 17 to 21. Once the dealer reaches these values or they bust,
     *      you compare and determine a winner (highest score) between Evryp left in the game and the dealer (if hasn't busted).
     * 
     * The winner gets paid 1-to-1, so if you've put 5$ down, you win 5$ and you now have 10$.
     * If you've tied with the dealer, you get your money back. If the dealer has a total closer to 21, you lose your wager.
     * 
     * Important Notes:
     * - You are only playing against the dealer, nobody else.
     * - If you are dealt an Ace and a 10 as your starting hand, you automatically win 1.5x. 
     *      If you've wagered 10$, you get back 15$ for a total of 25$.
     */

    public override IScoreStrategy Scoring { get; set; } = new BlackjackScoring();

    public int DealerStandScore { get; set; } = 17;
    public bool ForceStandOnWin { get; set; } = true;

    private Deck _pile;
    public readonly Deck dealer = [];

    public Blackjack()
    {
        _pile = new Deck.Standard();
        _pile.Shuffle();
    }

    public void Setup(Deck[] decks)
    {
        var mDecks = new Deck[decks.Length + 1];
        Array.Copy(decks, mDecks, decks.Length);

        mDecks[^1] = dealer;

        _pile.Deal(2 * mDecks.Length, mDecks);

        for (int i = 0; i < decks.Length; i++)
        {
            for (int j = 0; j < decks[i].Count; j++)
            {
                decks[i][j].Facing = FaceState.Up;
            }
        }

        dealer[0].Facing = FaceState.Up;
    }

    public override ActionResult Act(Deck deck, Action action)
    {
        if (action.Draw > 0)
        {
            _pile.Deal(deck);
            deck[^1].Facing = FaceState.Up;
        }

        var score = Score(new(deck, FaceState.Up));

        var standing = action.Draw < 1; // Standing
        var notStop = score < Scoring.ScoreLimit // Busted (inverted) 
            || score == Scoring.ScoreLimit && !ForceStandOnWin; // You've won or busted (but can keep playing depending on options)
        
        return new(!standing && notStop);
    }

    /// <returns>
    ///     <c>true</c> if the deck <see cref="player"/> has won,
    ///     <c>false</c> if the deck <see cref="dealer"/> won or
    ///     <c>null</c> if it's a tie.
    /// </returns>
    public bool? DetermineWinner(Deck player, Deck dealer)
    {
        var deckScore = Score(new(player));
        var dealerScore = Score(new(dealer));

        if (deckScore > Scoring.ScoreLimit)
        {
            return false;
        }

        if (deckScore == Scoring.ScoreLimit || 
            deckScore > dealerScore ||
            dealerScore > Scoring.ScoreLimit)
        {
            return true;
        }

        if (deckScore < dealerScore)
        {
            return false;
        }
        
        return null;
    }
}
