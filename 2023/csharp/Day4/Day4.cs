using Utils;

namespace Day4;

public sealed class Solution4 : ISolution
{
    private static string[][] Input(string filename) =>
        new Parser("2023").IntoArrayMultiSeparator(filename, "\n", " | ");

    public string Part1(string filename)
    {
        List<int> points = [];
        var input = Input(filename);
        RemoveCardNumbers(input);

        foreach (var card in input)
        {
            var cWinners = WinningNumbers(card);
            var cPoints = TotalPoints(cWinners);
            points.Add(cPoints);
        }

        return points.Sum().ToString();
    }

    public string Part2(string filename)
    {
        throw new NotImplementedException();
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
        var winners = card[0].Split(" ").Where(i => i != "");
        var candidates = card[1].Split(" ").Where(i => i != "");

        foreach (var c in candidates)
        foreach (var w in winners)
            if (c == w)
                winningNumbers.Add(int.Parse(c));

        return [.. winningNumbers];
    }

    private static int TotalPoints(Span<int> winningNumbers)
    {
        if (winningNumbers.Length == 0) return 0;

        var points = 1;

        foreach (var _ in winningNumbers)
        {
            points *= 2;
        }

        return points / 2;
    }
}
