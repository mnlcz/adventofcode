using Utils;

namespace Day3;

public sealed class Solution3 : ISolution
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
		var input = Input(filename);
		List<int> ratios = [];
		for (var i = 0; i < input.Length; i++)
		{
			var line = input[i].AsSpan();
			for (var j = 0; j < line.Length; j++)
			{
				var character = input[i][j];
				if (character == '*')
				{
					(var isGear, var n1, var n2) = IsGear(input, i, j);
					if (isGear)
					{
						ratios.Add(n1 * n2);
					}
				}
			}
		}

		return ratios.Sum().ToString();
	}

	static bool IsSymbol(char c) => !char.IsNumber(c) && c != '.';

	static (bool, int, int) IsGear(Span<string> input, int i, int j)
	{
		var isGear = false;
		int n1 = -1, n2 = -1;
		var s1 = "";

		if (NotOutOfBoundsCheck.Up(i) && char.IsNumber(input[i - 1][j]))
		{
			if (NotOutOfBoundsCheck.Left(j - 1)) s1 += input[i - 1][j - 2];
			if (NotOutOfBoundsCheck.Left(j)) 
			{
				if (!char.IsNumber(input[i - 1][j - 1])) s1 = "";
				s1 += input[i - 1][j - 1]; 
			}
			s1 += input[i - 1][j];
			if (NotOutOfBoundsCheck.Right(j, input[0].Length)) s1 += input[i - 1][j + 1];
			if (NotOutOfBoundsCheck.Right(j + 1, input[0].Length))
				if (char.IsNumber(s1.Last())) s1 += input[i - 1][j + 2]; 
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
				n1 = int.Parse(string.Join("", ns));
			s1 = "";
		}
		if (NotOutOfBoundsCheck.Down(i, input.Length) && char.IsNumber(input[i + 1][j]))
		{
			if (NotOutOfBoundsCheck.Left(j - 1)) s1 += input[i + 1][j - 2];
			if (NotOutOfBoundsCheck.Left(j)) 
			{
				if (!char.IsNumber(input[i + 1][j - 1])) s1 = "";
				s1 += input[i + 1][j - 1]; 
			}
			s1 += input[i + 1][j];
			if (NotOutOfBoundsCheck.Right(j, input[0].Length)) s1 += input[i + 1][j + 1];
			if (NotOutOfBoundsCheck.Right(j + 1, input[0].Length)) 
				if (char.IsNumber(s1.Last())) s1 += input[i + 1][j + 2];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.Left(j) && char.IsNumber(input[i][j - 1]) && n2 == -1)
		{
			if (NotOutOfBoundsCheck.Left(j - 2)) s1 += input[i][j - 3];
			if (NotOutOfBoundsCheck.Left(j - 1)) s1 += input[i][j - 2];
			s1 += input[i][j - 1];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.Right(j, input[i].Length) && char.IsNumber(input[i][j + 1]) && n2 == -1)
		{
			s1 += input[i][j + 1];
			if (NotOutOfBoundsCheck.Right(j + 1, input[0].Length)) s1 += input[i][j + 2];
			if (NotOutOfBoundsCheck.Right(j + 2, input[0].Length)) s1 += input[i][j + 3];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.UpLeft(i, j) && char.IsNumber(input[i - 1][j - 1]) && n2 == -1)
		{
			if (NotOutOfBoundsCheck.Left(j - 2)) s1 += input[i - 1][j - 3];
			if (NotOutOfBoundsCheck.Left(j - 1)) s1 += input[i - 1][j - 2];
			s1 += input[i - 1][j - 1];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.UpRight(i, j, input[i].Length) && char.IsNumber(input[i - 1][j + 1]) && n2 == -1)
		{
			s1 += input[i - 1][j + 1];
			if (NotOutOfBoundsCheck.Right(j - 1, input[0].Length)) s1 += input[i - 1][j + 2];
			if (NotOutOfBoundsCheck.Right(j - 2, input[0].Length)) s1 += input[i - 1][j + 3];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.DownLeft(i, j, input.Length) && char.IsNumber(input[i + 1][j - 1]) && n2 == -1)
		{
			if (NotOutOfBoundsCheck.Left(j - 2)) s1 += input[i + 1][j - 3];
			if (NotOutOfBoundsCheck.Left(j - 1)) s1 += input[i + 1][j - 2];
			s1 += input[i + 1][j - 1];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (NotOutOfBoundsCheck.DownRight(i, j, input.Length, input[i].Length) && char.IsNumber(input[i + 1][j + 1]) && n2 == -1)
		{
			s1 += input[i + 1][j + 1];
			if (NotOutOfBoundsCheck.Right(j + 1, input[0].Length)) s1 += input[i + 1][j + 2];
			if (NotOutOfBoundsCheck.Right(j + 2, input[0].Length)) s1 += input[i + 1][j + 3];
			var ns = s1.Where(c => char.IsNumber(c));
			if (ns.Any())
			{
				if (n1 == -1)
					n1 = int.Parse(string.Join("", ns));
				else
					n2 = int.Parse(string.Join("", ns));
			}
			s1 = "";
		}
		if (n2 != -1) isGear = true;

		return (isGear, n1, n2);
	}
}
