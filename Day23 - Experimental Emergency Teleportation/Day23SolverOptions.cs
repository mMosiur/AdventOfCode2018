using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day23;

public sealed class Day23SolverOptions : DaySolverOptions
{
	public static Day23SolverOptions Default => new();

	public int MyPositionX { get; set; } = 0;
	public int MyPositionY { get; set; } = 0;
	public int MyPositionZ { get; set; } = 0;
}
