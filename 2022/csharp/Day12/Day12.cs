using Utils;

var input = Parser.IntoGrid("12");
var (graph, start, end) = Setup(input);
// TODO: BFS shortest path

static (Dictionary<Point, List<Point>>, Point, Point) Setup(Dictionary<Point, char> input) =>
(
    BuildGraph(input),
    input.Where(kv => kv.Value == 'S').Select(kv => kv.Key).First(),
    input.Where(kv => kv.Value == 'E').Select(kv => kv.Key).First()
);

static Dictionary<Point, List<Point>> BuildGraph(Dictionary<Point, char> grid)
{
    var graph = new Dictionary<Point, List<Point>>();
    var h = grid
        .Keys
        .Select(k => k.y)
        .Max();
    var l = grid
        .Keys
        .Select(k => k.x)
        .Max();

    var isValidNeighbour = (Point n) => 
        n.x >= 0 && n.x <= l && n.y >= 0 && n.y <= h;
    var isHigherElevation = (Point current, Point n) =>
        grid[current] == 'S' && grid[n] == 'a' ||
        grid[current] == 'E' && grid[n] == 'z' ||
        grid[current] == 'z' && grid[n] == 'E' ||
        grid[n] - grid[current] is 1 or 0;

    for (var i = 0; i <= h; i++)
    {
        for (var j = 0; j <= l; j++)
        {
            var neighbours = new List<Point>();
            var current = new Point(j, i);
            var moves = new[] { Movement.Up, Movement.Down, Movement.Left, Movement.Right };
            foreach (var m in moves)
            {
                var n = current.Move(m);
                if (isValidNeighbour(n) && isHigherElevation(current, n))
                    neighbours.Add(n);
            }
            graph[current] = neighbours;
        }
    }

    return graph;
}
