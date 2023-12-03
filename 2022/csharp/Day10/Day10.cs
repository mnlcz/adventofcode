using Utils;

var input = Parser.IntoArrayMultiSeparator("10", "\n", " ");
Part1(input);
Part2(input);

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

static void Part2(List<List<string>> input)
{
	var spriteMidPx = 1;
	var row = "";
	var crt = new List<string>();
	foreach (var l in input)
	{
		if (l is [_, var n])
			Addx2(crt, ref row, ref spriteMidPx, int.Parse(n));
		else
			Noop2(crt, ref row, ref spriteMidPx);
	}
	Console.WriteLine("Part 2:");
	Render(crt);
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
	if (cycle == prev + 40)
	{
		signals.Add(cycle * x);
		prev = cycle;
	}
}

static void Addx2(List<string> crt, ref string row, ref int spriteMidPx, int n)
{
	Draw(ref row, spriteMidPx);
	CheckRow(crt, ref row);
	Draw(ref row, spriteMidPx);
	CheckRow(crt, ref row);
	spriteMidPx += n;
}

static void Noop2(List<string> crt, ref string row, ref int spriteMidPx)
{
	Draw(ref row, spriteMidPx);
	CheckRow(crt, ref row);
}

static void Draw(ref string row, int sprite) =>
	row += IsRowInSprite(row.Length, sprite) ? "#" : ".";

static bool IsRowInSprite(int i, int sprite) =>
	sprite == i || sprite - 1 == i || sprite + 1 == i;

static void CheckRow(List<string> crt, ref string row)
{
	if (row.Length == 40)
	{
		crt.Add(row);
		row = "";
	}
}

static void Render(List<string> crt)
{
	foreach (var l in crt)
	{
		foreach (var c in l)
			Console.Write(c == '#' ? "#" : " ");
		Console.WriteLine();
	}
}
