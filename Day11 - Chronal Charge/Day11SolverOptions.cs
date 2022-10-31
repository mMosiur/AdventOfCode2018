using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day11;

public sealed class Day11SolverOptions : DaySolverOptions
{
	public static Day11SolverOptions Default => new();
	public int GridSize { get; set; } = 300;
	public int? GridSerialNumber { get; set; } = null;
}
