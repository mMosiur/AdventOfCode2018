using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day13;

public class Day13Solver : DaySolver
{
	public Day13Solver(Day13SolverOptions options) : base(options)
	{
		string[] lines = InputLines.ToArray();
		TrackSymbol[,] map = TrackMapParser.Parse(lines);
	}

	public Day13Solver(Action<Day13SolverOptions>? configure = null)
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
