namespace AdventOfCode.Year2018.Day22.Geometry;

public readonly struct Coordinate : IEquatable<Coordinate>
{
	public int X { get; }
	public int Y { get; }

	public Coordinate(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static readonly Coordinate Origin = new(0, 0);

	public static IEnumerable<Coordinate> EnumerateArea(int startX, int startY, int endX, int endY)
	{
		for (var y = startY; y <= endY; y++)
		{
			for (var x = startX; x <= endX; x++)
			{
				yield return new Coordinate(x, y);
			}
		}
	}

	public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

	public override string ToString()
	{
		return $"({X},{Y})";
	}

	public bool Equals(Coordinate other) => X == other.X && Y == other.Y;

	public override bool Equals(object? obj) => obj is Coordinate coordinate && Equals(coordinate);

	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Coordinate left, Coordinate right) => left.Equals(right);
	public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);
}
