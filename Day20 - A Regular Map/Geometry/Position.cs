namespace AdventOfCode.Year2018.Day20.Geometry;

public readonly struct Position
{
	public int X { get; }
	public int Y { get; }

	public static Position Origin { get; } = new(0, 0);

	public Position(int x, int y)
	{
		X = x;
		Y = y;
	}

	public override string ToString() => $"Position({X}, {Y})";
}
