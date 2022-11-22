namespace AdventOfCode.Year2018.Day25.Geometry;

static class Geometry
{
	public static int ManhattanDistance(Point a, Point b)
	{
		return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z) + Math.Abs(a.W - b.W);
	}
}
