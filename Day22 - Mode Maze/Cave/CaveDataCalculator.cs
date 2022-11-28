namespace AdventOfCode.Year2018.Day22.Cave;

class CaveDataCalculator
{
	private const ushort EROSION_LEVEL_MODULUS = 20183;
	private const ushort REGION_TYPE_MODULUS = 3;
	private const int X0_GEOLOGIC_INDEX_MULTIPLIER = 48271;
	private const int Y0_GEOLOGIC_INDEX_MULTIPLIER = 16807;

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

	public ushort GenerateEdgeErosionLevel(Coordinate coordinate)
	{
		uint geologicIndex = coordinate switch
		{
			(0, 0) => 0,
			{ X: 0 } => (uint)(coordinate.Y * X0_GEOLOGIC_INDEX_MULTIPLIER),
			{ Y: 0 } => (uint)(coordinate.X * Y0_GEOLOGIC_INDEX_MULTIPLIER),
			_ when coordinate == _targetCoordinate => 0,
			_ => throw new InvalidOperationException("Coordinate is not on the edge of the cave system."),
		};
		return CalculateErosionLevel(geologicIndex);
	}

	public ushort GenerateInteriorErosionLevel(ushort erosionLevelLeft, ushort erosionLevelAbove)
	{
		uint geologicIndex = (uint)(erosionLevelLeft * erosionLevelAbove % EROSION_LEVEL_MODULUS);
		return CalculateErosionLevel(geologicIndex);
	}

#pragma warning disable CA1822 // It does not access instance data but it potentially could
	public RegionType CalculateRegionType(ushort erosionLevel)
	{
		return (erosionLevel % REGION_TYPE_MODULUS) switch
		{
			0 => RegionType.Rocky,
			1 => RegionType.Wet,
			2 => RegionType.Narrow,
			_ => throw new InvalidOperationException("Erosion level calculation error.")
		};
	}
#pragma warning restore CA1822

#pragma warning disable CA1822 // It does not access instance data but it potentially could
	public int CalculateRiskLevel(RegionType regionType) => regionType switch
	{
		RegionType.Rocky => 0,
		RegionType.Wet => 1,
		RegionType.Narrow => 2,
		_ => throw new ArgumentException("Invalid region type.", nameof(regionType))
	};
#pragma warning restore CA1822

	public int CalculateRiskLevel(CaveSystem caveSystem)
	{
		Area areaOfInterest = new(
			xRange: new Range(0, caveSystem.TargetCoordinate.X),
			yRange: new Range(0, caveSystem.TargetCoordinate.Y)
		);
		return caveSystem.EnumerateRegions(areaOfInterest)
			.Sum(CalculateRiskLevel);
	}
}
