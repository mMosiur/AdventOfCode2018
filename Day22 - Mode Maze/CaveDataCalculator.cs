using AdventOfCode.Year2018.Day22.Geometry;

namespace AdventOfCode.Year2018.Day22;

public class CaveDataCalculator
{
	private const ushort EROSION_LEVEL_MODULUS = 20183;
	private const int X0_GEOLOGIC_INDEX_MULTIPLIER = 48271;
	private const int Y1_GEOLOGIC_INDEX_MULTIPLIER = 16807;

	private readonly uint _depth;
	private readonly Coordinate _targetCoordinate;

	public CaveDataCalculator(uint depth, Coordinate targetCoordinate)
	{
		_depth = depth;
		_targetCoordinate = targetCoordinate;
	}

	public ushort CalculateErosionLevel(uint geologicIndex)
	{
		return (ushort)((geologicIndex + _depth) % EROSION_LEVEL_MODULUS);
	}

	public ushort CalculateErosionLevel(Coordinate coordinate)
	{
		uint geologicIndex = coordinate switch
		{
			(0, 0) => 0,
			{ X: 0 } => (uint)(coordinate.Y * X0_GEOLOGIC_INDEX_MULTIPLIER),
			{ Y: 0 } => (uint)(coordinate.X * Y1_GEOLOGIC_INDEX_MULTIPLIER),
			_ when coordinate == _targetCoordinate => 0,
			_ => throw new InvalidOperationException("Coordinate is not on the edge of the cave system."),
		};
		return CalculateErosionLevel(geologicIndex);
	}

	public ushort CalculateErosionLevel(ushort erosionLevelLeft, ushort erosionLevelAbove)
	{
		uint geologicIndex = (uint)(erosionLevelLeft * erosionLevelAbove % EROSION_LEVEL_MODULUS);
		return CalculateErosionLevel(geologicIndex);
	}

	public RegionType CalculateRegionType(ushort erosionLevel)
	{
		return (erosionLevel % 3) switch
		{
			0 => RegionType.Rocky,
			1 => RegionType.Wet,
			2 => RegionType.Narrow,
			_ => throw new Exception("This should never happen.")
		};
	}

	public int CalculateRiskLevel(RegionType regionType) => regionType switch
	{
		RegionType.Rocky => 0,
		RegionType.Wet => 1,
		RegionType.Narrow => 2,
		_ => throw new ArgumentException("Invalid region type.", nameof(regionType))
	};

	public int CalculateRiskLevel(CaveSystem caveSystem)
	{
		return caveSystem.EnumerateRegions().Sum(r => CalculateRiskLevel(r));
	}
}
