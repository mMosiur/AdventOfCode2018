using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day19;

public class Day19SolverOptions : DaySolverOptions
{
	public int NumberOfRegisters { get; set; } = 6;
	public int ResultRegisterNumber { get; set; } = 0;

	public int PartTwoChangedRegisterNumber { get; set; } = 0;
	public uint PartTwoChangedRegisterValue { get; set; } = 1;
}
