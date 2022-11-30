namespace AdventOfCode.Year2018.Day01;

class FrequencyDriftAnalyzer
{
	public int StartingFrequency { get; }

	public FrequencyDriftAnalyzer(int startingFrequency)
	{
		StartingFrequency = startingFrequency;
	}

	public int GetFrequencyAfterChanges(IReadOnlyList<int> frequencyChanges)
	{
		int currentFrequency = StartingFrequency;
		foreach (int change in frequencyChanges)
		{
			currentFrequency += change;
		}
		return currentFrequency;
	}

	public int GetFirstFrequencyReachedTwice(IReadOnlyList<int> frequencyChanges, bool loopFrequencyChanges = false)
	{
		const int MAX_LOOP_COUNT = 1000;
		int currentFrequency = StartingFrequency;
		HashSet<int> frequenciesReached = new() { currentFrequency };
		int maxLoopCount = loopFrequencyChanges ? MAX_LOOP_COUNT : 1;
		for (int loop = 0; loop < maxLoopCount; loop++)
		{
			foreach (int change in frequencyChanges)
			{
				currentFrequency += change;
				bool duplicate = !frequenciesReached.Add(currentFrequency);
				if (duplicate)
				{
					return currentFrequency;
				}
			}
		}
		throw new DaySolverException($"No frequency was reached twice (looped {maxLoopCount} times).");
	}
}
