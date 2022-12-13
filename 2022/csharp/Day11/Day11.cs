using Utils;

var input = Parser.IntoArray("11", "\n\n").ToArray();
var monkeys = CompleteMonkeys(input);
Part1(monkeys);

static void Part1(List<Monkey> monkeys)
{
    for (var i = 0; i < 20; i++)
        foreach (var monkey in monkeys)
            monkey.Execute();
    Console.WriteLine($"Part 1: " + $"{monkeys
            .Select(m => m.Inspections)
            .OrderByDescending(i => i)
            .Take(2)
            .Aggregate((a, b) => a * b)}");
}

static List<Monkey> CompleteMonkeys(string[] input)
{
    var monkeys = new List<Monkey>();
    var idxs = new List<(int, int)>();
    foreach (var block in input)
    {
        var (m, i) = ParseOne(block);
        monkeys.Add(m);
        idxs.Add(i);
    }
    for (var i = 0; i < monkeys.Count; i++)
    {
        var (r1, r2) = idxs[i];
        monkeys[i].Reciever1 = monkeys[r1];
        monkeys[i].Reciever2 = monkeys[r2];
    }
    return monkeys;
}

static (Monkey unfinishedMonkey, (int, int) recieverIndexes) ParseOne(string block)
{
    var line = block.Split("\n");
    // Monkey number <=> Monkey index in collection
    var number = uint.Parse(line[0].Replace(":", "").Split(" ")[1]);

    // Items
    var items = new Queue<long>();
    var itemsUnparsed = line[1].Trim().Split(": ")[1].Split(", ");
    foreach (var i in itemsUnparsed)
        items.Enqueue(long.Parse(i));

    // Operation
    var split = line[2].Trim().Split("old ")[1].Split(" ");
    Func<long, long> operation = (split[0], split[1]) switch
    {
        ("*", "old") => (long other) => other * other,
        ("+", var n) => (long other) => other + long.Parse(n),
        ("*", var n) => (long other) => other * long.Parse(n),
        _ => throw new Exception("u fucked up bro")
    };

    // Test
    var divisor = int.Parse(line[3].Trim().Split("by ")[1]);
    var reciever1 = int.Parse(line[4].Trim().Split("key ")[1]);
    var reciever2 = int.Parse(line[5].Trim().Split("key ")[1]);

    // Unfinished monkey
    var monkey = new Monkey
    {
        Number = number,
        Items = items,
        DivisibleNumber = divisor,
        Operation = operation
    };

    return (monkey, (reciever1, reciever2));
}

file sealed class Monkey
{
    public required uint Number { get; init; }
    public required Queue<long> Items { get; init; }
    public required long DivisibleNumber { get; init; }
    public required Func<long, long> Operation { get; init; }
    public Monkey? Reciever1 { get; set; }
    public Monkey? Reciever2 { get; set; }
    public long Inspections { get; set; } = 0;

    public void Execute()
    {
        while (Items.Count > 0)
        {
            Inspections++;
            var worryLevel = Items.Dequeue();
            worryLevel = Operation(worryLevel);
            GetBored(ref worryLevel);

            if (worryLevel % DivisibleNumber == 0)
                Reciever1!.Items.Enqueue(worryLevel);
            else
                Reciever2!.Items.Enqueue(worryLevel);
        }
    }

    public override string ToString()
    {
        var number = $"Monkey {Number}:\n";
        var items = $"Items: {string.Join(" ", Items)}\n";
        var divisor = $"Divisor: {DivisibleNumber}\n";
        var m1 = $"Reciever if true: {Reciever1?.Number}\n";
        var m2 = $"Reciever if false: {Reciever2?.Number}\n";
        return number + items + divisor + m1 + m2 + "\n";
    }

    private static void GetBored(ref long worryLevel) => worryLevel /= 3;
}