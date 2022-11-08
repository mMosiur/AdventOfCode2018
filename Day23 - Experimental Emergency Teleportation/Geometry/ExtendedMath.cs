namespace AdventOfCode.Year2018.Day23.Geometry;

static class ExtendedMath
{
	public static int ManhattanDistance(Point point1, Point point2)
	{
		return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y) + Math.Abs(point1.Z - point2.Z);
	}

	public static Point GetCuboidPointClosestToPoint(Cuboid cuboid, Point point)
	{
		return new(
			x: Math.Clamp(point.X, cuboid.XRange.Start, cuboid.XRange.End),
			y: Math.Clamp(point.Y, cuboid.YRange.Start, cuboid.YRange.End),
			z: Math.Clamp(point.Z, cuboid.ZRange.Start, cuboid.ZRange.End)
		);
	}
}
