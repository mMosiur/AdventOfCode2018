using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day24;

public sealed class Day24SolverOptions : DaySolverOptions
{
	public static Day24SolverOptions Default => new();

	public string PartTwoBoostingArmyName { get; set; } = "Immune System";
	public int LowerBoostBound { get; set; } = 0;
	public int UpperBoostBound { get; set; } = 2000;
}
