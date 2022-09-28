using AdventOfCode.Year2018.Day17.Geometry;

namespace AdventOfCode.Year2018.Day17.Scan;

public class GroundBuilder
{
	private readonly List<ILine> _veinsOfClay;
	private Point? _springOfWaterPosition;

	public GroundBuilder()
	{
		_veinsOfClay = new List<ILine>();
		_springOfWaterPosition = null;
	}

	public GroundBuilder SetSpringOfWaterPosition(Point position)
	{
		_springOfWaterPosition = position;
		return this;
	}

	public GroundBuilder AddVeinOfClay(ILine veinOfClay)
	{
		ArgumentNullException.ThrowIfNull(veinOfClay);
		_veinsOfClay.Add(veinOfClay);
		return this;
	}

	public GroundBuilder AddVeinsOfClay(IEnumerable<ILine> veinsOfClay)
	{
		ArgumentNullException.ThrowIfNull(veinsOfClay);
		foreach (ILine veinOfClay in veinsOfClay)
		{
			AddVeinOfClay(veinOfClay);
		}
		return this;
	}

	private static Area GetGroundScanArea(Point springOfWaterPosition, IEnumerable<ILine> veinsOfClay)
	{
		ArgumentNullException.ThrowIfNull(veinsOfClay);
		int minX = springOfWaterPosition.X;
		int maxX = springOfWaterPosition.X;
		int minY = springOfWaterPosition.Y;
		int maxY = springOfWaterPosition.Y;
		foreach (ILine veinOfClay in veinsOfClay)
		{
			if (veinOfClay is VerticalLine verticalLine)
			{
				minX = Math.Min(minX, verticalLine.X);
				maxX = Math.Max(maxX, verticalLine.X);
				minY = Math.Min(minY, verticalLine.Y.Start);
				maxY = Math.Max(maxY, verticalLine.Y.End);
			}
			else if (veinOfClay is HorizontalLine horizontalLine)
			{
				minX = Math.Min(minX, horizontalLine.X.Start);
				maxX = Math.Max(maxX, horizontalLine.X.End);
				minY = Math.Min(minY, horizontalLine.Y);
				maxY = Math.Max(maxY, horizontalLine.Y);
			}
		}
		return new Area(minX, maxX, minY, maxY);
	}

	public Ground Build()
	{
		if (_springOfWaterPosition is null)
		{
			throw new InvalidOperationException("Spring of water position must be set.");
		}
		Area area = GetGroundScanArea(_springOfWaterPosition.Value, _veinsOfClay);
		GroundType[,] groundScan = new GroundType[area.Width, area.Height];
		for (int x = 0; x < area.Width; x++)
		{
			for (int y = 0; y < area.Height; y++)
			{
				groundScan[x, y] = GroundType.Sand;
			}
		}
		foreach (Point point in _veinsOfClay.SelectMany(v => v.Points))
		{
			groundScan[point.X - area.XRange.Start, point.Y - area.YRange.Start] = GroundType.Clay;
		}
		if (groundScan[_springOfWaterPosition.Value.X - area.XRange.Start, _springOfWaterPosition.Value.Y - area.YRange.Start] == GroundType.Clay)
		{
			throw new InvalidOperationException("Spring of water position contradicts with clay veins.");
		}
		groundScan[_springOfWaterPosition.Value.X - area.XRange.Start, _springOfWaterPosition.Value.Y - area.YRange.Start] = GroundType.WaterSpring;
		return new GroundInner(groundScan, area);
	}

	private class GroundInner : Ground
	{
		public GroundInner(GroundType[,] ground, Area area) : base(ground, area)
		{
		}
	}
}
