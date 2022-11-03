using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day04;

static class EventTypeParser
{
	private static readonly Regex BeginsShiftRegex = new(
		pattern: @"^\s*Guard\s+#(?<id>\d+)\s+begins\sshift\s*$",
		options: RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	private static readonly Regex FallsAsleepRegex = new(
		pattern: @"^\s*falls\s+asleep\s*$",
		options: RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	private static readonly Regex WakesUpRegex = new(
		pattern: @"^\s*wakes\s+up\s*$",
		options: RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	public static EventType Parse(string s) => Parse(s, out _);

	public static EventType Parse(string s, out int? id)
	{
		Match match = BeginsShiftRegex.Match(s);
		if (match.Success)
		{
			id = int.Parse(match.Groups["id"].ValueSpan);
			return EventType.BeginsShift;
		}
		id = null;
		if (FallsAsleepRegex.IsMatch(s))
		{
			return EventType.FallsAsleep;
		}
		if (WakesUpRegex.IsMatch(s))
		{
			return EventType.WakesUp;
		}
		throw new FormatException($"Unable to parse event type from string '{s}'");
	}
}
