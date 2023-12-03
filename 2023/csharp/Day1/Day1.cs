using Utils;

namespace Day1;

public sealed class Solution1 : ISolution
{
	private static string[] Input(string filename) => new Parser("2023").IntoArray(filename);

	public string Part1(string filename)
	{
		List<int> nums = [];
		var parsed = Input(filename);
		foreach (var l in parsed)
			nums.Add(GetNumber(l));

		return nums.Sum().ToString();
	}

	public string Part2(string filename)
	{
		List<int> nums = [];
		var parsed = Input(filename);
		foreach (var l in parsed)
			nums.Add(GetNumberAndString(l));

		return nums.Sum().ToString();
	}

	public static int GetNumber(string line)
	{
		var fst = line.ToList().Find(char.IsNumber);
		var snd = line.Reverse().ToList().Find(char.IsNumber);

		return int.Parse(fst + snd.ToString());
	}

	public static int GetNumberAndString(string line)
	{
		Dictionary<string, List<int>> idxs = [];
		foreach (var d in NumericString.Digits)
			idxs[d] = GetIndexes(line, d);
		foreach (var w in NumericString.Words)
			idxs[w] = GetIndexes(line, w);
		var nums = idxs.Where(x => x.Value.Count > 0).ToList();
		var fst = nums.MinBy(x => x.Value.Min()).Key;
		var snd = nums.MaxBy(x => x.Value.Max()).Key;
		fst = fst.Length > 1 ? NumericString.GetDigitFromString(fst) : fst;
		snd = snd.Length > 1 ? NumericString.GetDigitFromString(snd) : snd;

		return int.Parse(fst + snd);
	}

	public static List<int> GetIndexes(string line, string value)
	{
		List<int> indexes = [];
		var reps = 0;
		while (line.IndexOf(value, StringComparison.Ordinal) != -1)
		{
			var i = line.IndexOf(value, StringComparison.Ordinal);
			indexes.Add(i + reps);
			line = line[(i + value.Length)..];
			reps += i + value.Length;
		}

		return indexes;
	}
}
