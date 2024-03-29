namespace AdventOfCode.Year2018.Day15.Path;

struct PathSignature : IComparable<PathSignature>, IEquatable<PathSignature>
{
	private readonly Coordinate? _firstStep;

	public Coordinate Start { get; }
	public Coordinate FirstStep => _firstStep ?? throw new InvalidOperationException("Path has no steps.");
	public Coordinate End { get; private set; }
	public int Distance { get; private set; }

	private PathSignature(Coordinate start, Coordinate? firstStep, Coordinate end, int distance)
	{
		Start = start;
		_firstStep = firstStep;
		End = end;
		Distance = distance;
	}

	public PathSignature(Coordinate start) : this(start, null, start, 0) { }

	public PathSignature ExtendedTo(Coordinate coordinate)
	{
		if (!coordinate.IsAdjacent(End))
		{
			throw new ArgumentException($"Cannot extend path to coordinate {coordinate} because it is not adjacent to the current end of the path at {End}.", nameof(coordinate));
		}
		return new PathSignature(Start, _firstStep ?? coordinate, coordinate, Distance + 1);
	}

	public int CompareTo(PathSignature other)
	{
		int distResult = Distance.CompareTo(other.Distance);
		if (distResult != 0) return distResult;
		CoordinateComparer coordComparer = new();
		int endCoordinateResult = coordComparer.Compare(End, other.End);
		if (endCoordinateResult != 0) return endCoordinateResult;
		return (_firstStep, other._firstStep) switch
		{
			(null, null) => 0,
			(null, _) => -1,
			(_, null) => 1,
			_ => coordComparer.Compare(_firstStep.Value, other._firstStep.Value)
		};
	}

	public override string ToString()
	{
		return $"{Start} -[{Distance}]> {End}";
	}

	public bool Equals(PathSignature other)
	{
		return Start == other.Start && End == other.End && Distance == other.Distance;
	}

	public override bool Equals(object? obj)
	{
		return obj is PathSignature path && Equals(path);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Start, _firstStep, End, Distance);
	}

	#region Comparison operators

	public static bool operator <(PathSignature left, PathSignature right) => left.CompareTo(right) < 0;

	public static bool operator <=(PathSignature left, PathSignature right) => left.CompareTo(right) <= 0;

	public static bool operator >(PathSignature left, PathSignature right) => left.CompareTo(right) > 0;

	public static bool operator >=(PathSignature left, PathSignature right) => left.CompareTo(right) >= 0;

	public static bool operator ==(PathSignature left, PathSignature right) => left.Equals(right);

	public static bool operator !=(PathSignature left, PathSignature right) => !(left == right);

	#endregion
}
