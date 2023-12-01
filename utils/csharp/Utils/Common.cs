namespace Utils;

public record struct Point(int x, int y)
{
	public override readonly string ToString() => $"x: {x}, y: {y}";

	public static Point operator +(Point one, Point other) => new(one.x + other.x, one.y + other.y);

	public readonly int Chebyshev(Point other) => Math.Max(Math.Abs(x - other.x), Math.Abs(y - other.y));

	public readonly Point Move(Point direction) => this + direction;
}

public readonly record struct Movement
{
	public static Point Right { get; } = new(1, 0);
	public static Point Left { get; } = new(-1, 0);
	public static Point Up { get; } = new(0, 1);
	public static Point Down { get; } = new(0, -1);

	public static Point GetMove(Direction d) => d switch
	{
		Direction.Up => Up,
		Direction.Down => Down,
		Direction.Left => Left,
		Direction.Right => Right,
		_ => throw new ArgumentException("Invalid direction")
	};
}

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public static class NumericString
{
	public static IEnumerable<string> Words => ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

	public static IEnumerable<string> Digits => ["1", "2", "3", "4", "5", "6", "7", "8", "9"];

	public static int GetNumberFromString(string s) => s switch
	{
		"1" or "one" => 1,
		"2" or "two" => 2,
		"3" or "three" => 3,
		"4" or "four" => 4,
		"5" or "five" => 5,
		"6" or "six" => 6,
		"7" or "seven" => 7,
		"8" or "eight" => 8,
		"9" or "nine" => 9,
		_ => throw new ArgumentException($"Valor númerico de {s} inválido")
	};

	public static string GetDigitFromString(string s) => s switch
	{
		"one" => "1",
		"two" => "2",
		"three" => "3",
		"four" => "4",
		"five" => "5",
		"six" => "6",
		"seven" => "7",
		"eight" => "8",
		"nine" => "9",
		_ => throw new ArgumentException($"La string ${s} no posee un valor numerico valido")
	};
}
