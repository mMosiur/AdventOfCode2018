using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

class Nanobot
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
		return Geometry.Math.ManhattanDistance(Position, nanobot.Position) <= Radius;
	}
}
