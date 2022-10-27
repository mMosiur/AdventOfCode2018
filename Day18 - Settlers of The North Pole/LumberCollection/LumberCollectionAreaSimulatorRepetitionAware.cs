namespace AdventOfCode.Year2018.Day18.LumberCollection;

class LumberCollectionAreaSimulatorRepetitionAware : LumberCollectionAreaSimulator
{
	public LumberCollectionAreaSimulatorRepetitionAware(LumberCollectionArea area) : base(area)
	{
	}

	public override void Simulate(int minutes)
	{
		if (minutes < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(minutes), "Number of minutes to simulate must be non-negative.");
		}
		Dictionary<string, int> previousStates = new();
		for (int minute = 0; minute < minutes; minute++)
		{
			string compressed = Area.Compress();
			if (previousStates.TryGetValue(compressed, out int repetitionMinute))
			{
				// Repetition detected
				int repetitionDistance = minute - repetitionMinute;
				int finalStateMinute = repetitionMinute + (minutes - repetitionMinute) % repetitionDistance;
				string finalState = previousStates.First(kvp => kvp.Value == finalStateMinute).Key;
				Area.LoadCompressed(finalState);
				return;
			}
			previousStates[compressed] = minute;
			SimulateMinute();
		}
	}
}
