using Utils;

namespace Day3;

public sealed class Solution3 : ISolution
{
	private string[] Input(string filename)
	{
		return new Parser("2023").IntoArray(filename);
	}

	public string Part1(string filename)
	{
		var input = Input(filename);
		Writer.Show(input);
		return "TODO";
	}

	public string Part2(string filename)
	{
		throw new NotImplementedException();
	}
}
