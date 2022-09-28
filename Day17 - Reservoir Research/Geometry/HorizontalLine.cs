namespace AdventOfCode.Year2018.Day17.Geometry;

public struct HorizontalLine : ILine
{
	public Range X { get; }
	public int Y { get; }

	public HorizontalLine(Range x, int y)
	{
		X = x;
		Y = y;
	}

	public HorizontalLine(int x1, int x2, int y) : this(new Range(x1, x2), y)
	{
	}

	public IEnumerable<Point> Points
	{
		get
		{
			for (int x = X.Start; x <= X.End; x++)
			{
				yield return new Point(x, Y);
			}
		}
	}

	public bool Contains(Point point) => X.Contains(point.X) && Y == point.Y;

	public override string ToString() => $"[x={X}, y={Y}]";
}
