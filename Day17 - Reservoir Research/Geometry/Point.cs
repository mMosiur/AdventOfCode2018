namespace AdventOfCode.Year2018.Day17.Geometry;

readonly struct Point
{
	public int X { get; }
	public int Y { get; }

	public Point Above => new(X, Y - 1);
	public Point Below => new(X, Y + 1);
	public Point Left => new(X - 1, Y);
	public Point Right => new(X + 1, Y);

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

	public bool Equals(Point other) => X == other.X && Y == other.Y;

	public override bool Equals(object? obj) => obj is Point point && Equals(point);

	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Point left, Point right) => left.Equals(right);

	public static bool operator !=(Point left, Point right) => !(left == right);

	public override string ToString() => $"(x={X}, y={Y})";
}
