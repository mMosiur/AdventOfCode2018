namespace AdventOfCode.Year2018.Day17.Scan;

public enum GroundType
{
	Sand,
	Clay,
	WaterResting,
	WaterFlowing,
	WaterSpring,
}

public static class GroundTypes
{
	public static GroundType FromChar(char c) => c switch
	{
		'.' => GroundType.Sand,
		'#' => GroundType.Clay,
		'~' => GroundType.WaterResting,
		'|' => GroundType.WaterFlowing,
		'+' => GroundType.WaterSpring,
		_ => throw new ArgumentException("Invalid ground type character.", nameof(c)),
	};

	public static char ToChar(this GroundType groundType) => groundType switch
	{
		GroundType.Sand => '.',
		GroundType.Clay => '#',
		GroundType.WaterResting => '~',
		GroundType.WaterFlowing => '|',
		GroundType.WaterSpring => '+',
		_ => throw new ArgumentException("Invalid ground type.", nameof(groundType)),
	};

	public static bool IsWater(this GroundType groundType)
		=> groundType is GroundType.WaterResting or GroundType.WaterFlowing;

	public static bool IsSpreading(this GroundType groundType)
		=> groundType is GroundType.WaterFlowing or GroundType.WaterSpring;

	public static bool IsPassable(this GroundType groundType)
		=> groundType is GroundType.Sand;

	public static bool IsBlocked(this GroundType groundType)
		=> groundType is GroundType.Clay or GroundType.WaterResting;
}
