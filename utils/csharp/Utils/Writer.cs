namespace Utils;

public static class Writer
{
    public static void Show<T>(IEnumerable<T> arr)
    {
        var t = typeof(T).Name;
        var i = 0;
        foreach (var v in arr)
        {
            Console.WriteLine($"[{i}] => ({t}): {v}");
            i++;
        }
    }

    public static void Show<T>(T[][] list)
    {
        var t = typeof(T).Name;
        for (var i = 0; i < list.Length; i++)
        {
            Console.Write($"[{i}] => ({t}[]): \n\t");
            for (var j = 0; j < list[i].Length; j++)
                Console.Write($"[{j}] => {list[i][j]}\n\t");
            Console.WriteLine();
        }
    }

    public static void Show<T>(T[,] matrix)
    {
        var h = matrix.GetLength(0);
        var l = matrix.GetLength(1);
        for (var i = 0; i < h; i++)
        {
            for (var j = 0; j < l; j++)
                Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }
    }

    public static void Show(Dictionary<Point, char> grid)
    {
        var h = grid
            .Keys
            .Select(k => k.y)
            .Max();

        var l = grid
            .Keys
            .Select(k => k.x)
            .Max();

        for (var i = h; i >= 0; i--)
        {
            for (var j = 0; j <= l; j++)
                Console.Write(grid[new Point(j, i)] + " ");
            Console.WriteLine();
        }
    }

    public static void Solution(ISolution solution, string filename)
    {
        Console.WriteLine($"Part 1: {solution.Part1(filename)}");
        Console.WriteLine($"Part 2: {solution.Part2(filename)}");
    }
}
