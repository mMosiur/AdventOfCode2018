using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day06;

public sealed class Day06SolverOptions : DaySolverOptions
{
	public int MaxTotalDistance { get; set; } = 10_000 - 1;
}
