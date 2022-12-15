namespace Utils;

public record struct Point(int x, int y)
{
    public override string ToString() => $"x: {x}, y: {y}";

    public static Point operator +(Point one, Point other) => new(one.x + other.x, one.y + other.y);

    public int Chebyshev(Point other) => Math.Max(Math.Abs(x - other.x), Math.Abs(y - other.y));

    public Point Move(Point direction) => this + direction;
}

public readonly record struct Direction
{
    public static Point Right { get; } = new(1, 0);
    public static Point Left { get; } = new(-1, 0);
    public static Point Up { get; } = new(0, 1);
    public static Point Down { get; } = new(0, -1);
}
