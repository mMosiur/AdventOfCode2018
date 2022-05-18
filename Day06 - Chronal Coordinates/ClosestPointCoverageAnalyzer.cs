namespace AdventOfCode.Year2018.Day06;

public class ClosestPointCoverageAnalyzer
{
	private readonly IEnumerable<Point> _points;

	public ClosestPointCoverageAnalyzer(IEnumerable<Point> points)
	{
		_points = points;
	}

	public Dictionary<Point, int?> GetAreasCovered()
	{
		int minX = _points.Min(p => p.X);
		int maxX = _points.Max(p => p.X);
		int minY = _points.Min(p => p.Y);
		int maxY = _points.Max(p => p.Y);
		Dictionary<Point, int?> areas = _points.ToDictionary(
			keySelector: p => p,
			elementSelector: p => (int?)0
		);
		CalculateBoundedAreas(areas, minX, maxX, minY, maxY);
		RemoveInfiniteAreas(areas, minX, maxX, minY, maxY);
		return areas;
	}

	private void CalculateBoundedAreas(Dictionary<Point, int?> areas, int minX, int maxX, int minY, int maxY)
	{
		for (int x = minX; x <= maxX; x++)
		{
			for (int y = minY; y <= maxY; y++)
			{
				Point point = new(x, y);
				Point? closestPoint = point.GetClosestPointFrom(_points);
				if (closestPoint is null)
				{
					continue;
				}
				areas[closestPoint.Value]++;
			}
		}
	}

	private void RemoveInfiniteAreas(Dictionary<Point, int?> areas, int minX, int maxX, int minY, int maxY)
	{
		for (int x = minX - 1; x <= maxX + 1; x++)
		{
			Point point = new(x, minY - 1);
			Point? closestPoint = point.GetClosestPointFrom(_points);
			if (closestPoint is not null)
			{
				areas[closestPoint.Value] = null;
			}
			point = new(x, maxY + 1);
			closestPoint = point.GetClosestPointFrom(_points);
			if (closestPoint is not null)
			{
				areas[closestPoint.Value] = null;
			}
		}
		for (int y = minY - 1; y <= maxY + 1; y++)
		{
			Point point = new(minX - 1, y);
			Point? closestPoint = point.GetClosestPointFrom(_points);
			if (closestPoint is not null)
			{
				areas[closestPoint.Value] = null;
			}
			point = new(maxX + 1, y);
			closestPoint = point.GetClosestPointFrom(_points);
			if (closestPoint is not null)
			{
				areas[closestPoint.Value] = null;
			}
		}
	}
}
