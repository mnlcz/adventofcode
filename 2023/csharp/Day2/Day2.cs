using Utils;
using Cube = (uint num, string color);

namespace Day2;

public sealed class Solution2 : ISolution
{
	private List<Game> Input(string filename)
	{
		var input = new Parser("2023").IntoArray(filename);
		List<Game> games = [];
		foreach (var l in input)
			games.Add(new Game(l));
		return games;
	}

	public string Part1(string filename)
	{
		var games = Input(filename);
		return games.Where(g => g.Possible(12, 13, 14)).Select(g => (int)g.Id).Sum().ToString();
	}

	public string Part2(string filename)
	{
		var games = Input(filename);
		return games.Select(g => (int)g.Power()).Sum().ToString();
	}
}

public sealed class Game
{
	public readonly uint Id;
	private readonly IEnumerable<GameSet> _sets;

	public Game(string line)
	{
		Id = uint.Parse(Parser.Between(line, "Game ", ":"));
		line = line.Split(": ")[1];
		var sSets = line.Split("; ");
		List<GameSet> list = [];
		foreach (var s in sSets)
			list.Add(new GameSet(s));
		_sets = list;
	}

	public bool Possible(uint r, uint g, uint b)
	{
		return _sets.All(s => s.Possible(r, g, b));
	}

	public (uint r, uint g, uint b) CubesNeeded()
	{
		uint r = 0, g = 0, b = 0;
		foreach (var s in _sets)
		{
			var (sR, sG, sB) = s.CubesNeeded();
			r = r < sR ? sR : r;
			g = g < sG ? sG : g;
			b = b < sB ? sB : b;
		}
		return (r, g, b);
	}

	public uint Power()
	{
		var (r, g, b) = CubesNeeded();
		return r * g * b;
	}

	public override string ToString()
	{
		return $"ID: {Id}\nSets: {string.Join("; ", _sets)}";
	}
}

public sealed class GameSet
{
	private readonly IEnumerable<Cube> _cubes;

	public GameSet(string text)
	{
		List<Cube> list = [];
		var sCubes = text.Split(", ");
		foreach (var s in sCubes)
		{
			var temp = s.Split(" ");
			var (n, c) = (uint.Parse(temp[0]), temp[1]);
			list.Add((n, c));
		}

		_cubes = list;
	}

	public bool Possible(uint r, uint g, uint b)
	{
		uint rCount = 0, gCount = 0, bCount = 0;
		if (_cubes.Any(c => c.color == "blue"))
			bCount = _cubes.First(c => c.color == "blue").num;
		if (_cubes.Any(c => c.color == "red"))
			rCount = _cubes.First(c => c.color == "red").num;
		if (_cubes.Any(c => c.color == "green"))
			gCount = _cubes.First(c => c.color == "green").num;
		return rCount <= r && gCount <= g && bCount <= b;
	}

	public (uint r, uint g, uint b) CubesNeeded()
	{
		var r = _cubes.Any(c => c.color == "red") ? _cubes.First(c => c.color == "red").num : 0;
		var g = _cubes.Any(c => c.color == "green") ? _cubes.First(c => c.color == "green").num : 0;
		var b = _cubes.Any(c => c.color == "blue") ? _cubes.First(c => c.color == "blue").num : 0;
		return (r, g, b);
	}

	public override string ToString()
	{
		return $"{string.Join(", ", _cubes)}";
	}
}