using Utils;

namespace Day1;

public class Solution1 : ISolution
{
	private static string[] Input(bool useSample, string sample)
		=> Parser.IntoArray(useSample ? sample : "1");

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

	private static int GetNumber(string line)
	{
		var fst = line.ToList().Find(char.IsNumber);
		var snd = line.Reverse().ToList().Find(char.IsNumber);

		return int.Parse(fst.ToString() + snd.ToString());
	}

	private static int GetNumberAndString(string line)
	{
		Dictionary<string, int> idxs = [];
		void getIndexes(IEnumerable<string> collection)
		{
			foreach (var s in collection)
			{
				var idx = line.IndexOf(s);
				if (idx != -1)
					idxs[s] = idx;
			}
		}
		getIndexes(NumericString.Words);
		getIndexes(NumericString.Digits);
		var fst = idxs.MinBy(x => x.Value).Key;
		var snd = idxs.MaxBy(x => x.Value).Key;
		fst = fst.Length > 1 ? NumericString.GetDigitFromString(fst) : fst;
		snd = snd.Length > 1 ? NumericString.GetDigitFromString(snd) : snd;

		return int.Parse(fst + snd);
	}
}
