using Utils;

var input = Parser.IntoArrayMultiSeparator("10", "\n", " ");
Part1(input);

static void Part1(List<List<string>> input)
{
    var x = 1;
    var cycle = 0;
    var signals = new List<int>();
    var prev = -20;
    foreach (var l in input)
    {
        if (l is [_, var n])
            Addx(ref x, ref cycle, int.Parse(n), signals, ref prev);
        else
            Noop(x, ref cycle, signals, ref prev);
    }
    Console.WriteLine($"Part 1: {signals.Sum()}");
}

static void Addx(ref int x, ref int cycle, int n, List<int> signals, ref int prev)
{
    cycle++;
    Check(x, cycle, signals, ref prev);
    cycle++;
    Check(x, cycle, signals, ref prev);
    x += n;
}

static void Noop(int x, ref int cycle, List<int> signals, ref int prev)
{
    cycle++;
    Check(x, cycle, signals, ref prev);
}

static void Check(int x, int cycle, List<int> signals, ref int prev)
{
    // Maybe limit also by signals.Count. In the example 6 entries are needed.
    if (cycle == prev + 40)
    {
        signals.Add(cycle * x);
        prev = cycle;
    }
}
