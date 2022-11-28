namespace AdventOfCode.Year2018.Day23.Geometry;

readonly struct Cuboid : IEquatable<Cuboid>
{
	public Range XRange { get; }
	public Range YRange { get; }
	public Range ZRange { get; }

	public int Volume => XRange.Length * YRange.Length * ZRange.Length;

	public Cuboid(Range xRange, Range yRange, Range zRange)
	{
		XRange = xRange;
		YRange = yRange;
		ZRange = zRange;
	}

	public IEnumerable<Point> Points
	{
		get
		{
			foreach (int x in XRange)
			{
				foreach (int y in YRange)
				{
					foreach (int z in ZRange)
					{
						yield return new Point(x, y, z);
					}
				}
			}
		}
	}

	public Point GetPointClosestToExternalPoint(Point point)
	{
		return new(
			x: Math.Clamp(point.X, XRange.Start, XRange.End),
			y: Math.Clamp(point.Y, YRange.Start, YRange.End),
			z: Math.Clamp(point.Z, ZRange.Start, ZRange.End)
		);
	}

	#region IEquatable<Cuboid>
	public bool Equals(Cuboid other) => XRange == other.XRange && YRange == other.YRange && ZRange == other.ZRange;
	public override bool Equals(object? obj) => obj is Cuboid cuboid && Equals(cuboid);
	public override int GetHashCode() => HashCode.Combine(XRange, YRange, ZRange);
	public static bool operator ==(Cuboid left, Cuboid right) => left.Equals(right);
	public static bool operator !=(Cuboid left, Cuboid right) => !left.Equals(right);
	#endregion
}
