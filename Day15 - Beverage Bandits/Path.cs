namespace AdventOfCode.Year2018.Day15;

public class Path : IComparable<Path>
{
	private readonly List<Coordinate> _path;

	public Coordinate Start => _path[0];
	public IReadOnlyList<Coordinate> Trail => _path;
	public Coordinate End => _path[^1];
	public int Distance => _path.Count - 1;

	private Path(List<Coordinate> path)
	{
		_path = path;
	}

	public Path(Coordinate start)
	{
		_path = new List<Coordinate> { start };
	}

	public void ExtendTo(Coordinate coordinate)
	{
		if (!coordinate.IsAdjacent(End))
		{
			throw new ArgumentException($"Cannot extend path to coordinate {coordinate} because it is not adjacent to the current end of the path at {End}.", nameof(coordinate));
		}
		_path.Add(coordinate);
	}

	public Path NewExtendedTo(Coordinate coordinate)
	{
		Path newPath = Clone();
		newPath.ExtendTo(coordinate);
		return newPath;
	}

	public Path Reverted() => new(_path.AsEnumerable().Reverse().ToList());

	public Path Clone() => new(_path.ToList());

	public int CompareTo(Path? other)
	{
		ArgumentNullException.ThrowIfNull(other);
		int distResult = Distance.CompareTo(other.Distance);
		if (distResult != 0) return distResult;
		int endResult = End.CompareTo(other.End);
		if (endResult != 0) return endResult;
		return Enumerable.Zip(Trail, other.Trail, (a, b) => a.CompareTo(b)).FirstOrDefault(c => c != 0);
	}

	public override string ToString()
	{
		return $"{Start} -[{Distance}]> {End}";
	}

	#region Comparison operators

	public static bool operator <(Path left, Path right) => left.CompareTo(right) < 0;

	public static bool operator <=(Path left, Path right) => left.CompareTo(right) <= 0;

	public static bool operator >(Path left, Path right) => left.CompareTo(right) > 0;

	public static bool operator >=(Path left, Path right) => left.CompareTo(right) >= 0;

	#endregion
}
