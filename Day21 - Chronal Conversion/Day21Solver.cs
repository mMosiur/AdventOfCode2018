using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day21;

public class Day21Solver : DaySolver
{
	public Day21Solver(Day21SolverOptions options) : base(options)
	{
		// Initialize Day21 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day21Solver(Action<Day21SolverOptions>? configure = null)
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
