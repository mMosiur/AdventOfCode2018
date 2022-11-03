namespace AdventOfCode.Year2018.Day04;

record struct GuardTimeRecord(DateTime TimeStamp, EventType EventType, int GuardId)
{
	public static GuardTimeRecord FromTimeRecord(TimeRecord timeRecord, int guardId)
	{
		if (timeRecord.GuardId.HasValue && timeRecord.GuardId.Value != guardId)
		{
			throw new DaySolverException("Guard id mismatch");
		}
		return new GuardTimeRecord(timeRecord.TimeStamp, timeRecord.EventType, guardId);
	}
}
