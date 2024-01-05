using System.Collections;
using System.Runtime.InteropServices;

namespace Common;

public class Deck : IList<Card>, IEquatable<Deck?>
{
    public class Standard : Deck
    {
        public Standard() 
        {
            foreach (var suit in Enum.GetValues<Suits>())
            {
                foreach (var rank in Enum.GetValues<Ranks>())
                {
                    Add(new Card()
                    {
                        Rank = rank,
                        Suit = suit,
                    });
                }
            }
        }
    }

    protected readonly List<Card> cards;

    public int Count => cards.Count;

    public bool IsReadOnly => false;

    public Card this[int index]
    {
        get => cards[index];
        set => cards[index] = value;
    }

    public Deck()
    {
        cards = [];
    }

    public Deck(IEnumerable<Card> cards)
    {
        this.cards = new List<Card>(cards);
    }

    public Deck(params Card[] cards)
    {
        this.cards = new List<Card>(cards);
    }

    public void Shuffle()
    {
        var span = CollectionsMarshal.AsSpan(cards);
        Random.Shared.Shuffle(span);
    }

    public void Move(int x, int y)
    {
        (this[y], this[x]) = (this[x], this[y]);
    }

    public Card Take()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Tried taking a card from an empty deck.");
        }

        var card = this[0];
        cards.RemoveAt(0);

        return card;
    }

    public void Deal(Deck target)
    {
        var card = Take();
        target.Add(card);
    }

    public void Deal(int count, params Deck[] targets)
    {
        for (int i = 0; i < count; i++)
        {
            var target = targets[i % targets.Length];
            Deal(target);
        }
    }

    public void Add(Card item)
    {
        cards.Add(item);
    }

    public int IndexOf(Card item)
    {
        return cards.IndexOf(item);
    }

    public void Insert(int index, Card item)
    {
        cards.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        cards.RemoveAt(index);
    }

    public bool Remove(Card item)
    {
        return cards.Remove(item);
    }

    public void Clear()
    {
        cards.Clear();
    }

    public bool Contains(Card item)
    {
        return cards.Contains(item);
    }

    public void CopyTo(Card[] array, int arrayIndex)
    {
        cards.CopyTo(array, arrayIndex);
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Deck);
    }

    public bool Equals(Deck? other)
    {
        return other is not null &&
                Enumerable.SequenceEqual(cards, other.cards) &&
               IsReadOnly == other.IsReadOnly;
    }

    public override int GetHashCode()
    {
        var cardsHash = new HashCode();

        for (int i = 0; i < Count; i++)
        {
            cardsHash.Add(this[i]);
        }

        return HashCode.Combine(cardsHash.ToHashCode(), IsReadOnly);
    }
}
