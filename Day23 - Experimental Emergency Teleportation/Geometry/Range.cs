using System.Collections;

namespace AdventOfCode.Year2018.Day23.Geometry;

readonly struct Range : IEquatable<Range>, IEnumerable<int>
{
	public int Start { get; }
	public int End { get; }

	public int Length => End - Start + 1;

	public Range(System.Range range)
	{
		if (range.Start.IsFromEnd || range.End.IsFromEnd)
		{
			throw new ArgumentException("Cannot convert System.Range with FromEnd values.", nameof(range));
		}
		Start = range.Start.Value;
		End = range.End.Value;
	}

	public Range(int start, int end)
	{
		if (end < start)
		{
			throw new ArgumentOutOfRangeException(nameof(end), end, $"End must be greater than or equal to start ({start}).");
		}
		Start = start;
		End = end;
	}

	public IEnumerator<int> GetEnumerator() => Enumerable.Range(Start, Length).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public static implicit operator Range(System.Range range) => new(range);

	#region IEquatable<Range>
	public bool Equals(Range other) => Start == other.Start && End == other.End;
	public override bool Equals(object? obj) => obj is Range range && Equals(range);
	public override int GetHashCode() => HashCode.Combine(Start, End);
	public static bool operator ==(Range left, Range right) => left.Equals(right);
	public static bool operator !=(Range left, Range right) => !left.Equals(right);
	#endregion

	public override string ToString() => $"[{Start}, {End}]";
}
