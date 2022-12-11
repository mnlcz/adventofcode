using Utils;

var input = Parser.IntoArrayMultiSeparator("9", "\n", " ").Select(s => (s[0], int.Parse(s[1])));
Part1(input);

static void Part1(IEnumerable<(string, int)> input)
{
    var rope = new Rope
    {
        Head = new Coordinate { X = 0, Y = 0 },
        Tail = new Coordinate { X = 0, Y = 0 },
        Visited = new HashSet<Coordinate>()
    };
    foreach (var move in input)
        for (var i = 0; i < move.Item2; i++)
            rope.Move(move.Item1[0]);
    Console.WriteLine($"Part 1: {rope.Visited.Count}");
}

file record struct Coordinate
{
    public required int X { get; init; }
    public required int Y { get; init; }

    public override string ToString()
    {
        return $"X: {X}\tY: {Y}";
    }

    public int Distance(Coordinate other)
    {
        return Math.Max(Math.Abs(X - other.X), Math.Abs(Y - other.Y));
    }
}

file sealed class Rope
{
    public required Coordinate Head { get; set; }
    public required Coordinate Tail { get; set; }
    public required HashSet<Coordinate> Visited { get; set; }

    public void Move(char direction)
    {
        (Head, Tail) = direction switch
        {
            'U' => Up(),
            'D' => Down(),
            'R' => Right(),
            'L' => Left(),
            _ => throw new ArgumentException("{direction} is an invalid direction")
        };
        Visited.Add(Tail);
    }

    private (Coordinate head, Coordinate tail) Up()
    {
        var h = Head with { Y = Head.Y + 1 };
        var t = Tail; 
        if (h.Distance(t) == 2 && (h.X == t.X || h.Y == t.Y))
        {
            t = Tail with { Y = Tail.Y + 1 };
        }
        else if (h.Distance(t) > 1 && h.X != t.X && h.Y != t.Y)
        {
            t = h.X > t.X
                ? Tail with { X = Tail.X + 1, Y = Tail.Y + 1 }
                : Tail with { X = Tail.X - 1, Y = Tail.Y + 1 };
        }
        return (h, t);
    }

    private (Coordinate head, Coordinate tail) Down()
    {
        var h = Head with { Y = Head.Y - 1 }; // assume Y != 0
        var t = Tail;
        if (h.Distance(t) == 2 && (h.X == t.X || h.Y == t.Y))
        {
            t = Tail with { Y = Tail.Y - 1 };
        }
        else if (h.Distance(t) > 1 && h.X != t.X && h.Y != t.Y)
        {
            t = h.X > t.X
                ? Tail with { X = Tail.X + 1, Y = Tail.Y - 1 }
                : Tail with { X = Tail.X - 1, Y = Tail.Y - 1 };
        }
        return (h, t);
    }

    private (Coordinate head, Coordinate tail) Right()
    {
        var h = Head with { X = Head.X + 1 };
        var t = Tail;
        if (h.Distance(t) == 2 && (h.X == t.X || h.Y == t.Y))
        {
            t = Tail with { X = Tail.X + 1 };
        }
        else if (h.Distance(t) > 1 && h.X != t.X && h.Y != t.Y)
        {
            t = h.Y > t.Y
                ? Tail with { X = Tail.X + 1, Y = Tail.Y + 1 }
                : Tail with { X = Tail.X + 1, Y = Tail.Y - 1 };
        }
        return (h, t);
    }

    private (Coordinate head, Coordinate tail) Left()
    {
        var h = Head with { X = Head.X - 1 };
        var t = Tail;
        if (h.Distance(t) == 2 && (h.X == t.X || h.Y == t.Y))
        {
            t = Tail with { X = Tail.X - 1 };
        }
        else if (h.Distance(t) > 1 && h.X != t.X && h.Y != t.Y)
        {
            t = h.Y > t.Y
                ? Tail with { X = Tail.X - 1, Y = Tail.Y + 1 }
                : Tail with { X = Tail.X - 1, Y = Tail.Y - 1 };
        }
        return (h, t);

    }

    public override string ToString()
    {
        return $"Head: {Head}\nTail: {Tail}\n";
    }
}
