using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day11;

public sealed class Day11Solver : DaySolver
{
	public override int Year => 2018;
	public override int Day => 11;
	public override string Title => "Chronal Charge";

	private readonly int _inputNumber;

	private readonly Lazy<FuelCellGrid> _grid;

	private FuelCellGrid Grid => _grid.Value;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		try
		{
			_inputNumber = options.GridSerialNumber ?? int.Parse(Input);
			_grid = new Lazy<FuelCellGrid>(() => new FuelCellGrid(options.GridSize, _inputNumber));
		}
		catch (FormatException e)
		{
			throw new InputException("Invalid input.", e);
		}
	}

	public Day11Solver(Action<Day11SolverOptions> configure)
		: this(DaySolverOptions.FromConfigureAction(configure))
	{
	}

	public Day11Solver() : this(new Day11SolverOptions())
	{
	}

	public override string SolvePart1()
	{
		FuelCellGridCalculator calc = new(Grid);
		(int x, int y) = calc.FindMaxSumPowerLevelsCoord(3);
		return $"{x},{y}";
	}

	public override string SolvePart2()
	{
		FuelCellGridCalculator calc = new(Grid);
		(int x, int y, int size) = calc.FindMaxSumPowerLevelsCoord();
		return $"{x},{y},{size}";
	}
}
