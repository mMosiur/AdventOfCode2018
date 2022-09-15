namespace AdventOfCode.Year2018.Day15;

public struct Coordinate : IComparable<Coordinate>, IEquatable<Coordinate>
{
	public int X { get; }
	public int Y { get; }

	public Coordinate(int x, int y)
	{
		X = x;
		Y = y;
	}

	public IEnumerable<Coordinate> AdjacentCoordinates()
	{
		yield return new Coordinate(X - 1, Y);
		yield return new Coordinate(X + 1, Y);
		yield return new Coordinate(X, Y - 1);
		yield return new Coordinate(X, Y + 1);
	}

	public bool IsAdjacent(Coordinate other)
	{
		return ExtendedMath.ManhattanDistance(this, other) == 1;
	}

	public override string ToString() => $"({X}, {Y})";

	#region Comparison methods

	public int CompareTo(Coordinate other)
	{
		int result = X.CompareTo(other.X);
		if (result != 0) return result;
		return Y.CompareTo(other.Y);
	}

	public bool Equals(Coordinate other) => X == other.X && Y == other.Y;

	public override bool Equals(object? obj) => obj is Coordinate other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(X, Y);

	#endregion

	#region Comparison operators

	public static bool operator ==(Coordinate left, Coordinate right) => left.Equals(right);

	public static bool operator !=(Coordinate left, Coordinate right) => !(left == right);

	public static bool operator <(Coordinate left, Coordinate right) => left.CompareTo(right) < 0;

	public static bool operator <=(Coordinate left, Coordinate right) => left.CompareTo(right) <= 0;

	public static bool operator >(Coordinate left, Coordinate right) => left.CompareTo(right) > 0;

	public static bool operator >=(Coordinate left, Coordinate right) => left.CompareTo(right) >= 0;

	#endregion
}
