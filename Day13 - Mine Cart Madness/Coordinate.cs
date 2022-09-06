namespace AdventOfCode.Year2018.Day13;

public record struct Coordinate(int X, int Y)
{
	public override string ToString()
	{
		return $"{X},{Y}";
	}
}
