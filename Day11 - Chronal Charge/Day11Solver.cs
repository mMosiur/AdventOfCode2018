using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2018.Day11;

public class Day11Solver : DaySolver
{
	private readonly int _inputNumber;

	public Day11Solver(Day11SolverOptions options) : base(options)
	{
		try
		{
			_inputNumber = options.GridSerialNumber ?? int.Parse(Input);
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
		var grid = new FuelCellGrid(_inputNumber);
		int max = int.MinValue;
		int maxSumX = 0;
		int maxSumY = 0;
		for(int x = 1; x <= grid.Width - 3; x++)
		{
			for(int y = 1; y <= grid.Height - 3; y++)
			{
				int sum = grid.SumSquarePowerLevels(x, y, 3);
				if(sum > max)
				{
					max = sum;
					maxSumX = x;
					maxSumY = y;
				}
			}
		}
		return $"{maxSumX},{maxSumY}";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
