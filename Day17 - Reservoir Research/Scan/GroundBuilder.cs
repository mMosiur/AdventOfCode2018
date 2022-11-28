using AdventOfCode.Year2018.Day17.Geometry;

namespace AdventOfCode.Year2018.Day17.Scan;

class GroundBuilder
{
	private readonly List<ILine> _veinsOfClay = new();
	private Point? _springOfWaterPosition = null;
	private readonly AreaBuilder _areaBuilder = new(1, 0);

	public Area CurrentArea => _areaBuilder.Build();
	public IReadOnlyCollection<ILine> VeinsOfClay => _veinsOfClay;
	public Point? SpringOfWaterPosition => _springOfWaterPosition;

	public GroundBuilder AddSpringOfWater(Point position)
	{
		if (_springOfWaterPosition is not null)
		{
			throw new InvalidOperationException("Spring of water position was already set.");
		}
		_springOfWaterPosition = position;
		_areaBuilder.AddPoint(position);
		return this;
	}

	public GroundBuilder AddVeinOfClay(ILine veinOfClay)
	{
		ArgumentNullException.ThrowIfNull(veinOfClay);
		_veinsOfClay.Add(veinOfClay);
		_areaBuilder.AddLine(veinOfClay);
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

	public Ground Build()
	{
		if (_springOfWaterPosition is null)
		{
			throw new InvalidOperationException("Spring of water position must be set.");
		}
		Area area = _areaBuilder.Build();
		GroundType[,] groundScan = new GroundType[area.GetWidth(), area.GetHeight()];
		for (int x = 0; x < area.GetWidth(); x++)
		{
			for (int y = 0; y < area.GetHeight(); y++)
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
