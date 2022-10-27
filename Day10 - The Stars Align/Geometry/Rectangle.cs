namespace AdventOfCode.Year2018.Day10.Geometry;

ref struct Rectangle
{
	public int MinX { get; }
	public int MinY { get; }
	public int MaxX { get; }
	public int MaxY { get; }

	public Rectangle(int minX, int minY, int maxX, int maxY)
	{
		if (minX >= maxX)
		{
			throw new ArgumentOutOfRangeException(nameof(minX), $"Must be less than {nameof(maxX)}");
		}
		if (minY >= maxY)
		{
			throw new ArgumentOutOfRangeException(nameof(minY), $"Must be less than {nameof(maxY)}");
		}
		MinX = minX;
		MinY = minY;
		MaxX = maxX;
		MaxY = maxY;
	}

	public Point TopLeft => new(MinX, MinY);
	public Point TopRight => new(MaxX, MinY);
	public Point BottomLeft => new(MinX, MaxY);
	public Point BottomRight => new(MaxX, MaxY);

	public int Width => MaxX - MinX + 1;
	public int Height => MaxY - MinY + 1;
}
