namespace AdventOfCode.Year2018.Day06;

public static class Math
{
	public static int ManhattanDistance(Point p1, Point p2)
		=> System.Math.Abs(p1.X - p2.X) + System.Math.Abs(p1.Y - p2.Y);
}
