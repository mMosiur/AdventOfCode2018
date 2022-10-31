using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day20;

public sealed class Day20Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 20;
	public override string Title => "A Regular Map";

	private readonly int _partTwoMinDistance;
	private readonly Lazy<RoomDistances> _roomDistances;

	private RoomDistances RoomDistances => _roomDistances.Value;

	public Day20Solver(Day20SolverOptions options) : base(options)
	{
		_partTwoMinDistance = options.PartTwoMinDistance;
		_roomDistances = new Lazy<RoomDistances>(GenerateRoomDistances);
	}

	public Day20Solver(Action<Day20SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day20Solver() : this(Day20SolverOptions.Default)
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
		int result = RoomDistances.Count(kvp => kvp.Value >= _partTwoMinDistance);
		return result.ToString();
	}
}
