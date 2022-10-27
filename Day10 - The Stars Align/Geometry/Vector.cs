namespace AdventOfCode.Year2018.Day10.Geometry;

record struct Vector(int X, int Y)
{
	public static Vector operator +(Vector vector1, Vector vector2)
	{
		return new(vector1.X + vector2.X, vector1.Y + vector2.Y);
	}

	public static Point operator +(Point point, Vector vector)
	{
		return new(point.X + vector.X, point.Y + vector.Y);
	}
}
