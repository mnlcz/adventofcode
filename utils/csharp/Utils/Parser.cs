namespace Utils;

public static class Parser
{
    public static string Raw(string inputName) =>
        File.ReadAllText($"../../../../../inputs/{inputName}.txt");

    public static string[] IntoArray(string inputName, string separator = "\n")
    {
        var input = File.ReadAllText($"../../../../../inputs/{inputName}.txt");
        if (FixWindowsCarriageReturn(separator, input) is (true, var newSeparator))
            separator = newSeparator;

        return input
            .Split(separator)
            .Where(l => !string.IsNullOrEmpty(l))
            .ToArray();
    }

    public static T[] IntoArray<T>(string inputName, string separator = "\n") where T : IParsable<T>
    {
        var input = File.ReadAllText($"../../../../../inputs/{inputName}.txt");
        if (FixWindowsCarriageReturn(separator, input) is (true, var newSeparator))
            separator = newSeparator;

        return input
            .Split(separator)
            .Where(l => !string.IsNullOrEmpty(l))
            .Select(x => T.Parse(x, null))
            .ToArray();
    }

    public static List<List<string>> IntoArrayMultiSeparator(string inputName, string separator1, string separator2)
    {
        var input = IntoArray(inputName, separator1);
        var output = new List<List<string>>();

        foreach (var i in input)
            if (FixWindowsCarriageReturn(separator2, i) is (true, var newSeparator))
            {
                separator2 = newSeparator;
                break;
            }

        foreach (var i in input)
            output.Add(i.Split(separator2).ToList());

        return output;
    }

    public static List<List<T>> IntoArrayMultiSeparator<T>(string inputName, string separator1, string separator2)
        where T : IParsable<T>
    {
        var inputStr = IntoArray(inputName, separator1)!;
        var outputStr = new List<List<string>>();
        var output = new List<List<T>>();

        foreach (var i in inputStr)
            if (FixWindowsCarriageReturn(separator2, i) is (true, var newSeparator))
            {
                separator2 = newSeparator;
                break;
            }

        foreach (var i in inputStr)
            outputStr.Add(i.Split(separator2).ToList());

        foreach (var i in outputStr)
            output.Add(i.Select(x => T.Parse(x, null)).ToList());

        return output;
    }

    public static Dictionary<Point, char> IntoGrid(string inputName)
    {
        var input = IntoArray(inputName).Reverse().ToArray();
        var grid = new Dictionary<Point, char>();
        var l = input[0].Length;
        var h = input.Length;

        for (var i = 0; i < h; i++)
        {
            for (var j = 0; j < l; j++)
                grid[new Point(j, i)] = input[i][j];
        }

        return grid;
    }

    public static string After(string input, string token) => input.Split(token)[1];

    public static T After<T>(string input, string token) where T : IParsable<T> =>
        T.Parse(input.Split(token)[1], null);

    public static T[] After<T>(string input, string token, string collectionSeparator) where T : IParsable<T>
    {
        var str = input.Split(token)[1];
        return Nums<T>(str, collectionSeparator);
    }

    public static string Before(string input, string token) => input.Split(token)[0];

    public static T Before<T>(string input, string token) where T : IParsable<T> =>
        T.Parse(input.Split(token)[0], null);

    public static T[] Before<T>(string input, string token, string collectionSeparator) where T : IParsable<T>
    {
        var str = input.Split(token)[0];
        return Nums<T>(str, collectionSeparator);
    }

    public static string Between(string input, string leftToken, string rightToken) =>
        After(Before(input, rightToken), leftToken);

    public static T Between<T>(string input, string leftToken, string rightToken) where T : IParsable<T>
    {
        var bef = input.Split(rightToken)[0];
        return After<T>(bef, leftToken);
    }

    public static T[] Between<T>(string input, string leftToken, string rightToken, string collectionSeparator)
        where T : IParsable<T>
    {
        var bef = input.Split(rightToken)[0];
        return After<T>(bef, leftToken, collectionSeparator);
    }

    public static int[] IntoSum(string inputName, string separator1, string separator2)
    {
        var input = IntoArrayMultiSeparator<int>(inputName, separator1, separator2);
        var output = new List<int>();

        foreach (var i in input)
            output.Add(i.Sum());

        return output.ToArray();
    }

    public static T[] Nums<T>(string input, string separator) where T : IParsable<T> =>
        input
        .Split(separator)
        .Select(x => T.Parse(x, null))
        .ToArray();

    public static int[] Range(string inputName, string separator)
    {
        var input = Nums<int>(inputName, separator);
        var (x, y) = (input[0], input[1]);

        return Enumerable.Range(x, y).ToArray();
    }

    private static (bool, string) FixWindowsCarriageReturn(string separator, string input) =>
        input.Contains("\r\n") && separator.Contains('\n') && !separator.Contains('\r')
            ? (true, separator.Replace("\n", "\r\n"))
            : (false, separator);
}
