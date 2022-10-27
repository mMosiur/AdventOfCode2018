namespace AdventOfCode.Year2018.Day18.Geometry;

readonly record struct Point(int Y, int X)
{
	public Point TopLeft => new(Y - 1, X - 1);
	public Point Top => new(Y - 1, X);
	public Point TopRight => new(Y - 1, X + 1);
	public Point Left => new(Y, X - 1);
	public Point Right => new(Y, X + 1);
	public Point BottomLeft => new(Y + 1, X - 1);
	public Point Bottom => new(Y + 1, X);
	public Point BottomRight => new(Y + 1, X + 1);

	public IEnumerable<Point> EnumerateNeighborPoints()
	{
		yield return TopLeft;
		yield return Top;
		yield return TopRight;
		yield return Left;
		yield return Right;
		yield return BottomLeft;
		yield return Bottom;
		yield return BottomRight;
	}
}
