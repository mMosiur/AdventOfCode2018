namespace AdventOfCode.Year2018.Day22.Geometry;

readonly struct Area : IEquatable<Area>
{
	public Range XRange { get; }
	public Range YRange { get; }

	public int Width => XRange.Length;
	public int Height => YRange.Length;

	public Coordinate Origin => new(XRange.Start, YRange.Start);

	public Area(Range xRange, Range yRange)
	{
		XRange = xRange;
		YRange = yRange;
	}

	public Area(int xStart, int xEnd, int yStart, int yEnd)
		: this(new Range(xStart, xEnd), new Range(yStart, yEnd))
	{
	}

	public bool Contains(Coordinate coordinate) => XRange.Contains(coordinate.X) && YRange.Contains(coordinate.Y);
	public bool Contains(Area area) => XRange.Contains(area.XRange) && YRange.Contains(area.YRange);

	public IEnumerable<Coordinate> EnumerateCoordinates()
	{
		for (int y = YRange.Start; y <= YRange.End; y++)
		{
			for (int x = XRange.Start; x <= XRange.End; x++)
			{
				yield return new Coordinate(x, y);
			}
		}
	}

	public bool Equals(Area other) => XRange == other.XRange && YRange == other.YRange;
	public override bool Equals(object? obj) => obj is Area area && Equals(area);
	public override int GetHashCode() => HashCode.Combine(XRange, YRange);
	public static bool operator ==(Area left, Area right) => left.Equals(right);
	public static bool operator !=(Area left, Area right) => !(left == right);
}
