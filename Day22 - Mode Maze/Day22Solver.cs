using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22;

public class Day22Solver : DaySolver
{
	private readonly ushort _depth;
	private readonly Coordinate _targetCoordinate;

	public Day22Solver(Day22SolverOptions options) : base(options)
	{
		try
		{
			(_depth, _targetCoordinate) = InputParser.Parse(Input);
		}
		catch (FormatException e)
		{
			throw new ApplicationException("Input was not in the expected format.", e);
		}
	}

	public Day22Solver(Action<Day22SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		CaveSystem caveSystem = new CaveSystemBuilder()
			.WithTargetCoordinate(_targetCoordinate)
			.WithDepth(_depth)
			.Build();
		CaveDataCalculator calc = new(_depth, _targetCoordinate);
		int result = calc.CalculateRiskLevel(caveSystem);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
