using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day12;

public sealed class Day12SolverOptions : DaySolverOptions
{
	public static Day12SolverOptions Default => new();
	public bool AssumeMissingNotesProduceEmpty { get; set; } = false;
}
