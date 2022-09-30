using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day18.LumberCollection;

namespace AdventOfCode.Year2018.Day18;

public class Day18Solver : DaySolver
{
	private readonly Day18SolverOptions _options;
	private readonly LumberCollectionArea _initialArea;

	public Day18Solver(Day18SolverOptions options) : base(options)
	{
		_options = options;
		_initialArea = LumberCollectionArea.Parse(Input);
	}

	public Day18Solver(Action<Day18SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public override string SolvePart1()
	{
		int simulationMinutesCount = _options.PartOneSimulationMinutesCount;

		LumberCollectionAreaSimulator simulator = new(_initialArea);
		simulator.Simulate(simulationMinutesCount);
		int result = simulator.Area.ResourceValue;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
