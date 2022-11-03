namespace AdventOfCode.Year2018.Day23.Geometry;

static class Math
{
	public static int ManhattanDistance(Point point1, Point point2)
	{
		return System.Math.Abs(point1.X - point2.X)
			+ System.Math.Abs(point1.Y - point2.Y)
			+ System.Math.Abs(point1.Z - point2.Z);
	}
}
