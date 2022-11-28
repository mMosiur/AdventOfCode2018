namespace AdventOfCode.Year2018.Day17.Geometry;

static class PointExtensions
{
	public static Point GetAbove(this Point point)
	{
		return new Point(point.X, point.Y - 1);
	}

	public static Point GetBelow(this Point point)
	{
		return new Point(point.X, point.Y + 1);
	}

	public static Point GetLeft(this Point point)
	{
		return new Point(point.X - 1, point.Y);
	}

	public static Point GetRight(this Point point)
	{
		return new Point(point.X + 1, point.Y);
	}
}
