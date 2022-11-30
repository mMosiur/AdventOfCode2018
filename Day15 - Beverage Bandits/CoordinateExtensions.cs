namespace AdventOfCode.Year2018.Day15;

static class CoordinateExtensions
{
	public static IEnumerable<Coordinate> AdjacentCoordinates(this Coordinate coordinate)
	{
		yield return new Coordinate(coordinate.X - 1, coordinate.Y);
		yield return new Coordinate(coordinate.X + 1, coordinate.Y);
		yield return new Coordinate(coordinate.X, coordinate.Y - 1);
		yield return new Coordinate(coordinate.X, coordinate.Y + 1);
	}

	public static bool IsAdjacent(this Coordinate coordinate, Coordinate other)
	{
		return MathG.ManhattanDistance(coordinate, other) == 1;
	}
}
