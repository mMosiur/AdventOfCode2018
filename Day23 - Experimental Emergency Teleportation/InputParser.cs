using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day23;

static class InputParser
{
	private static readonly Regex _nanobotInfoRegex = new(@"\s*pos\s*=\s*<\s*(-?\d+\s*,\s*-?\d+\s*,\s*-?\d+)\s*>,\s*r\s*=\s*(\d+)\s*", RegexOptions.Compiled);

	public static (Point Position, int Radius) ParseNanobotInfo(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		Match match = _nanobotInfoRegex.Match(s);
		if (!match.Success)
		{
			throw new FormatException("Invalid input format.");
		}
		Point position = Point.Parse(match.Groups[1].ValueSpan);
		int radius = int.Parse(match.Groups[2].ValueSpan);
		return (position, radius);
	}
}
