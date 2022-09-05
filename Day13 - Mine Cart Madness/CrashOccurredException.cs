namespace AdventOfCode.Year2018.Day13;

public class CrashOccurredException : Exception
{
	public Coordinate CrashCoordinate { get; }

	public CrashOccurredException(Coordinate crashCoordinate)
	{
		CrashCoordinate = crashCoordinate;
	}

	public CrashOccurredException(Coordinate crashCoordinate, string message) : base(message)
	{
		CrashCoordinate = crashCoordinate;
	}

	public CrashOccurredException(Coordinate crashCoordinate, string message, Exception inner) : base(message, inner)
	{
		CrashCoordinate = crashCoordinate;
	}
}
