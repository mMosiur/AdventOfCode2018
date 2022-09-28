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
		_veinsOfClay = InputLines.Select(Line.Parse);
	}

	public Day17Solver(Action<Day17SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		Ground ground = new GroundBuilder()
			.SetSpringOfWaterPosition(_springOfWaterPosition)
			.AddVeinsOfClay(_veinsOfClay)
			.Build();
		bool changed = true;
		while(changed)
		{
			changed = ground.NextState();
		}
		int result = ground.Count(gt => gt.IsWater());
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
