namespace AdventOfCode.Year2018.Day17.Geometry;

readonly struct Area
{
	public Range XRange { get; }
	public Range YRange { get; }

	public int Width => XRange.Length;
	public int Height => YRange.Length;

	public Area(Range xRange, Range yRange)
	{
		XRange = xRange;
		YRange = yRange;
	}

	public Area(int xStart, int xEnd, int yStart, int yEnd) : this(new Range(xStart, xEnd), new Range(yStart, yEnd))
	{
	}

	public Area(Point topLeftCorner, int width, int height) : this(topLeftCorner.X, topLeftCorner.X + width - 1, topLeftCorner.Y, topLeftCorner.Y + height - 1)
	{
	}

	public bool Contains(Point point) => XRange.Contains(point.X) && YRange.Contains(point.Y);

	public bool Contains(Area area) => XRange.Contains(area.XRange) && YRange.Contains(area.YRange);

	public IEnumerable<Point> EnumeratePoints()
	{
		for (int x = XRange.Start; x <= XRange.End; x++)
		{
			for (int y = YRange.Start; y <= YRange.End; y++)
			{
				yield return new Point(x, y);
			}
		}
	}
}
