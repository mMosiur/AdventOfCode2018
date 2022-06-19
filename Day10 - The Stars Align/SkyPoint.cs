using System.Text.RegularExpressions;
using AdventOfCode.Year2018.Day10.Geometry;

namespace AdventOfCode.Year2018.Day10;

public class SkyPoint
{
	private static readonly Regex _regex = new(
		@"^(?: |\t)*position(?: |\t)*=(?: |\t)*<(?: |\t)*(?<positionX>\-?\d+)(?: |\t)*,(?: |\t)*(?<positionY>\-?\d+)(?: |\t)*>(?: |\t)*velocity(?: |\t)*=(?: |\t)*<(?: |\t)*(?<velocityX>\-?\d+)(?: |\t)*,(?: |\t)*(?<velocityY>\-?\d+)(?: |\t)*>(?: |\t)*$",
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
		int positionX = int.Parse(match.Groups["positionX"].ValueSpan);
		int positionY = int.Parse(match.Groups["positionY"].ValueSpan);
		int velocityX = int.Parse(match.Groups["velocityX"].ValueSpan);
		int velocityY = int.Parse(match.Groups["velocityY"].ValueSpan);
		return new(new(positionX, positionY), new(velocityX, velocityY));
	}

	public void Move()
	{
		Position += Velocity;
	}
}
