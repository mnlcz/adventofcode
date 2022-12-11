using Utils;

var input = Parser.IntoArrayMultiSeparator("9", "\n", " ").Select(s => (s[0][0], int.Parse(s[1])));
var h = new Point(0, 0);
var t = new Point(0, 0);
var directions = new Dictionary<char, Point>
{
    ['R'] = new(1, 0),
    ['U'] = new(0, 1),
    ['L'] = new(-1, 0),
    ['D'] = new(0, -1)
};
Part1(input);

void Part1(IEnumerable<(char, int)> input)
{
    var visited = new HashSet<Point> { t };
    foreach (var m in input)
    {
        var dir = directions[m.Item1];
        var n = m.Item2;
        for (var i = 0; i < n; i++)
        {
            Move(dir);
            visited.Add(t);
        }
    }
    Console.WriteLine($"Part 1: {visited.Count}");
}

static bool AreTouching(Point p1, Point p2)
{
    return p1.Chebyshev(p2) <= 1;
}

void Move(Point direction)
{
    h += direction;
    if (!AreTouching(h, t))
    {
        var newX = h.x == t.x ? 0 : (h.x - t.x) / Math.Abs(h.x - t.x);
        var newY = h.y == t.y ? 0 : (h.y - t.y) / Math.Abs(h.y - t.y);
        t.x += newX;
        t.y += newY;
    }
}

