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
		static bool isSymbol(char c) => !char.IsNumber(c) && c != '.';
		for (var i = 0; i < input.Length; i++)
		{
			for (var j = 0; j < input[i].Length; j++)
			{
				var character = input[i][j];
				if (char.IsNumber(character))
				{
					// Check part
					if (OutOfBoundsCheck.Up(i))
						if (isSymbol(input[i - 1][j])) isPart = true;
					if (OutOfBoundsCheck.Down(i, input.Length))
						if (isSymbol(input[i + 1][j])) isPart = true;
					if (OutOfBoundsCheck.Left(j))
						if (isSymbol(input[i][j - 1])) isPart = true;
					if (OutOfBoundsCheck.Right(j, input[i].Length))
						if (isSymbol(input[i][j + 1])) isPart = true;
					if (OutOfBoundsCheck.UpLeft(i, j))
						if (isSymbol(input[i - 1][j - 1])) isPart = true;
					if (OutOfBoundsCheck.UpRight(i, j, input[i].Length))
						if (isSymbol(input[i - 1][j + 1])) isPart = true;
					if (OutOfBoundsCheck.DownLeft(i, j, input.Length))
						if (isSymbol(input[i + 1][j - 1])) isPart = true;
					if (OutOfBoundsCheck.DownRight(i, j, input.Length, input[i].Length))
						if (isSymbol(input[i + 1][j + 1])) isPart = true;

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
		throw new NotImplementedException();
	}
}
