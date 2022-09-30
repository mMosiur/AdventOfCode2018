namespace AdventOfCode.Year2018.Day17.Geometry;

public readonly struct VerticalLine : ILine
{
	public int X { get; }
	public Range Y { get; }

	public VerticalLine(int x, Range y)
	{
		X = x;
		Y = y;
	}

	public VerticalLine(int x, int y1, int y2) : this(x, new Range(y1, y2))
	{
	}

	public IEnumerable<Point> Points
	{
		get
		{
			for (int y = Y.Start; y <= Y.End; y++)
			{
				yield return new Point(X, y);
			}
		}
	}

	public bool Contains(Point point) => X == point.X && Y.Contains(point.Y);

	public override string ToString() => $"[x={X}, y={Y}]";
}
