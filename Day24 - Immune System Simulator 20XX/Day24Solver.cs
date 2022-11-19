using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day24;

public sealed class Day24Solver : DaySolver
{
	public override int Year => 2018;

	public override int Day => 24;

	public override string Title => "Immune System Simulator 20XX";

	public Day24Solver(Day24SolverOptions options) : base(options)
	{
	}

	public Day24Solver(Action<Day24SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day24Solver() : this(Day24SolverOptions.Default)
	{
	}

	public override string SolvePart1()
	{
		(Army army1, Army army2) = InputParser.Parse(InputLines);
		CombatSimulator simulator = new(army1, army2);
		simulator.Simulate();
		int army1units = army1.ActiveGroups.Sum(g => g.UnitCount);
		int army2units = army2.ActiveGroups.Sum(g => g.UnitCount);
		int result = army1units + army2units;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
