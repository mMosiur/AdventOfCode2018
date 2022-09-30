using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day17.Geometry;
using AdventOfCode.Year2018.Day17.Scan;

namespace AdventOfCode.Year2018.Day17;

public class Day17Solver : DaySolver
{
	private readonly IEnumerable<ILine> _veinsOfClay;
	private readonly Point _springOfWaterPosition;

	private Area? _areaToConsider = null;
	private Ground? _ground = null;

	public Day17Solver(Day17SolverOptions options) : base(options)
	{
		_springOfWaterPosition = options.SpringOfWaterPosition;
		_veinsOfClay = InputLines.Select(StraightLine.Parse);
	}

	public Day17Solver(Action<Day17SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	[MemberNotNull(nameof(_ground))]
	[MemberNotNull(nameof(_areaToConsider))]
	private void BuildGround()
	{
		if (_ground is not null && _areaToConsider is not null)
		{
			return;
		}
		GroundBuilder groundBuilder = new();
		groundBuilder.AddVeinsOfClay(_veinsOfClay);
		_areaToConsider = groundBuilder.CurrentArea;
		groundBuilder.AddSpringOfWater(_springOfWaterPosition);
		_ground = groundBuilder.Build();
	}

	public override string SolvePart1()
	{
		BuildGround();
		_ground.SimulateFlow();
		int result = _ground
			.EnumerateArea(_areaToConsider.Value)
			.Count(gt => gt.IsWater());
		return result.ToString();
	}

	public override string SolvePart2()
	{
		BuildGround();
		_ground.SimulateFlow();
		int result = _ground
			.EnumerateArea(_areaToConsider.Value)
			.Count(gt => gt is GroundType.WaterResting);
		return result.ToString();
	}
}
