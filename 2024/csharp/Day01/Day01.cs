namespace Day01;

public static class Solution
{
    private static string[] Input(string file)
    {
        return File.ReadAllLines($"../../../../inputs/{file}");
    }

    public static void Part1()
    {
        var input = Input("01.txt");
        var ll = new List<int>();
        var rl = new List<int>();

        foreach (var l in input)
        {
            var sp = l.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ll.Add(int.Parse(sp[0]));
            rl.Add(int.Parse(sp[1]));
        }

        if (ll.Count != rl.Count) throw new InvalidDataException();

        ll.Sort();
        rl.Sort();

        var sum = 0;

        for (var i = 0; i < ll.Count; i++)
        {
            var diff = ll[i] - rl[i];
            sum += diff < 0 ? -diff : diff;
        }

        Console.WriteLine("Part 1: " + sum);
    }

    public static void Part2()
    {
        var input = Input("01.txt");
        var ll = new List<string>();
        var rl = new Dictionary<string, int>();

        foreach (var l in input)
        {
            var spl = l.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var r = spl[1];
            ll.Add(spl[0]);
            
            if (rl.TryGetValue(r, out var value))
                rl[r] = value + 1;
            else
                rl.Add(r, 1);
        }
        
        var calculated = new Dictionary<string, int>();
        var total = 0;

        foreach (var left in ll)
        {
            if (calculated.TryGetValue(left, out var value))
                total += value;
            else
            {
                if (rl.TryGetValue(left, out var reps))
                {
                    var res = int.Parse(left) * reps;
                    total += res;
                    calculated[left] = res;
                }
                else
                    calculated[left] = 0;
            }
        }

        Console.WriteLine("Part 2: " + total);
    }
}