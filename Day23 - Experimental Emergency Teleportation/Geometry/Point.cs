namespace AdventOfCode.Year2018.Day23.Geometry;

readonly struct Point : IEquatable<Point>
{
	public int X { get; }
	public int Y { get; }
	public int Z { get; }

	public Point(int x, int y, int z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	#region IEquatable<Point>
	public bool Equals(Point other) => X == other.X && Y == other.Y && Z == other.Z;
	public override bool Equals(object? obj) => obj is Point point && Equals(point);
	public override int GetHashCode() => HashCode.Combine(X, Y, Z);
	public static bool operator ==(Point left, Point right) => left.Equals(right);
	public static bool operator !=(Point left, Point right) => !(left == right);
	#endregion
}
