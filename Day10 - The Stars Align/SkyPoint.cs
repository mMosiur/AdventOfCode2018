using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Day10;

class SkyPoint
{
	private static readonly Regex _regex = new(
		@"^\s*position\s*=\s*<(?<position>\s*\-?\d+\s*,\s*\-?\d+\s*)>\s*velocity\s*=\s*<(?<velocity>\s*\-?\d+\s*,\s*\-?\d+\s*)>\s*$",
		RegexOptions.Compiled | RegexOptions.IgnoreCase
	);

	public Point Position { get; private set; }
	public Vector Velocity { get; private set; }

	public SkyPoint(Point position, Vector velocity)
	{
		Position = position;
		Velocity = velocity;
	}

	public static SkyPoint Parse(string s)
	{
		Match match = _regex.Match(s);
		if (!match.Success)
		{
			throw new FormatException($"Invalid format: {s}");
		}
		Point position = Point.Parse(match.Groups["position"].ValueSpan);
		Vector velocity = Vector.Parse(match.Groups["velocity"].ValueSpan);
		return new(position, velocity);
	}

	public void Move()
	{
		Position += Velocity;
	}
}
