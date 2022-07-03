using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day11;

public class Day11Solver : DaySolver
{
	private readonly int _inputNumber;

	private readonly Lazy<FuelCellGrid> _grid;

	public FuelCellGrid Grid => _grid.Value;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		try
		{
			_inputNumber = options.GridSerialNumber ?? int.Parse(Input);
			_grid = new Lazy<FuelCellGrid>(() => new FuelCellGrid(options.GridSize, _inputNumber));
		}
		catch (FormatException e)
		{
			throw new ApplicationException("Invalid input.", e);
		}
	}

	public Day11Solver(Action<Day11SolverOptions>? configure = null)
		: this(DaySolverOptions.FromConfigureAction(configure))
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
