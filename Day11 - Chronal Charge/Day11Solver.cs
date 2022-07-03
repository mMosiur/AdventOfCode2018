using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day11;

public class Day11Solver : DaySolver
{
	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		// Initialize Day11 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day11Solver(Action<Day11SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		return "UNSOLVED";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
