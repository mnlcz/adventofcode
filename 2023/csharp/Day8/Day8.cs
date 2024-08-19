using Utils;
using System.Text.RegularExpressions;

namespace Day8;

public sealed class Solution8 : ISolution
{
    public (string, Dictionary<string, Node>) Parse(string filename)
    {
        var s = (new Parser("2023")).IntoArray(filename);
        var instructions = s[0];
        Dictionary<string, Node> nodes = [];

        foreach (var n in s.Skip(1))
        {
            var match = Regex.Match(n, @"(\w+)\s*=\s*\((\w+),\s*(\w+)\)");

            if (!match.Success)
                throw new Exception("Invalid input format");

            nodes.Add(match.Groups[1].Value, new Node(match.Groups[2].Value, match.Groups[3].Value));
        }

        return (instructions, nodes);
    }

    public string Part1(string filename)
    {
        var (directions, nodes) = Parse(filename);

        return Cycle(nodes, directions).ToString();
    }

    public string Part2(string filename)
    {
        throw new NotImplementedException();
    }

    public int Cycle(Dictionary<string, Node> nodes, string directions)
    {
        var element = nodes.First().Key;
        var instruction = nodes.First().Value;
        var counter = 0;

        while (true)
        {
            foreach (var d in directions)
            {
                element = d == 'L' ? nodes[element].left : nodes[element].right;
                instruction = nodes[element];
                counter++;

                if (element == "ZZZ")
                    return counter;
            }
        }

        throw new InvalidDataException();
    }
}

public record struct Node(string left, string right);
