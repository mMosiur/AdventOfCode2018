using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day16;

public class Day16SolverOptions : DaySolverOptions
{
	public int RegisterSize { get; init; } = 4;
	public int PartOneMinimumBehaviorMatches { get; init; } = 3;
	public int PartTwoResultValueRegister { get; init; } = 0;
}
