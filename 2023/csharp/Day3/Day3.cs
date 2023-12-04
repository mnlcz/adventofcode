using Utils;
using Coord = (int, int);
using NumCoord = (string, (int, int));

namespace Day3;

public sealed partial class Solution3 : ISolution
{
	private static string[] Input(string filename) => new Parser("2023").IntoArray(filename);

	public string Part1(string filename)
	{
		var input = Input(filename);
		List<int> nums = [];
		Span<char> num = ['.', '.', '.'];
		var isPart = false;
		for (var i = 0; i < input.Length; i++)
		{
			for (var j = 0; j < input[i].Length; j++)
			{
				var character = input[i][j];
				if (char.IsNumber(character))
				{
					if (NotOutOfBoundsCheck.Up(i))
						if (IsSymbol(input[i - 1][j])) isPart = true;
					if (NotOutOfBoundsCheck.Down(i, input.Length))
						if (IsSymbol(input[i + 1][j])) isPart = true;
					if (NotOutOfBoundsCheck.Left(j))
						if (IsSymbol(input[i][j - 1])) isPart = true;
					if (NotOutOfBoundsCheck.Right(j, input[i].Length))
						if (IsSymbol(input[i][j + 1])) isPart = true;
					if (NotOutOfBoundsCheck.UpLeft(i, j))
						if (IsSymbol(input[i - 1][j - 1])) isPart = true;
					if (NotOutOfBoundsCheck.UpRight(i, j, input[i].Length))
						if (IsSymbol(input[i - 1][j + 1])) isPart = true;
					if (NotOutOfBoundsCheck.DownLeft(i, j, input.Length))
						if (IsSymbol(input[i + 1][j - 1])) isPart = true;
					if (NotOutOfBoundsCheck.DownRight(i, j, input.Length, input[i].Length))
						if (IsSymbol(input[i + 1][j + 1])) isPart = true;

					if (num[0] == '.') num[0] = character;
					else if (num[1] == '.') num[1] = character;
					else num[2] = character;
				}
				else
				{
					if (num[0] == '.') continue;
					var str = num[0].ToString();
					if (num[1] != '.') str += num[1];
					if (num[2] != '.') str += num[2];
					if (isPart)
						nums.Add(int.Parse(str));
					isPart = false;
					num.Fill('.');
				}
			}
		}

		return nums.Sum().ToString();
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

	static bool IsSymbol(char c) => !char.IsNumber(c) && c != '.';

	private static long GetTotalRatio(Span<(int, int)> validGears)
	{
		long total = 0;

		foreach (var parts in validGears)
			total += parts.Item1 * parts.Item2;

		return total;
	}

	private static (int, int)[] GetValidGears(Span<Coord> gears, Span<NumCoord> nums)
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

	private static NumCoord[] GetNumsWithCoord(Span<string> lines)
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

	private static Coord[] GetGearsCoord(Span<string> lines)
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

	private static bool IsClose(Coord coord, NumCoord num)
	{
		var result = false;
		var digitCoords = GetDigitCoords(num);
		var surroundings = GetSurroundings(coord);

		foreach (var surr in surroundings)
			foreach (var dc in digitCoords)
				if (surr == dc) result = true;

		return result;
	}

	private static Coord[] GetDigitCoords(NumCoord num)
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

	private static Coord[] GetSurroundings(Coord coord)
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
