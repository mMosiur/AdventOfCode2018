using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day10;

public sealed class Day10Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 10;
	public override string Title => "The Stars Align";

	private readonly IEnumerable<SkyPoint> _points;
	private readonly SkySimulator _simulator;

	public Day10Solver(Day10SolverOptions options) : base(options)
	{
		_points = InputLines.Select(SkyPoint.Parse);
		_simulator = new SkySimulator(_points.ToList(), options);
	}

	public Day10Solver(Action<Day10SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day10Solver() : this(Day10SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		_simulator.SimulateTillSmallestSkyFound();
		return _simulator.SmallestSkyRepresentation;
	}

	public override string SolvePart2()
	{
		_simulator.SimulateTillSmallestSkyFound();
		return _simulator.SecondsToSmallestSky.ToString();
	}
}
