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
		int currentFrequency = StartingFrequency;
		var frequenciesReached = new HashSet<int> { currentFrequency };
		while (loopFrequencyChanges)
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
		throw new ApplicationException("No frequency was reached twice without looping frequency changes.");
	}
}
