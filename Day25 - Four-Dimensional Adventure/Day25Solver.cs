using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day25.Geometry;

namespace AdventOfCode.Year2018.Day25;

public sealed class Day25Solver : DaySolver
{
	private readonly IReadOnlyCollection<Point> _points;

	public override int Year => 2018;
	public override int Day => 25;
	public override string Title => "Four-Dimensional Adventure";

	public Day25Solver(Day25SolverOptions options) : base(options)
	{
		_points = InputLines.Select(s => Point.Parse(s)).ToArray();
	}

	public Day25Solver(Action<Day25SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day25Solver() : this(Day25SolverOptions.Default)
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
