namespace Utils;

public record struct Point(int x, int y)
{
    public override string ToString() => $"x: {x}, y: {y}";

    public static Point operator +(Point one, Point other)
    {
        return new(one.x + other.x, one.y + other.y);
    }

    public int Chebyshev(Point other) => Math.Max(Math.Abs(x - other.x), Math.Abs(y - other.y));
}
