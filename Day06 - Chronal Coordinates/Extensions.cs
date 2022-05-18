using System.Net.NetworkInformation;

namespace AdventOfCode.Year2018.Day06;

public static class Extensions
{
	/// <summary>
	/// Returns the closes point from the given point set using Manhattan Distance metric.
	/// If multiple points are equally close, null is returned;
	/// </summary>
	public static Point? GetClosestPointFrom(this Point point, IEnumerable<Point> points)
	{
		Point? closestPoint = null;
		int? closestDistance = null;
		bool multipleClosest = false;
		foreach (Point p in points)
		{
			int distance = Math.ManhattanDistance(point, p);
			if (closestDistance is null || distance < closestDistance)
			{
				closestPoint = p;
				closestDistance = distance;
				multipleClosest = false;
			}
			else if (distance == closestDistance)
			{
				multipleClosest = true;
			}
		}
		return multipleClosest ? null : closestPoint;
	}
}
