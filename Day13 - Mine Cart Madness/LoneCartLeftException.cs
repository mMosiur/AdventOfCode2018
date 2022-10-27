namespace AdventOfCode.Year2018.Day13;

[Serializable]
class LoneCartLeftException : Exception
{
	public Coordinate LoneCartPosition { get; }

	public LoneCartLeftException(Coordinate position)
	{
		LoneCartPosition = position;
	}

	public LoneCartLeftException(Coordinate position, string message) : base(message)
	{
		LoneCartPosition = position;
	}

	public LoneCartLeftException(Coordinate position, string message, Exception inner) : base(message, inner)
	{
		LoneCartPosition = position;
	}
}
