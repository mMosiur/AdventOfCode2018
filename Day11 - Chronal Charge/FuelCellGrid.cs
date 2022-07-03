namespace AdventOfCode.Year2018.Day11;

public class FuelCellGrid
{
	private readonly int[,] _fuelCellsPowerLevels;

	public int GridSerialNumber { get; }

	public int Width => _fuelCellsPowerLevels.GetLength(0);
	public int Height => _fuelCellsPowerLevels.GetLength(1);

	public FuelCellGrid(int gridSerialNumber)
	{
		GridSerialNumber = gridSerialNumber;
		_fuelCellsPowerLevels = new int[300, 300];
		for (int x = 1; x <= 300; x++)
		{
			for (int y = 1; y <= 300; y++)
			{
				_fuelCellsPowerLevels[x - 1, y - 1] = CalculateFuelCellPowerLevel(x, y);
			}
		}
	}

	public int this[int x, int y]
	{
		get => _fuelCellsPowerLevels[x, y];
	}

	public int SumSquarePowerLevels(int x, int y, int size)
	{
		int sum = 0;
		for (int i = x; i < x + size; i++)
		{
			for (int j = y; j < y + size; j++)
			{
				sum += _fuelCellsPowerLevels[i - 1, j - 1];
			}
		}
		return sum;
	}

	public int CalculateFuelCellPowerLevel(int x, int y)
	{
		checked
		{
			int rackId = x + 10;
			int powerLevel = rackId * y;
			powerLevel += GridSerialNumber;
			powerLevel *= rackId;
			powerLevel = powerLevel / 100 % 10;
			powerLevel -= 5;
			return powerLevel;
		}
	}
}
