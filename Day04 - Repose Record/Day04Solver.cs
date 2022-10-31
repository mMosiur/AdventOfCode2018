using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day04;

public sealed class Day04Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 4;
	public override string Title => "Repose Record";

	private readonly List<GuardTimeRecord> _timeRecords;

	public Day04Solver(Day04SolverOptions options) : base(options)
	{
		List<TimeRecord> timeRecords = InputLines.Select(TimeRecord.Parse).ToList();
		timeRecords.Sort((tr1, tr2) => tr1.TimeStamp.CompareTo(tr2.TimeStamp));
		if (timeRecords.First().GuardId is null)
		{
			throw new ApplicationException("First time records guard ID couldn't be inferred.");
		}
		int id = 0;
		_timeRecords = timeRecords.Select(timeRecord =>
		{
			id = timeRecord.GuardId ?? id;
			return GuardTimeRecord.FromTimeRecord(timeRecord, id);
		}).ToList();
	}

	public Day04Solver(Action<Day04SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day04Solver() : this(Day04SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		var analyzer = new GuardSleepAnalyzer(_timeRecords);
		int mostSleepyGuardId = analyzer.GetLongestSleepingGuardId();
		int mostOftenSleptMinute = analyzer
			.EnumerateMinutesAsleep(mostSleepyGuardId)
			.GroupBy(dt => TimeOnly.FromDateTime(dt))
			.MaxBy(g => g.Count())?.Key.Minute
			?? throw new ApplicationException("No minutes were slept.");
		int result = mostSleepyGuardId * mostOftenSleptMinute;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		var analyzer = new GuardSleepAnalyzer(_timeRecords);
		HashSet<(int GuardId, int Minute, int Count)> sleepStatsByMinute = new();
		foreach (int guardId in analyzer.GuardIds())
		{
			IGrouping<TimeOnly, DateTime>? grouping = analyzer
				.EnumerateMinutesAsleep(guardId)
				.GroupBy(dt => TimeOnly.FromDateTime(dt))
				.MaxBy(g => g.Count());
			if (grouping is null)
			{
				// Guard never slept.
				continue;
			}
			sleepStatsByMinute.Add((guardId, grouping.Key.Minute, grouping.Count()));
		}
		var longestSleepStatsByMinute = sleepStatsByMinute.MaxBy(value => value.Count);
		int result = longestSleepStatsByMinute.GuardId * longestSleepStatsByMinute.Minute;
		return result.ToString();
	}
}
