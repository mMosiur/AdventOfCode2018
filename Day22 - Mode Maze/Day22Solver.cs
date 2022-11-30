using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day22.Cave;

namespace AdventOfCode.Year2018.Day22;

public sealed class Day22Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 22;
	public override string Title => "Mode Maze";

	private readonly ushort _depth;
	private readonly Coordinate _targetCoordinate;
	private readonly Day22SolverOptions _options;
	private readonly Lazy<CaveSystem> _caveSystem;

	private CaveSystem CaveSystem => _caveSystem.Value;

	public Day22Solver(Day22SolverOptions options) : base(options)
	{
		_options = options;
		try
		{
			(_depth, _targetCoordinate) = InputParser.Parse(Input);
		}
		catch (FormatException e)
		{
			throw new InputException("Input was not in the expected format.", e);
		}
		_caveSystem = new Lazy<CaveSystem>(GenerateCaveSystem);
	}

	public Day22Solver(Action<Day22SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day22Solver() : this(new Day22SolverOptions())
	{
	}

	private CaveSystem GenerateCaveSystem()
	{
		ushort swapToMoveTimeRatio = Convert.ToUInt16(Math.Floor(((double)_options.TimeToSwapTools / _options.TimeToCrossRegion) + 1.0));
		ushort distToClosestBorder = Convert.ToUInt16(Math.Min(_targetCoordinate.X, _targetCoordinate.Y) + 1);
		ushort padding = Convert.ToUInt16(swapToMoveTimeRatio * distToClosestBorder);
		return new CaveSystemBuilder()
			.WithTargetCoordinate(_targetCoordinate)
			.WithDepth(_depth)
			.WithPadding(padding)
			.Build();
	}

	public override string SolvePart1()
	{
		CaveDataCalculator calc = new(_depth, _targetCoordinate);
		int result = calc.CalculateRiskLevel(CaveSystem);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		CaveTravelSimulator simulator = new(CaveSystem, _options.TimeToSwapTools, _options.TimeToCrossRegion);
		uint result = simulator.CalculateShortestTimeToReachTarget(
			ToolHelpers.FromChar(_options.StartingTool),
			ToolHelpers.FromChar(_options.ToolRequiredToFinish)
		);
		return result.ToString();
	}
}
