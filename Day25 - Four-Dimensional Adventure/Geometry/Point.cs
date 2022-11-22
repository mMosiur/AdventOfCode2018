using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2018.Day25.Geometry;

readonly partial struct Point
{
	public required int X { get; init; }
	public required int Y { get; init; }
	public required int Z { get; init; }
	public required int W { get; init; }

	[SetsRequiredMembers]
	public Point(int x, int y, int z, int w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	public override string ToString()
	{
		return $"Point({X}, {Y}, {Z}, {W})";
	}
}
