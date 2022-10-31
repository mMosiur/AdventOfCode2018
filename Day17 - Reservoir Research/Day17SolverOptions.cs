using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day17;

public sealed class Day17SolverOptions : DaySolverOptions
{
	public static Day17SolverOptions Default => new();
	public int SpringOfWaterPositionX { get; set; } = 500;
	public int SpringOfWaterPositionY { get; set; } = 0;
}
