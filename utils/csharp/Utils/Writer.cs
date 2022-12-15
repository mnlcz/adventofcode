namespace Utils;

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
