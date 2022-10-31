using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day16;

public sealed class Day16SolverOptions : DaySolverOptions
{
	public static Day16SolverOptions Default => new();
	public int RegisterSize { get; init; } = 4;
	public int PartOneMinimumBehaviorMatches { get; init; } = 3;
	public int PartTwoResultValueRegister { get; init; } = 0;
}
