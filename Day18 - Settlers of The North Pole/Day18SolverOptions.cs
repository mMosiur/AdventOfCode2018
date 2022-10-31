using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day18;

public sealed class Day18SolverOptions : DaySolverOptions
{
	public static Day18SolverOptions Default => new();
	public int PartOneSimulationMinutesCount { get; set; } = 10;
	public int PartTwoSimulationMinutesCount { get; set; } = 1_000_000_000;
}
