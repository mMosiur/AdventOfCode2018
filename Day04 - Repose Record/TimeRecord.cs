using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day04;

record struct TimeRecord(DateTime TimeStamp, EventType EventType, int? GuardId)
{

	private static readonly Regex Regex = new(
		pattern: @"^\s*\[\s*(?<timestamp>\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}\s*)\]\s+(?<type>.+)(?<!\s)\s*$",
		options: RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	public static TimeRecord Parse(string s)
	{
		Match match = Regex.Match(s);
		DateTime timestamp = DateTime.ParseExact(match.Groups["timestamp"].ValueSpan, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
		EventType eventType = EventTypeParser.Parse(match.Groups["type"].Value, out int? guardId);
		return new TimeRecord(timestamp, eventType, guardId);
	}
}
