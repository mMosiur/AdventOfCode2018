using AdventOfCode.Abstractions;
using AdventOfCode.Year2018.Day18.LumberCollection;

namespace AdventOfCode.Year2018.Day18;

public sealed class Day18Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 18;
	public override string Title => "Settlers of The North Pole";

	private readonly Day18SolverOptions _options;
	private readonly LumberCollectionArea _initialArea;

	public Day18Solver(Day18SolverOptions options) : base(options)
	{
		_options = options;
		_initialArea = LumberCollectionArea.Parse(Input);
	}

	public Day18Solver(Action<Day18SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day18Solver() : this(new Day18SolverOptions())
	{
	}

	private static int GetResourceValueAfterSimulation(LumberCollectionAreaSimulator simulator, int simulationMinutesCount)
	{
		simulator.Simulate(simulationMinutesCount);
		return simulator.Area.ResourceValue;
	}

	public override string SolvePart1()
	{
		int simulationMinutesCount = _options.PartOneSimulationMinutesCount;
		LumberCollectionAreaSimulator simulator = new LumberCollectionAreaSimulatorBruteForce(_initialArea);

		int result = GetResourceValueAfterSimulation(simulator, simulationMinutesCount);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int simulationMinutesCount = _options.PartTwoSimulationMinutesCount;
		LumberCollectionAreaSimulator simulator = new LumberCollectionAreaSimulatorRepetitionAware(_initialArea);

		int result = GetResourceValueAfterSimulation(simulator, simulationMinutesCount);
		return result.ToString();
	}
}
