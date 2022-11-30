using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day24;

public sealed class Day24Solver : DaySolver
{
	private readonly Lazy<CombatSimulator> _simulator;
	private readonly Day24SolverOptions _options;

	public override int Year => 2018;
	public override int Day => 24;
	public override string Title => "Immune System Simulator 20XX";

	private CombatSimulator Simulator => _simulator.Value;

	public Day24Solver(Day24SolverOptions options) : base(options)
	{
		_simulator = new Lazy<CombatSimulator>(() =>
		{
			(Army army1, Army army2) = InputParser.Parse(InputLines);
			return new(army1, army2);
		});
		_options = options;
	}

	public Day24Solver(Action<Day24SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day24Solver() : this(new Day24SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		Army winningArmy = Simulator.Simulate() ?? throw new DaySolverException("The combat has ended in tie.");
		int result = winningArmy.ActiveGroups.Sum(g => g.UnitCount);
		return $"{result}";
	}

	public override string SolvePart2()
	{
		int boost = Simulator.FindAndSetSmallestBootValueForArmyToWin(
			_options.PartTwoBoostingArmyName,
			_options.LowerBoostBound,
			_options.UpperBoostBound
		);
		// After previous call the army has already been assigned specified boost value.
		Army winningArmy = Simulator.Simulate() ?? throw new UnreachableException("The boost value would not have been found if the combat ended in tie.");
		int result = winningArmy.ActiveGroups.Sum(g => g.UnitCount);
		return $"{result}";
	}
}
