namespace AdventOfCode.Year2018.Day17.Geometry;

class AreaBuilder
{
	private bool _initialized = false;
	private int _minX = int.MaxValue;
	private int _maxX = int.MinValue;
	private int _minY = int.MaxValue;
	private int _maxY = int.MinValue;

	public int XPadding { get; }
	public int YPadding { get; }

	public AreaBuilder(int xPadding, int yPadding)
	{
		if (xPadding < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(xPadding), "Padding must be non-negative.");
		}
		if (yPadding < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(yPadding), "Padding must be non-negative.");
		}
		XPadding = xPadding;
		YPadding = yPadding;
	}

	public AreaBuilder() : this(0, 0)
	{
	}

	public void AddPoint(Point point)
	{
		_minX = Math.Min(_minX, point.X - XPadding);
		_maxX = Math.Max(_maxX, point.X + XPadding);
		_minY = Math.Min(_minY, point.Y - YPadding);
		_maxY = Math.Max(_maxY, point.Y + YPadding);
		_initialized = true;
	}

	public void AddLine(ILine line)
	{
		if (line is VerticalLine verticalLine)
		{
			_minX = Math.Min(_minX, verticalLine.X - XPadding);
			_maxX = Math.Max(_maxX, verticalLine.X + XPadding);
			_minY = Math.Min(_minY, verticalLine.Y.Start - YPadding);
			_maxY = Math.Max(_maxY, verticalLine.Y.End + YPadding);
		}
		else if (line is HorizontalLine horizontalLine)
		{
			_minX = Math.Min(_minX, horizontalLine.X.Start - XPadding);
			_maxX = Math.Max(_maxX, horizontalLine.X.End + XPadding);
			_minY = Math.Min(_minY, horizontalLine.Y - YPadding);
			_maxY = Math.Max(_maxY, horizontalLine.Y + YPadding);
		}
		else
		{
			throw new ArgumentException("Unknown line type", nameof(line));
		}
		_initialized = true;
	}

	public Area Build()
	{
		if (!_initialized)
		{
			throw new InvalidOperationException("Nothing was added to AreaBuilder.");
		}
		return new Area(new(_minX, _maxX), new(_minY, _maxY));
	}
}
