using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day25;

public sealed class Day25SolverOptions : DaySolverOptions
{
	public static Day25SolverOptions Default => new();

	public int MaxDistanceInConstellation { get; set; } = 3;
}
