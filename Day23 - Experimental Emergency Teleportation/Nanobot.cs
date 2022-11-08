using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

sealed class Nanobot
{
	public Point Position { get; }
	public int Radius { get; }

	public Nanobot(Point position, int radius)
	{
		Position = position;
		Radius = radius;
	}

	public bool IsInRange(Nanobot nanobot)
	{
		return ExtendedMath.ManhattanDistance(Position, nanobot.Position) <= Radius;
	}
}
