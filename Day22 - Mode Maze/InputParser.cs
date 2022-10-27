using System.Text.RegularExpressions;
using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22;

class InputParser
{
	private static readonly Regex _depthRegex = new(@"[ \t]*depth:[ \t]*(\d+)[ \t]*$");
	private static readonly Regex _targetRegex = new(@"^[ \t]*target:[ \t]*(\d+),[ \t]*(\d+)[ \t]*$");

	public static (ushort depth, Coordinate target) Parse(string input)
	{
		StringReader reader = new(input);
		ushort? depth = null;
		Coordinate? target = null;
		string? line;
		while ((line = reader.ReadLine()) is not null)
		{
			Match depthMatch = _depthRegex.Match(line);
			if (depthMatch.Success)
			{
				if (depth is not null)
				{
					throw new ApplicationException("Input contained multiple depth lines.");
				}
				depth = ushort.Parse(depthMatch.Groups[1].ValueSpan);
				continue;
			}
			Match targetMatch = _targetRegex.Match(line);
			if (targetMatch.Success)
			{
				if (target is not null)
				{
					throw new ApplicationException("Input contained multiple target lines.");
				}
				int x = int.Parse(targetMatch.Groups[1].ValueSpan);
				int y = int.Parse(targetMatch.Groups[2].ValueSpan);
				target = new Coordinate(x, y);
				continue;
			}
			throw new FormatException($"Unrecognized input line: \"{line}\".");
		}
		if (depth is null)
		{
			throw new ApplicationException("Input did not contain a depth.");
		}
		if (target is null)
		{
			throw new ApplicationException("Input did not contain a target.");
		}
		return new(depth.Value, target.Value);
	}
}
