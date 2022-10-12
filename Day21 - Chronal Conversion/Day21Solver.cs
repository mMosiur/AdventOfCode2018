using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day21;

public class Day21Solver : DaySolver
{
	private readonly Device.Program _program;

	public Day21Solver(Day21SolverOptions options) : base(options)
	{
		_program = Device.Program.Parse(Input);
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
