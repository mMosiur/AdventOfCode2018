namespace AdventOfCode.Year2018.Day09;

class Marble
{
	public int Number { get; }

	public Marble(int number)
	{
		Number = number;
	}

	public override string ToString()
	{
		return $"Marble(${Number})";
	}
}
