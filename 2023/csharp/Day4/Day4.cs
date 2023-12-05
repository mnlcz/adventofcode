using Utils;

namespace Day4;

public sealed class Solution4 : ISolution
{
    private static string[][] Input(string filename)
    {
        var input = new Parser("2023").IntoArrayMultiSeparator(filename, "\n", " | ");
        RemoveCardNumbers(input);

        return input;
    }

    public string Part1(string filename)
    {
        var input = Input(filename);

        return TotalPoints(input).ToString();
    }

    public string Part2(string filename)
    {
        var input = Input(filename);
        var extraCopies = ExtraCopies(input);
        var totalCopies = TotalCopies(extraCopies);

        return totalCopies.Sum().ToString();
    }

    private static int[] TotalCopies(Span<int> extras)
    {
        List<int> copies = [];

        foreach (var _ in extras) copies.Add(1);

        for (var i = 0; i < extras.Length; i++)
        {
            var times = copies[i];
            var extra = extras[i];
            for (var j = 0; j < times; j++)
            for (var k = 0; k < extra; k++)
                copies[i + k + 1]++;
        }

        return [.. copies];
    }

    private static int[] ExtraCopies(Span<string[]> cards)
    {
        List<int> extraCopies = [];
        foreach (var card in cards) extraCopies.Add(WinningNumbers(card).Length);

        return [.. extraCopies];
    }

    private static int TotalPoints(Span<string[]> cards)
    {
        List<int> points = [];

        foreach (var card in cards)
        {
            var cWinners = WinningNumbers(card);
            var cPoints = CardPoints(cWinners);
            points.Add(cPoints);
        }

        return points.Sum();
    }

    private static void RemoveCardNumbers(string[][] input)
    {
        foreach (var c in input)
        {
            var clean = c[0].Split(": ")[1];
            c[0] = clean;
        }
    }

    private static int[] WinningNumbers(Span<string> card)
    {
        List<int> winningNumbers = [];
        var winners = card[0].Split(" ").Where(i => i != "").ToArray();
        var candidates = card[1].Split(" ").Where(i => i != "").ToArray();

        foreach (var c in candidates)
        foreach (var w in winners)
            if (c == w)
                winningNumbers.Add(int.Parse(c));

        return [.. winningNumbers];
    }

    private static int CardPoints(Span<int> winningNumbers)
    {
        if (winningNumbers.Length == 0) return 0;

        var points = 1;

        foreach (var _ in winningNumbers)
            points *= 2;

        return points / 2;
    }
}