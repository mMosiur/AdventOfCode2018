namespace AdventOfCode.Year2018.Day06;

class TotalDistanceCoverageAnalyzer
{
	private readonly IEnumerable<Point> _points;

	public TotalDistanceCoverageAnalyzer(IEnumerable<Point> points)
	{
		_points = points;
	}

	public int GetAreaWithTotalDistanceAtLeast(int maxTotalDistance)
	{
		int minX = _points.Min(p => p.X);
		int maxX = _points.Max(p => p.X);
		int minY = _points.Min(p => p.Y);
		int maxY = _points.Max(p => p.Y);
		int addedArea = 0;
		foreach (Point point in Helpers.GetPointsInArea(minX, maxX, minY, maxY))
		{
			int totalDistance = GetTotalDistanceFrom(point, _points);
			if (totalDistance <= maxTotalDistance)
			{
				addedArea++;
			}
		}
		int totalArea = 0;
		while (addedArea > 0)
		{
			totalArea += addedArea;
			minX--;
			maxY++;
			minY--;
			maxY++;
			addedArea = GetAreaOfRing(minX, maxX, minY, maxY, maxTotalDistance);
		}
		return totalArea;
	}

	private int GetAreaOfRing(int minX, int maxX, int minY, int maxY, int maxDistance)
	{
		return Helpers.GetPointsInRectangleBorder(minX, maxX, minY, maxY)
			.Count(p => GetTotalDistanceFrom(p, _points) <= maxDistance);
	}

	private static int GetTotalDistanceFrom(Point origin, IEnumerable<Point> targets)
	{
		int totalDistance = 0;
		foreach (Point target in targets)
		{
			totalDistance += MathG.ManhattanDistance(origin, target);
		}
		return totalDistance;
	}
}
