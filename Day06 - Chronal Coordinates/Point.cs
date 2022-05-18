using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day06;

public record struct Point(int X, int Y)
{
	private static readonly Regex PointRegex = new(@"^[^\S\r\n]*(?<x>\d+)[^\S\r\n]*,[^\S\r\n]*(?<y>\d+)[^\S\r\n]*$", RegexOptions.Compiled);
	public static Point Parse(string s)
	{
		Match match = PointRegex.Match(s);
		if (!match.Success) throw new FormatException($"Invalid point format: \"{s}\"");
		int x = int.Parse(match.Groups["x"].ValueSpan);
		int y = int.Parse(match.Groups["y"].ValueSpan);
		return new(x, y);
	}

	public override string ToString() => $"({X},{Y})";
}
