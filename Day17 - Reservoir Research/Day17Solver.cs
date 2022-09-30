using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day17.Geometry;
using AdventOfCode.Year2018.Day17.Scan;

namespace AdventOfCode.Year2018.Day17;

public class Day17Solver : DaySolver
{
	private readonly IEnumerable<ILine> _veinsOfClay;
	private readonly Point _springOfWaterPosition;

	public Day17Solver(Day17SolverOptions options) : base(options)
	{
		_springOfWaterPosition = options.SpringOfWaterPosition;
		_veinsOfClay = InputLines.Select(StraightLine.Parse);
	}

	public Day17Solver(Action<Day17SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		GroundBuilder groundBuilder = new();
		groundBuilder.AddVeinsOfClay(_veinsOfClay);
		Area areaToConsider = groundBuilder.CurrentArea;
		groundBuilder.AddSpringOfWater(_springOfWaterPosition);
		Ground ground = groundBuilder.Build();

		ground.SimulateFlow();
		int result = ground
			.EnumerateArea(areaToConsider)
			.Count(gt => gt.IsWater());
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
