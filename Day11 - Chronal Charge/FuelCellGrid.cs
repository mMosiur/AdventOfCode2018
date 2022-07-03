namespace AdventOfCode.Year2018.Day11;

public class FuelCellGrid
{
	private readonly int[,] _fuelCellsPowerLevels;

	public int SerialNumber { get; }
	public int Size { get; }

	public FuelCellGrid(int size, int serialNumber)
	{
		SerialNumber = serialNumber;
		Size = size;
		_fuelCellsPowerLevels = new int[Size, Size];
		for (int x = 1; x <= Size; x++)
		{
			for (int y = 1; y <= Size; y++)
			{
				_fuelCellsPowerLevels[x - 1, y - 1] = CalculateFuelCellPowerLevel(x, y, SerialNumber);
			}
		}
	}

	public int this[int x, int y]
	{
		get => _fuelCellsPowerLevels[x - 1, y - 1];
	}

	public static int CalculateFuelCellPowerLevel(int x, int y, int gridSerialNumber)
	{
		int rackId = x + 10; // Find the fuel cell's rack ID, which is its X coordinate plus 10.
		int powerLevel = rackId * y; // Begin with a power level of the rack ID times the Y coordinate.
		powerLevel += gridSerialNumber; // Increase the power level by the value of the grid serial number (your puzzle input).
		powerLevel *= rackId; // Set the power level to itself multiplied by the rack ID.
		powerLevel = powerLevel / 100 % 10; // Keep only the hundreds digit of the power level (so 12345 becomes 3; numbers with no hundreds digit become 0).
		powerLevel -= 5; // Subtract 5 from the power level.
		return powerLevel;
	}
}
