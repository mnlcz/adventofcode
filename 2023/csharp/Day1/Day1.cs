using Utils;

namespace Day1;

public class Solution1 : ISolution
{
	private static string[] Input(bool useSample, string sample) => Parser.IntoArray(useSample ? sample : "1");

	public string Part1(bool useSample = false, string sampleName = "")
	{
		List<int> nums = [];
		var parsed = Input(useSample, sampleName);
		foreach (var l in parsed)
			nums.Add(GetNumber(l));

		return nums.Sum().ToString();
	}

	public string Part2(bool useSample = false, string sampleName = "")
	{
		List<int> nums = [];
		var parsed = Input(useSample, sampleName);
		foreach (var l in parsed)
			nums.Add(GetNumberAndString(l));

		return nums.Sum().ToString();
	}

	public static int GetNumber(string line)
	{
		var fst = line.ToList().Find(char.IsNumber);
		var snd = line.Reverse().ToList().Find(char.IsNumber);

		return int.Parse(fst.ToString() + snd.ToString());
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
		while (line.IndexOf(value) != -1)
		{
			var i = line.IndexOf(value);
			indexes.Add(i + reps);
			line = line[(i + value.Length)..];
			reps += i + value.Length;
		}

		return indexes;
	}
}
