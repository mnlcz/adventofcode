using Utils;
using Coord = (int, int);
using NumCoord = (string, (int, int));

namespace Day3;

public sealed class Solution3 : ISolution
{
    public static string[] Input(string filename) => new Parser("2023").IntoArray(filename);

    public string Part1(string filename)
    {
        var lines = Input(filename);
        var nums = GetNumsWithCoord(lines);
        var parts = GetPartsCoord(lines);
        var valids = GetValidParts(parts, nums);
        var total = GetTotalParts(valids);

        return total.ToString();
    }

    public string Part2(string filename)
    {
        var lines = Input(filename);
        var nums = GetNumsWithCoord(lines);
        var gears = GetGearsCoord(lines);
        var valids = GetValidGears(gears, nums);
        var total = GetTotalRatio(valids);

        return total.ToString();
    }

    public static long GetTotalRatio(Span<(int, int)> validGears)
    {
        long total = 0;

        foreach (var parts in validGears)
            total += parts.Item1 * parts.Item2;

        return total;
    }

    public static long GetTotalParts(int[] validParts) => validParts.Sum();

    public static (int, int)[] GetValidGears(Span<Coord> gears, Span<NumCoord> nums)
    {
        List<(int, int)> valids = [];

        foreach (var gc in gears)
        {
            var counter = 0;
            List<string> candidates = [];
            foreach (var nc in nums)
            {
                var num = nc.Item1;

                if (IsClose(gc, nc))
                {
                    counter++;
                    candidates.Add(num);
                }
            }

            if (counter == 2)
                valids.Add((int.Parse(candidates[0]), int.Parse(candidates[1])));
        }

        return [.. valids];
    }

    public static int[] GetValidParts(Span<Coord> parts, Span<NumCoord> nums)
    {
        List<int> valids = [];

        foreach (var pc in parts)
        foreach (var nc in nums)
        {
            var num = nc.Item1;

            if (IsClose(pc, nc))
                valids.Add(int.Parse(num));
        }

        return [.. valids];
    }

    public static NumCoord[] GetNumsWithCoord(Span<string> lines)
    {
        var vlim = lines.Length;
        var hlim = lines[0].Length;
        var sNum = "";
        List<NumCoord> nums = [];

        for (var i = 0; i < vlim; i++)
        {
            var line = lines[i];
            var (ci, cj) = (-1, -1);
            for (var j = 0; j < hlim; j++)
            {
                var ch = line[j];
                if (char.IsDigit(ch))
                {
                    if (sNum == "") (ci, cj) = (i, j);
                    sNum += ch;
                    if (j == hlim - 1)
                        nums.Add((sNum, (ci, cj)));
                }
                else
                {
                    if (sNum.Length > 0)
                    {
                        nums.Add((sNum, (ci, cj)));
                        sNum = "";
                        (ci, cj) = (-1, -1);
                    }
                }
            }
        }

        return [.. nums];
    }

    public static Coord[] GetGearsCoord(Span<string> lines)
    {
        var vlim = lines.Length;
        var hlim = lines[0].Length;
        List<Coord> cs = [];

        for (var i = 0; i < vlim; i++)
        {
            var line = lines[i];
            for (var j = 0; j < hlim; j++)
            {
                var ch = line[j];
                if (ch == '*') cs.Add((i, j));
            }
        }

        return [.. cs];
    }

    public static Coord[] GetPartsCoord(Span<string> lines)
    {
        var vlim = lines.Length;
        var hlim = lines[0].Length;
        List<Coord> cs = [];

        for (var i = 0; i < vlim; i++)
        {
            var line = lines[i];
            for (var j = 0; j < hlim; j++)
            {
                var ch = line[j];
                if (ch != '.' && !char.IsNumber(ch)) cs.Add((i, j));
            }
        }

        return [.. cs];
    }

    public static bool IsClose(Coord coord, NumCoord num)
    {
        var result = false;
        var digitCoords = GetDigitCoords(num);
        var surroundings = GetSurroundings(coord);

        foreach (var surr in surroundings)
        foreach (var dc in digitCoords)
            if (surr == dc)
                result = true;

        return result;
    }

    public static Coord[] GetDigitCoords(NumCoord num)
    {
        var numLen = num.Item1.Length;
        var numI = num.Item2.Item1;
        var numJ = num.Item2.Item2;
        List<Coord> numCoords = [num.Item2];
        var counter = 1;

        while (counter != numLen)
        {
            numCoords.Add((numI, numJ + counter));
            counter++;
        }

        return [.. numCoords];
    }

    public static Coord[] GetSurroundings(Coord coord)
    {
        var (ci, cj) = (coord.Item1, coord.Item2);
        var up = (ci - 1, cj);
        var down = (ci + 1, cj);
        var left = (ci, cj - 1);
        var right = (ci, cj + 1);
        var upleft = (ci - 1, cj - 1);
        var upright = (ci - 1, cj + 1);
        var downleft = (ci + 1, cj - 1);
        var downright = (ci + 1, cj + 1);

        return [up, down, left, right, upleft, upright, downleft, downright];
    }
}
