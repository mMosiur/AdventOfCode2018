using AdventOfCode.Year2018.Day25.Geometry;

namespace AdventOfCode.Year2018.Day25;

class ConstellationResolver
{
	private readonly IReadOnlyCollection<Point> _points;

	public ConstellationResolver(IReadOnlyCollection<Point> points)
	{
		_points = points;
	}

	public IReadOnlySet<Constellation> Resolve(int maxDistanceInConstellation)
	{
		ConstellationPoint[] constellationPoints = _points.Select(p => new ConstellationPoint(p)).ToArray();
		for (int i = 0; i < constellationPoints.Length; i++)
		{
			ConstellationPoint point1 = constellationPoints[i];
			for (int j = i + 1; j < constellationPoints.Length; j++)
			{
				ConstellationPoint point2 = constellationPoints[j];
				int distance = ExtendedMath.ManhattanDistance(point1.Point, point2.Point);
				if (distance <= maxDistanceInConstellation)
				{
					if (point1.Constellation == point2.Constellation) continue;
					point1.Constellation.MergeFrom(point2.Constellation);
				}
			}
		}
		return constellationPoints.Select(p => p.Constellation).ToHashSet();
	}
}
