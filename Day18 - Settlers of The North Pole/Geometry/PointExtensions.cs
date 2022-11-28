namespace AdventOfCode.Year2018.Day18.Geometry;

static class PointExtensions
{
	public static IEnumerable<Point> EnumerateNeighborPoints(this Point point)
	{
		yield return new(point.X - 1, point.Y - 1); // TopLeft;
		yield return new(point.X, point.Y - 1);     // Top;
		yield return new(point.X + 1, point.Y - 1); // TopRight;
		yield return new(point.X - 1, point.Y);     // Left;
		yield return new(point.X + 1, point.Y);     // Right;
		yield return new(point.X - 1, point.Y + 1); // BottomLeft;
		yield return new(point.X, point.Y + 1);     // Bottom;
		yield return new(point.X + 1, point.Y + 1); // BottomRight;
	}
}
