namespace Common;

[Flags]
public enum Suits : byte
{
    /// <summary>♠</summary>
    Spades = 0b0001,

    /// <summary>♣</summary>
    Clubs = 0b0010,

    /// <summary>♥</summary>
    Hearts = 0b0100,

    /// <summary>♦</summary>
    Diamond = 0b1000
}
