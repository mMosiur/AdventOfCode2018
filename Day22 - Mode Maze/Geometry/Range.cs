namespace AdventOfCode.Year2018.Day22.Geometry;

readonly struct Range : IEquatable<Range>
{

	public int Start { get; }
	public int End { get; }

	public int Length => End - Start + 1;

	public Range(int start, int end)
	{
		if (end < start)
		{
			throw new ArgumentException("Range end cannot be lower than it's start.", nameof(end));
		}
		Start = start;
		End = end;
	}

	public bool Contains(int value) => value >= Start && value <= End;
	public bool Contains(Range range) => range.Start >= Start && range.End <= End;

	public bool Equals(Range other) => Start == other.Start && End == other.End;
	public override bool Equals(object? obj) => obj is Range range && Equals(range);
	public override int GetHashCode() => HashCode.Combine(Start, End);
	public static bool operator ==(Range left, Range right) => left.Equals(right);
	public static bool operator !=(Range left, Range right) => !(left == right);
}
