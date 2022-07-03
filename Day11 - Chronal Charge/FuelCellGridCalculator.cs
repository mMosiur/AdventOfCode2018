namespace AdventOfCode.Year2018.Day11;

public class FuelCellGridCalculator
{
	private readonly FuelCellGrid _grid;

	public FuelCellGridCalculator(FuelCellGrid grid)
	{
		_grid = grid;
	}

	public int SumSquarePowerLevels(int topLeftX, int topLeftY, int squareSize)
	{
		int sum = 0;
		for (int x = topLeftX; x < topLeftX + squareSize; x++)
		{
			for (int y = topLeftY; y < topLeftY + squareSize; y++)
			{
				sum += _grid[x, y];
			}
		}
		return sum;
	}

	public (int topLeftX, int topLeftY) FindMaxSumPowerLevelsCoord(int squareSize)
	{
		int maxSum = int.MinValue;
		int maxSumTopLeftX = 0;
		int maxSumTopLeftY = 0;
		for (int x = 1; x <= _grid.Size - squareSize; x++)
		{
			for (int y = 1; y <= _grid.Size - squareSize; y++)
			{
				int sum = SumSquarePowerLevels(x, y, squareSize);
				if (sum > maxSum)
				{
					maxSum = sum;
					maxSumTopLeftX = x;
					maxSumTopLeftY = y;
				}
			}
		}
		if (maxSum == int.MinValue)
		{
			throw new ApplicationException("No max sum found.");
		}
		return (maxSumTopLeftX, maxSumTopLeftY);
	}

	public (int TopLeftX, int TopLeftY, int Size) FindMaxSumPowerLevelsCoord()
	{
		int maxSum = int.MinValue;
		int maxSumTopLeftX = 0;
		int maxSumTopLeftY = 0;
		int maxSumSize = 0;
		// Prepare partial-sum array
		int[,] partialSums = new int[_grid.Size + 1, _grid.Size + 1];
		for (int x = 1; x <= _grid.Size; x++)
		{
			for (int y = 1; y <= _grid.Size; y++)
			{
				partialSums[x, y] = _grid[x, y] + partialSums[x - 1, y] + partialSums[x, y - 1] - partialSums[x - 1, y - 1];
			}
		}
		// Calculate sums
		for (int size = 1; size <= _grid.Size; size++)
		{
			for (int x = size; x <= _grid.Size; x++)
			{
				for (int y = size; y <= _grid.Size; y++)
				{
					int sum = partialSums[x, y] - partialSums[x - size, y] - partialSums[x, y - size] + partialSums[x - size, y - size];
					if (sum > maxSum)
					{
						maxSum = sum;
						maxSumTopLeftX = x - size + 1;
						maxSumTopLeftY = y - size + 1;
						maxSumSize = size;
					}
				}
			}
		}
		return (maxSumTopLeftX, maxSumTopLeftY, maxSumSize);
	}
}
