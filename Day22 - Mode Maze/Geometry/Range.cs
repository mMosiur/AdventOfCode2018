using System.Collections;

namespace AdventOfCode.Year2018.Day22.Geometry;

public readonly struct Range : IEnumerable<int>, IEquatable<Range>
{
	public int Start { get; }
	public int End { get; }

	public int Length => End - Start + 1;

	public Range(int start, int end)
	{
		Start = start;
		End = end;
	}

	public bool Equals(Range other) => Start == other.Start && End == other.End;

	public override bool Equals(object? obj) => obj is Range range && Equals(range);

	public override int GetHashCode() => HashCode.Combine(Start, End);
	public static bool operator ==(Range left, Range right) => left.Equals(right);
	public static bool operator !=(Range left, Range right) => !(left == right);


	public IEnumerator<int> GetEnumerator() => Enumerable.Range(Start, End - Start + 1).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
