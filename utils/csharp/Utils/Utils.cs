namespace Utils;

public static class Parser
{
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

    public static int[] IntoArrayInts(string inputName, string separator = "\n") =>
        Array.ConvertAll(IntoArray(inputName, separator), int.Parse);

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

    public static List<List<int>> IntoArrayMultiSeparatorInts(string inputName, string separator1, string separator2)
    {
        var input = IntoArrayMultiSeparator(inputName, separator1, separator2);
        var output = new List<List<int>>();
        foreach (var i in input)
            output.Add(i.Select(x => int.Parse(x)).ToList());
        return output;
    }

    public static int[] IntoSum(string inputName, string separator1, string separator2)
    {
        var input = IntoArrayMultiSeparatorInts(inputName, separator1, separator2);
        var output = new List<int>();
        foreach (var i in input)
            output.Add(i.Sum());
        return output.ToArray();
    }

    public static int[] Ints(string inputName, string separator) => Array.ConvertAll(inputName.Split(separator), int.Parse);

    public static int[] Range(string inputName, string separator)
    {
        var input = Ints(inputName, separator);
        var (x, y) = (input[0], input[1]);
        return Enumerable.Range(x, y).ToArray();
    }

    private static (bool, string) FixWindowsCarriageReturn(string separator, string input) =>
        input.Contains("\r\n") && separator.Contains('\n') && !separator.Contains('\r')
            ? (true, separator.Replace("\n", "\r\n"))
            : (false, separator);
}

public static class Writer
{
    public static void Show<T>(T[] arr)
    {
        var t = typeof(T).Name;
        for (var i = 0; i < arr.Length; i++)
            Console.WriteLine($"[{i}] => ({t}): {arr[i]}");
    }

    public static void Show<T>(List<List<T>> list)
    {
        var t = typeof(T).Name;
        for (var i = 0; i < list.Count; i++)
        {
            Console.Write($"[{i}] => ({t}[]): ");
            foreach (var j in list[i])
                Console.Write($"{j} ");
            Console.WriteLine();
        }
    }
}

public record struct Point(int x, int y)
{
    public override string ToString() => $"x: {x}, y: {y}";

    public static Point operator +(Point one, Point other)
    {
        return new(one.x + other.x, one.y + other.y);
    }

    public int Chebyshev(Point other) => Math.Max(Math.Abs(x - other.x), Math.Abs(y - other.y));
}
