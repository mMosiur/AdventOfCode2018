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
		foreach (Point point in Helpers.GetPointsInArea(minX, maxX, minY, maxY))
		{
			Point? closestPoint = GetClosestPointFrom(point, _points);
			if (closestPoint is null)
			{
				continue;
			}
			areas[closestPoint.Value]++;
		}
	}

	private void RemoveInfiniteAreas(Dictionary<Point, int?> areas, int minX, int maxX, int minY, int maxY)
	{
		foreach (Point point in Helpers.GetPointsInRectangleBorder(minX, maxX, minY, maxY))
		{
			Point? closestPoint = GetClosestPointFrom(point, _points);
			if (closestPoint is not null)
			{
				areas[closestPoint.Value] = null;
			}
		}
	}

	private static Point? GetClosestPointFrom(Point origin, IEnumerable<Point> targets)
	{
		Point? closestTarget = null;
		int? closestDistance = null;
		bool multipleClosest = false;
		foreach (Point target in targets)
		{
			int distance = Math.ManhattanDistance(origin, target);
			if (closestDistance is null || distance < closestDistance)
			{
				closestTarget = target;
				closestDistance = distance;
				multipleClosest = false;
			}
			else if (distance == closestDistance)
			{
				multipleClosest = true;
			}
		}
		return multipleClosest ? null : closestTarget;
	}
}
