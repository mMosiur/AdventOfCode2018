namespace AdventOfCode.Year2018.Day17.Geometry;

public struct Range
{
	public int Start { get; }
	public int End { get; }

	public int Length => End - Start + 1;

	public Range(int start, int end)
	{
		if (end <= start)
		{
			throw new ArgumentOutOfRangeException(nameof(end), "End must be greater than start.");
		}
		Start = start;
		End = end;
	}

	public bool Contains(int value) => value >= Start && value <= End;

	public override string ToString() => $"{Start}..{End}";
}
