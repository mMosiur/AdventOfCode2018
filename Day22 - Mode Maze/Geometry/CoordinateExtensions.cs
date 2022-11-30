namespace AdventOfCode.Year2018.Day22.Geometry;

static class CoordinateExtensions
{
	public static IEnumerable<Coordinate> EnumerateAdjacent(this Coordinate coordinate)
	{
		yield return new(coordinate.X, coordinate.Y + 1);
		yield return new(coordinate.X + 1, coordinate.Y);
		yield return new(coordinate.X, coordinate.Y - 1);
		yield return new(coordinate.X - 1, coordinate.Y);
	}
}
