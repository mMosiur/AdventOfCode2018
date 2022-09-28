using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day17.Geometry;

public static class Line
{
	private static readonly Regex _regex = new(@"^(?<vertical>x ?= ?(?<x>\d+),[ \t]*y ?= ?(?<y1>\d+)\.\.(?<y2>\d+))|(?<horizontal>y ?= ?(?<y>\d+),[ \t]*x ?= ?(?<x1>\d+)\.\.(?<x2>\d+))$", RegexOptions.Compiled);

	public static ILine Parse(string s)
	{
		ArgumentNullException.ThrowIfNull(s);
		Match match = _regex.Match(s);
		try
		{
			if (!match.Success)
			{
				throw new FormatException("Invalid line format.");
			}
			if (match.Groups["vertical"].Success)
			{
				return new VerticalLine(
					int.Parse(match.Groups["x"].Value),
					int.Parse(match.Groups["y1"].Value),
					int.Parse(match.Groups["y2"].Value)
				);
			}
			else if (match.Groups["horizontal"].Success)
			{
				return new HorizontalLine(
					int.Parse(match.Groups["x1"].Value),
					int.Parse(match.Groups["x2"].Value),
					int.Parse(match.Groups["y"].Value)
				);
			}
			else
			{
				throw new FormatException("Invalid line format.");
			}
		}
		catch (FormatException e)
		{
			throw new FormatException($"Invalid line: '{s}'.", e);
		}
		catch (ArgumentException e)
		{
			throw new FormatException($"Invalid values in line: '{s}'.", e);
		}
	}
}
