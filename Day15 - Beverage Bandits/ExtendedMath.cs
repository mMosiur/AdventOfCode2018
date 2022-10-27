namespace AdventOfCode.Year2018.Day15;

static class ExtendedMath
{
	public static int ManhattanDistance(Coordinate coord1, Coordinate coord2)
	{
		return Math.Abs(coord1.X - coord2.X) + Math.Abs(coord1.Y - coord2.Y);
	}
}
