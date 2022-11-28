using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day06;

public sealed class Day06Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 6;
	public override string Title => "Chronal Coordinates";

	private readonly int _maxTotalDistance;
	private readonly Point[] _points;

	public Day06Solver(Day06SolverOptions options) : base(options)
	{
		_maxTotalDistance = options.MaxTotalDistance;
		_points = InputLines.Select(s => Point.Parse(s)).ToArray();
	}

	public Day06Solver(Action<Day06SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day06Solver() : this(new Day06SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		ClosestPointCoverageAnalyzer analyzer = new(_points);
		Dictionary<Point, int?> areas = analyzer.GetAreasCovered();
		(Point p, int? count) = areas.Where(kvp => kvp.Value is not null)
			.MaxBy(kvp => kvp.Value);
		int result = count!.Value;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		TotalDistanceCoverageAnalyzer analyzer = new(_points);
		int result = analyzer.GetAreaWithTotalDistanceAtLeast(_maxTotalDistance);
		return result.ToString();
	}
}
