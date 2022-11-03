using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day23.Geometry;

namespace AdventOfCode.Year2018.Day23;

public sealed class Day23Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 23;
	public override string Title => "Experimental Emergency Teleportation";

	private readonly (Point Position, int Radius)[] _nanobotInfo;

	private IEnumerable<Nanobot> GenerateNanobots()
	{
		return _nanobotInfo.Select(parsed => new Nanobot(parsed.Position, parsed.Radius));
	}

	public Day23Solver(Day23SolverOptions options) : base(options)
	{
		try
		{
			_nanobotInfo = InputLines.Select(InputParser.ParseNanobotInfo).ToArray();
		}
		catch (FormatException e)
		{
			throw new InputException("Invalid input.", e);
		}
	}

	public Day23Solver(Action<Day23SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day23Solver() : this(Day23SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		NanobotFormationAnalyzer analyzer = new(GenerateNanobots());
		int result = analyzer.NanobotsInRangeOfStrongestNanobot().Count();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
