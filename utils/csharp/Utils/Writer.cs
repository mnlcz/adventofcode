namespace Utils;

public static class Writer
{
	public static void Show<T>(IEnumerable<T> arr)
	{
		var t = typeof(T).Name;
		var i = 0;
		foreach (var v in arr)
		{
			Console.WriteLine($"[{i}] => ({t}): {v}");
			i++;
		}
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

	public static void Show<T>(T[,] matrix)
	{
		var h = matrix.GetLength(0);
		var l = matrix.GetLength(1);
		for (var i = 0; i < h; i++)
		{
			for (var j = 0; j < l; j++)
				Console.Write(matrix[i, j] + " ");
			Console.WriteLine();
		}
	}

	public static void Show(Dictionary<Point, char> grid)
	{
		var h = grid
			.Keys
			.Select(k => k.y)
			.Max();

		var l = grid
			.Keys
			.Select(k => k.x)
			.Max();

		for (var i = h; i >= 0; i--)
		{
			for (var j = 0; j <= l; j++)
				Console.Write(grid[new Point(j, i)] + " ");
			Console.WriteLine();
		}
	}

	public static void Solution(ISolution solution)
	{
		Console.WriteLine($"Part 1: {solution.Part1("1")}");
		Console.WriteLine($"Part 2: {solution.Part2("1")}");
	}
}
