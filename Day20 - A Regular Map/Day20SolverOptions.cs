using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day20;

public sealed class Day20SolverOptions : DaySolverOptions
{
	public static Day20SolverOptions Default => new();
	public int PartTwoMinDistance { get; } = 1000;
}
