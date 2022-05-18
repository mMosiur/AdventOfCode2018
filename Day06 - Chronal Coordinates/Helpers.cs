namespace AdventOfCode.Year2018.Day06;

public static class Helpers
{
	public static IEnumerable<Point> GetPointsInArea(int minX, int maxX, int minY, int maxY)
	{
		for (int x = minX; x <= maxX; x++)
		{
			for (int y = minY; y <= maxY; y++)
			{
				yield return new Point(x, y);
			}
		}
	}
	public static IEnumerable<Point> GetPointsInRectangleBorder(int minX, int maxX, int minY, int maxY)
	{
		int x = minX;
		int y = minY;
		while (x < maxX)
		{
			yield return new(x, y);
			x++;
		}
		while (y < maxY)
		{
			yield return new(maxX, y);
			y++;
		}
		while (x > minX)
		{
			yield return new(x, maxY);
			x--;
		}
		while (y > minY)
		{
			yield return new(minX, y);
			y--;
		}
	}
}
