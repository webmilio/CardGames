using Common;
using System.Diagnostics;

namespace BruteForce;

internal class Program
{
    private static void Main(string[] args)
    {
        var x = new Deck.Standard();
        var y = new Deck.Standard();

        var sw = new Stopwatch();
        ulong count = 0;

        Console.WriteLine("Shufflin!");

        do
        {
            x.Shuffle();
            y.Shuffle();

            count++;
        }
        while (!x.Equals(y));

        sw.Stop();

        Console.WriteLine("Shuffled {0} times!", count);

        Console.WriteLine("Seconds: {0}", sw.Elapsed.Seconds);
        Console.WriteLine("Minutes: {0}", sw.Elapsed.Minutes);
        Console.WriteLine("Hours: {1}", sw.Elapsed.Hours);
    }
}
