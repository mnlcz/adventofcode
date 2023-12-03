using Utils;

var input = Parser.IntoArray("11", "\n\n").ToArray();
Part1(input);
Part2(input);

static void Part1(string[] input)
{
	var monkeys = CompleteMonkeys(input);

	for (var i = 0; i < 20; i++)
		foreach (var monkey in monkeys)
			monkey.Execute();

	Console.WriteLine($"Part 1: " + $"{monkeys
			.Select(m => m.Inspections)
			.OrderByDescending(i => i)
			.Take(2)
			.Aggregate((a, b) => a * b)}");
}

static void Part2(string[] input)
{
	var monkeys = CompleteMonkeys(input);
	var bigMod = 1L;

	foreach (var monkey in monkeys)
		bigMod *= monkey.DivisibleNumber;

	for (var i = 0; i < 10_000; i++)
		foreach (var monkey in monkeys)
			monkey.Execute(false, bigMod);

	Console.Write($"Part 2: " + $"{monkeys
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
	var number = Parser.Between<uint>(line[0], " ", ":");

	// Items
	var itemsList = Parser.After<long>(line[1], ": ", ", ");
	var items = new Queue<long>(itemsList);

	// Operation
	var split = Parser.After(line[2], "old ").Split(" ");
	Func<long, long> operation = (split[0], split[1]) switch
	{
		("*", "old") => (long other) => other * other,
		("+", var n) => (long other) => other + long.Parse(n),
		("*", var n) => (long other) => other * long.Parse(n),
		_ => throw new Exception("u fucked up bro")
	};

	// Test
	var divisor = Parser.After<int>(line[3], "by ");
	var reciever1 = Parser.After<int>(line[4], "key ");
	var reciever2 = Parser.After<int>(line[5], "key ");

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

	public void Execute(bool worried = true, long bigMod = -1)
	{
		while (Items.Count > 0)
		{
			Inspections++;
			var worryLevel = Items.Dequeue();
			worryLevel = Operation(worryLevel);

			if (worried)
				GetBored(ref worryLevel);
			else
			{
				if (bigMod == -1)
					throw new ArgumentException($"{bigMod} should be changed");
				worryLevel %= bigMod;
			}

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