using AdventOfCode.Year2018.Day25.Geometry;

namespace AdventOfCode.Year2018.Day25;

class ConstellationResolver
{
	private readonly Dictionary<Point, int?> _constellationMap;

	public IReadOnlyCollection<Point> Points => _constellationMap.Keys;

	public ConstellationResolver(IReadOnlyCollection<Point> points)
	{
		_constellationMap = points.ToDictionary(p => p, p => (int?)null);
	}
}

class Constellation
{
	// TODO: Implement this class
}

class ConstellationPoint
{
	public Point Point { get; }
	public Constellation? Constellation { get; set; }

	public ConstellationPoint(Point point)
	{
		Point = point;
	}
}
