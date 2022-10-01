namespace AdventOfCode.Year2018.Day18.LumberCollection;

public class LumberCollectionAreaSimulatorBruteForce : LumberCollectionAreaSimulator
{
	public LumberCollectionAreaSimulatorBruteForce(LumberCollectionArea area) : base(area)
	{
	}

	public override void Simulate(int minutes)
	{
		if (minutes < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(minutes), "Number of minutes to simulate must be non-negative.");
		}
		for (int minute = 0; minute < minutes; minute++)
		{
			SimulateMinute();
		}
	}
}
