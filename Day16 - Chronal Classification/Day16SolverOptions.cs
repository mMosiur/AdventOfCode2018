using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day16;

public sealed class Day16SolverOptions : DaySolverOptions
{
	public int RegisterSize { get; set; } = 4;
	public int PartOneMinimumBehaviorMatches { get; set; } = 3;
	public int PartTwoResultValueRegister { get; set; } = 0;
}
