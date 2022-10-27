namespace AdventOfCode.Year2018.Day13;

record struct Coordinate(int X, int Y)
{
	public override string ToString()
	{
		return $"{X},{Y}";
	}
}
