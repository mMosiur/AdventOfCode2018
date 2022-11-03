namespace AdventOfCode.Year2018.Day04;

class GuardSleepAnalyzer
{
	private readonly IReadOnlyList<GuardTimeRecord> _guardTimeRecords;

	public GuardSleepAnalyzer(IReadOnlyList<GuardTimeRecord> guardTimeRecords)
	{
		_guardTimeRecords = guardTimeRecords;
	}

	public IEnumerable<int> GuardIds() => _guardTimeRecords.Select(gtr => gtr.GuardId).Distinct();

	public int GetLongestSleepingGuardId() => GetLongestSleepingGuardId(out _);

	public int GetLongestSleepingGuardId(out int minutesSlept)
	{
		Dictionary<int, int> guardSleepTimes = new();
		int id = 0;
		DateTime? fallAsleepTime = null;
		foreach (GuardTimeRecord timeRecord in _guardTimeRecords)
		{
			if (timeRecord.EventType == EventType.BeginsShift)
			{
				if (fallAsleepTime is not null) throw new DaySolverException("A guard started a shift without previous waking up.");
				id = timeRecord.GuardId;
				continue;
			}
			if (timeRecord.GuardId != id) throw new DaySolverException("The action guard ID doesn't match the current guard on shift ID.");
			if (timeRecord.EventType == EventType.FallsAsleep)
			{
				if (fallAsleepTime is not null) throw new DaySolverException("Guard fell asleep without waking up.");
				fallAsleepTime = timeRecord.TimeStamp;
				continue;
			}
			if (timeRecord.EventType == EventType.WakesUp)
			{
				if (fallAsleepTime is null) throw new DaySolverException("Guard woke up without falling asleep.");
				guardSleepTimes.TryGetValue(id, out int sleepTime);
				guardSleepTimes[id] = sleepTime + (int)timeRecord.TimeStamp.Subtract(fallAsleepTime.Value).TotalMinutes;
				fallAsleepTime = null;
				continue;
			}
			throw new DaySolverException("Unknown event type.");
		}
		if (fallAsleepTime is not null)
		{
			throw new DaySolverException("Guard time records are not complete.");
		}
		(int result, minutesSlept) = guardSleepTimes.MaxBy(kvp => kvp.Value);
		return result;
	}

	public IEnumerable<DateTime> EnumerateMinutesAsleep(int guardId)
	{
		TimeSpan oneMinute = TimeSpan.FromMinutes(1);
		DateTime currentTime = default;
		bool isSleeping = false;
		int id = 0;
		foreach (GuardTimeRecord timeRecord in _guardTimeRecords)
		{
			if (timeRecord.EventType is EventType.BeginsShift)
			{
				if (isSleeping) throw new DaySolverException("Guard is sleeping when his shift starts.");
				id = timeRecord.GuardId;
				currentTime = timeRecord.TimeStamp;
				isSleeping = false;
				continue;
			}
			if (timeRecord.GuardId != id) throw new DaySolverException("The action guard ID doesn't match the current guard on shift ID.");
			if (timeRecord.EventType is EventType.FallsAsleep)
			{
				currentTime = timeRecord.TimeStamp;
				isSleeping = true;
				continue;
			}
			if (timeRecord.EventType is EventType.WakesUp)
			{
				if (!isSleeping) throw new DaySolverException("Guard is not sleeping when he wakes up.");
				if (id == guardId)
				{
					while (currentTime < timeRecord.TimeStamp)
					{
						yield return currentTime;
						currentTime += oneMinute;
					}
				}
				currentTime = timeRecord.TimeStamp;
				isSleeping = false;
				continue;
			}
			throw new DaySolverException("Unknown event type.");
		}
		if (isSleeping) throw new DaySolverException("Guard is sleeping when finishing shift enumeration.");
	}
}
