using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day23;

public sealed class Day23Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 23;
	public override string Title => "Experimental Emergency Teleportation";

	private readonly (Point Position, int Radius)[] _nanobotInfo;
	private readonly Point _origin;

	private IEnumerable<Nanobot> GenerateNanobots()
	{
		return _nanobotInfo.Select(parsed => new Nanobot(parsed.Position, parsed.Radius));
	}

	public Day23Solver(Day23SolverOptions options) : base(options)
	{
		_origin = new Point(options.MyPositionX, options.MyPositionY, options.MyPositionZ);
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

	public Day23Solver() : this(new Day23SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		NanobotFormationAnalyzer analyzer = new(GenerateNanobots(), _origin);
		Nanobot strongestNanobot = analyzer.FindStrongestNanobot();
		int result = analyzer
			.NanobotsInRangeOf(strongestNanobot)
			.Count();
		return $"{result}";
	}

	public override string SolvePart2()
	{
		NanobotFormationAnalyzer analyzer = new(GenerateNanobots(), _origin);
		(_, Point point) = analyzer.FindPointInRangeOfMostNanobots();
		int result = MathG.ManhattanDistance(_origin, point);
		return $"{result}";
	}
}
