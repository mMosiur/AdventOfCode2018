using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day24;

public sealed class Day24Solver : DaySolver
{
	public override int Year => 2018;

	public override int Day => 24;

	public override string Title => "Immune System Simulator 20XX";

	public Day24Solver(Day24SolverOptions options) : base(options)
	{
		// Initialize Day24 solver here.
		// Property `Input` contains the raw input text.
		// Property `InputLines` enumerates lines in the input text.
	}

	public Day24Solver(Action<Day24SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day24Solver() : this(Day24SolverOptions.Default)
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
