using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day07;

public sealed class Day07SolverOptions : DaySolverOptions
{
	public static Day07SolverOptions Default => new();
	public int WorkersCount { get; set; } = 5;
	public int StepOverheadDuration { get; set; } = 60;
}
