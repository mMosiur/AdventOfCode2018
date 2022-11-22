using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day25.Geometry;

namespace AdventOfCode.Year2018.Day25;

public sealed class Day25Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 25;
	public override string Title => "Four-Dimensional Adventure";

	private readonly Day25SolverOptions _options;
	private readonly IReadOnlyCollection<Point> _points;

	public Day25Solver(Day25SolverOptions options) : base(options)
	{
		_options = options;
		_points = InputLines
			.Select(s => Point.Parse(s))
			.ToArray();
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
		ConstellationResolver resolver = new(_points);
		IReadOnlySet<Constellation> constellations = resolver.Resolve(_options.MaxDistanceInConstellation);
		int result = constellations.Count;
		return $"{result}";
	}

	public override string SolvePart2() => "¡Feliz Navidad!";
}
