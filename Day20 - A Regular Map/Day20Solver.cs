using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day20;

public class Day20Solver : DaySolver
{
	public Day20Solver(Day20SolverOptions options) : base(options)
	{
		// Initialize Day20 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day20Solver(Action<Day20SolverOptions>? configure = null)
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
