using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day06;

public sealed class Day06SolverOptions : DaySolverOptions
{
	public static Day06SolverOptions Default => new();
	public int MaxTotalDistance { get; set; } = 10_000 - 1;
}
