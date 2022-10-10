using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day20;

public class Day20Solver : DaySolver
{
	private readonly Lazy<RoomDistances> _roomDistances;

	private RoomDistances RoomDistances => _roomDistances.Value;

	public Day20Solver(Day20SolverOptions options) : base(options)
	{
		_roomDistances = new Lazy<RoomDistances>(GenerateRoomDistances);
	}

	public Day20Solver(Action<Day20SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	private RoomDistances GenerateRoomDistances()
	{
		PathRegex pathRegex = new(Input);
		return pathRegex.BuildRoomDistances();
	}

	public override string SolvePart1()
	{
		int result = RoomDistances.Max(kvp => kvp.Value);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
