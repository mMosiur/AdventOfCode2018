namespace AdventOfCode.Year2018.Day23.Geometry;

static class ExtendedMath
{
	public static int ManhattanDistance(Point point1, Point point2)
	{
		return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y) + Math.Abs(point1.Z - point2.Z);
	}
}
