using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22;

public class CaveSystemBuilder
{
	private ushort? _depth;
	private Coordinate? _targetCoordinate;
	private CaveDataCalculator? _calculator;

	public CaveSystemBuilder()
	{
	}

	public CaveSystemBuilder(Coordinate targetCoordinate)
	{
		_targetCoordinate = targetCoordinate;
	}

	public CaveSystemBuilder WithTargetCoordinate(Coordinate targetCoordinate)
	{
		if (_targetCoordinate is not null)
		{
			throw new InvalidOperationException("Target coordinate has already been set.");
		}
		_targetCoordinate = targetCoordinate;
		return this;
	}

	public CaveSystemBuilder WithDepth(ushort depth)
	{
		if (_depth is not null)
		{
			throw new InvalidOperationException("Depth has already been set.");
		}
		_depth = depth;
		return this;
	}

	private ushort[,] GenerateErosionLevels()
	{
		CaveDataCalculator calc = _calculator ?? throw new InvalidOperationException("Cave data calculator has not been created.");
		Coordinate targetCoordinate = _targetCoordinate ?? throw new InvalidOperationException("Target coordinate has not been set.");
		ushort width = Convert.ToUInt16(targetCoordinate.X + 1);
		ushort height = Convert.ToUInt16(targetCoordinate.Y + 1);
		ushort[,] erosionLevels = new ushort[width, height];
		erosionLevels[0, 0] = calc.CalculateErosionLevel(0);
		for (ushort x = 1; x < width; x++)
		{
			const ushort y = 0;
			ushort erosionLevel = calc.CalculateErosionLevel(new Coordinate(x, y));
			erosionLevels[x, y] = erosionLevel;
		}
		for (ushort y = 1; y < height; y++)
		{
			const ushort x = 0;
			erosionLevels[x, y] = calc.CalculateErosionLevel(new Coordinate(x, y));
		}
		for (ushort x = 1; x < width; x++)
		{
			for (ushort y = 1; y < height; y++)
			{
				erosionLevels[x, y] = calc.CalculateErosionLevel(
					erosionLevelLeft: erosionLevels[x - 1, y],
					erosionLevelAbove: erosionLevels[x, y - 1]
				);
			}
		}
		erosionLevels[width - 1, height - 1] = calc.CalculateErosionLevel(0);
		return erosionLevels;
	}

	private RegionType[,] GenerateRegionTypes(ushort[,] erosionLevels)
	{
		CaveDataCalculator calc = _calculator ?? throw new InvalidOperationException("Cave data calculator has not been created.");
		int width = erosionLevels.GetLength(0);
		int height = erosionLevels.GetLength(1);
		RegionType[,] regionTypes = new RegionType[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				regionTypes[x, y] = calc.CalculateRegionType(erosionLevels[x, y]);
			}
		}
		return regionTypes;
	}

	public CaveSystem Build()
	{
		if (_depth is null)
		{
			throw new InvalidOperationException("Depth has not been set.");
		}
		if (_targetCoordinate is null)
		{
			throw new InvalidOperationException("Target coordinate has not been set.");
		}
		_calculator = new CaveDataCalculator(_depth.Value, _targetCoordinate.Value);
		ushort[,] erosionLevels = GenerateErosionLevels();
		RegionType[,] regionTypes = GenerateRegionTypes(erosionLevels);
		return new CaveSystem(_depth.Value, _targetCoordinate.Value, regionTypes);
	}
}
