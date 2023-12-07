using System.Collections.ObjectModel;
using Utils;
using Info = (int dRange, int sRange, int len);

namespace Day5;

public enum Type
{
    Seed,
    SeedToSoil,
    SoilToFertilizer,
    FertilizerToWater,
    WaterToLight,
    LightToTemperature,
    TemperatureToHumidity,
    HumidityToLocation
}

public sealed class Solution5 : ISolution
{
    private static Span<string> Input(string filename) => new Parser("2023").IntoArray(filename, "\n\n");

    public string Part1(string filename)
    {
        var input = Input(filename);
        var locations = Locations(input);

        return locations.Select(long.Parse).Min().ToString();
    }

    public string Part2(string filename)
    {
        throw new NotImplementedException();
    }

    private string[] Locations(ReadOnlySpan<string> input)
    {
        var maps = Mappings(input);
        var seeds = Types(input)[(int)Type.Seed];
        List<string> locations = [];

        foreach (var seed in seeds) locations.Add(WalkPath(seed, maps).Last());

        return locations.ToArray();
    }

    private string[] WalkPath(ReadOnlySpan<char> seed, ReadOnlyDictionary<string, string>[] mappings)
    {
        List<string> values = [];

        var prevRes = seed.ToString();
        for (var i = Type.SeedToSoil; i <= Type.HumidityToLocation; i++)
        {
            prevRes = CorrespondingDestination(prevRes, mappings[(int)i]);
            values.Add(prevRes);
        }

        return values.ToArray();
    }

    private string CorrespondingDestination(string origin, ReadOnlyDictionary<string, string> mapping) =>
        mapping.ContainsKey(origin) ? mapping[origin] : origin;

    private ReadOnlyDictionary<string, string>[] Mappings(ReadOnlySpan<string> input)
    {
        var types = Types(input).Slice(1);

        // Seeds has no mappings but its necessary to add the empty dict to the array so later you can
        // index the values by using the enum. maps[(int)Type.SeedToSoil]
        var seeds = new Dictionary<string, string>();
        List<ReadOnlyDictionary<string, string>> rodics = [new ReadOnlyDictionary<string, string>(seeds)];

        foreach (var t in types) rodics.Add(SingleTypeMapping(t));

        return rodics.ToArray();
    }

    private ReadOnlyDictionary<string, string> SingleTypeMapping(ReadOnlySpan<string> type)
    {
        var mappings = new Dictionary<string, string>();

        foreach (ReadOnlySpan<char> line in type)
        {
            var (dRangeStart, sRangeStart, rangeLen) = TypeInfo(line);
            var destinationRange = Enumerable.Range(dRangeStart, rangeLen).ToArray();
            var sourceRange = Enumerable.Range(sRangeStart, rangeLen).ToArray();

            for (var i = 0; i < rangeLen; i++) mappings[sourceRange[i].ToString()] = destinationRange[i].ToString();
        }

        return new(mappings);
    }

    private Info TypeInfo(ReadOnlySpan<char> entry)
    {
        var s = entry.ToString().Split(" ").AsSpan();

        return (int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
    }

    private ReadOnlySpan<string[]> Types(ReadOnlySpan<string> input) =>
        new[]
        {
            Get(Type.Seed, input),
            Get(Type.SeedToSoil, input),
            Get(Type.SoilToFertilizer, input),
            Get(Type.FertilizerToWater, input),
            Get(Type.WaterToLight, input),
            Get(Type.LightToTemperature, input),
            Get(Type.TemperatureToHumidity, input),
            Get(Type.HumidityToLocation, input)
        }.AsSpan();

    private string[] Get(Type type, ReadOnlySpan<string> input)
    {
        var separator = type == Type.Seed ? " " : "\n";

        return input[(int)type].Split(separator).AsSpan().Slice(1).ToArray();
    }
}
