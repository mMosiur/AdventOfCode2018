namespace AdventOfCode.Year2018.Day22.Geometry;

readonly record struct Coordinate(int X, int Y)
{
	public IEnumerable<Coordinate> EnumerateAdjacent()
	{
		yield return new(X, Y + 1);
		yield return new(X + 1, Y);
		yield return new(X, Y - 1);
		yield return new(X - 1, Y);
	}
}
