﻿namespace Utils;

public static class Parser
{
    public static string[] IntoArray(string inputName, string separator = "\n")
    {
        var input = File.ReadAllText($"../../inputs/{inputName}.txt");
        return input
            .Split(separator)
            .Where(l => !string.IsNullOrEmpty(l))
            .ToArray();
    }

    public static int[] IntoArrayInts(string inputName, string separator = "\n")
    {
        return Array.ConvertAll(IntoArray(inputName, separator), int.Parse);
    }

    public static List<List<string>> IntoArrayMultiSeparator(string inputName, string separator1, string separator2)
    {
        var input = IntoArray(inputName, separator1);
        var output = new List<List<string>>();
        foreach (var i in input)
            output.Add(i.Split(separator2).ToList());
        return output;
    }

    public static List<List<int>> IntoArrayMultiSeparatorInts(string inputName, string separator1, string separator2)
    {
        var input = IntoArrayMultiSeparator(inputName, separator1, separator2);
        var output = new List<List<int>>();
        foreach (var i in input)
            output.Add(i.Select(x => int.Parse(x)).ToList());
        return output;
    }

    public static int[] IntoSum(string inputName, string separator1, string separator2)
    {
        var input = IntoArrayMultiSeparatorInts(inputName, separator1, separator2);
        var output = new List<int>();
        foreach (var i in input)
            output.Add(i.Sum());
        return output.ToArray();
    }

    public static int[] Ints(string inputName, string separator) => 
        Array.ConvertAll(inputName.Split(separator), int.Parse);
    
    public static int[] Range(string inputName, string separator)
    {
        var input = Ints(inputName, separator);
        var (x, y) = (input[0], input[1]);
        return Enumerable.Range(x, y).ToArray();
    }
}

public static class Writer
{
    public static void Show<T>(T[] arr)
    {
        var t = typeof(T).Name;
        for (var i = 0; i < arr.Length; i++)
            Console.WriteLine($"[{i}] => ({t}): {arr[i]}");
    }

    public static void Show<T>(List<List<T>> list)
    {
        var t = typeof(T).Name;
        for (var i = 0; i < list.Count; i++)
        {
            Console.Write($"[{i}] => ({t}[]): ");
            foreach (var j in list[i])
                Console.Write($"{j} ");
            Console.WriteLine();
        }
    }
}