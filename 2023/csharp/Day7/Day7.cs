using Utils;

namespace Day7;

public sealed class Solution7 : ISolution
{
    private string[][] Parse(string filename)
    {
        Parser p = new("2023");
        var s = p.IntoArrayMultiSeparator(filename, "\n", " ");

        return s;
    }

    public string Part1(string filename)
    {
        var input = Parse(filename);

        return TotalWinning(input);
    }

    public string Part2(string filename)
    {
        throw new NotImplementedException();
    }

    public string TotalWinning(string[][] rounds)
    {
        int w = 0;
        // Sort by Strongest() by Pair.X (hand)
        Array.Sort(rounds, (x, y) =>
        {
            var s = Strongest(x[0], y[0]);

            if (s == x[0])
                return 1;
            else if (s == y[0])
                return -1;
            else
                return 0;
        });

        // Winning per hand is rank * Pair.Y (bid)
        for (var i = 0; i < rounds.Length; i++)
        {
            var rank = i + 1;
            var bid = int.Parse(rounds[i][1]);
            w += rank * bid;
        }

        return w.ToString();
    }

    public string Strongest(string h1, string h2)
    {
        var t1 = InterpretHand(h1);
        var t2 = InterpretHand(h2);

        if (((int)t1) != ((int)t2))
        {
            return ((int)t1) < ((int)t2) ? h1 : h2;
        }

        for (var i = 0; i < h1.Length; i++)
        {
            var c1 = _cardStrength[h1[i]];
            var c2 = _cardStrength[h2[i]];

            if (h1[i] == h2[i])
                continue;

            return c1 < c2 ? h1 : h2;
        }

        return h1; // !!
    }

    public HandType InterpretHand(string hand)
    {
        List<int> cs = new();

        foreach (var i in _cardStrength.Keys)
        {
            var count = hand.Count(h => h == i);

            if (count == 5)
                return HandType.FiveOfKind;

            if (count == 4)
                return HandType.FourOfKind;

            if (count != 0 && count != 1)
                cs.Add(count);
        }

        cs.Sort();

        if (cs.Count == 2)
            return cs[0] == 3 || cs[1] == 3 ? HandType.FullHouse : HandType.TwoPair;

        if (cs.Count == 1)
            return cs[0] == 3 ? HandType.ThreeOfKind : HandType.OnePair;

        return HandType.HighCard;
    }

    // Lower = Stronger
    private readonly Dictionary<char, int> _cardStrength = new()
    {
        { 'A', 1 },
        { 'K', 2 },
        { 'Q', 3 },
        { 'J', 4 },
        { 'T', 5 },
        { '9', 6 },
        { '8', 7 },
        { '7', 8 },
        { '6', 9 },
        { '5', 10 },
        { '4', 11 },
        { '3', 12 },
        { '2', 13 },
    };

    // Lower = Stronger
    public enum HandType
    {
        FiveOfKind,
        FourOfKind,
        FullHouse,
        ThreeOfKind,
        TwoPair,
        OnePair,
        HighCard,
    }
}
