using System.Text.RegularExpressions;
using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

static class InputParser
{
	private static readonly Regex _nanobotInfoRegex = new(@"\s*pos\s*=\s*<\s*(-?\d+)\s*,\s*(-?\d+)\s*,\s*(-?\d+)\s*>,\s*r\s*=\s*(\d+)\s*", RegexOptions.Compiled);

	public static (Point Position, int Radius) ParseNanobotInfo(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		Match match = _nanobotInfoRegex.Match(s);
		if (!match.Success)
		{
			throw new FormatException("Invalid input format.");
		}
		int x = int.Parse(match.Groups[1].ValueSpan);
		int y = int.Parse(match.Groups[2].ValueSpan);
		int z = int.Parse(match.Groups[3].ValueSpan);
		int radius = int.Parse(match.Groups[4].ValueSpan);
		return (new Point(x, y, z), radius);
	}
}
