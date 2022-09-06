using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day13;

public class Day13Solver : DaySolver
{
	private readonly TrackSymbol[,] _map;

	public Day13Solver(Day13SolverOptions options) : base(options)
	{
		_map = TrackMapParser.Parse(InputLines.ToArray());
	}

	public Day13Solver(Action<Day13SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		CartCrashAnalyzer analyzer = new(_map);
		Coordinate firstCrashPosition = analyzer.FindFirstCrashPosition();
		return firstCrashPosition.ToString();
	}

	public override string SolvePart2()
	{
		CartCrashAnalyzer analyzer = new(_map);
		Coordinate lastStandingCartPosition = analyzer.FindLastStandingCartPosition();
		return lastStandingCartPosition.ToString();
	}
}
