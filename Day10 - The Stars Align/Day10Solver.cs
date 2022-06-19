using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day10;

public class Day10Solver : DaySolver
{
	private readonly IEnumerable<SkyPoint> _points;
	private readonly Day10SolverOptions _options;

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		_options = options;
		_points = InputLines.Select(SkyPoint.Parse);
	}

	public Day10Solver(Action<Day10SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		SkySimulator simulator = new(_points.ToList(), _options);
		simulator.Simulate();
		return simulator.SmallestSkyRepresentation;
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
