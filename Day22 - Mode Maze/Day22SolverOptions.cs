using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day22;

public sealed class Day22SolverOptions : DaySolverOptions
{
	public static Day22SolverOptions Default => new();
	public char StartingTool { get; set; } = 'T';
	public char ToolRequiredToFinish { get; set; } = 'T';
	public ushort TimeToSwapTools { get; set; } = 7;
	public ushort TimeToCrossRegion { get; set; } = 1;
}
