using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day21;

public sealed class Day21SolverOptions : DaySolverOptions
{
	public int NumberOfRegisters { get; set; } = 6;
	public int ControlRegisterNumber { get; set; } = 0;
}
