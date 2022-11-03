using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day19;

public sealed class Day19SolverOptions : DaySolverOptions
{
	public static Day19SolverOptions Default => new();
	public int NumberOfRegisters { get; set; } = 6;
	public int ResultRegisterNumber { get; set; } = 0;

	public int PartTwoChangedRegisterNumber { get; set; } = 0;
	public uint PartTwoChangedRegisterValue { get; set; } = 1;
}
